using System;
using Extensions;
using ImprovedTimers;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField, Range(1, 11)] private Vector2Int jumpForce;
    [SerializeField, Range(0, 10)] private float moveSpeed = 5f;
    [SerializeField] private Transform playerModel;
    [SerializeField] private InputReader input;
    
    public event Action<Vector3> OnJump = delegate { };
    public event Action<Vector3> OnLand = delegate { };
    
    private Rigidbody _rb;
    private Camera _mainCamera;
    private Vector2 _moveInput;
    
    public Vector3 GetMovementVelocity() => _moveInput;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        input.EnablePlayerActions();
        input.Move += OnMove;
    }
    
    private void OnDisable()
    {
        input.Move -= OnMove;
    }
    
    private void Start()
    {
        _mainCamera = Camera.main;
        input.Move += direction => _moveInput =  direction;
        input.Jump += isJumpKeyPressed =>
        {
            if (isJumpKeyPressed)
            {
                Jump();
            }
            else
            {
                Land();
            }
        };
        input.EnablePlayerActions();
    }
    
    private void OnMove(Vector2 movement) => _moveInput = movement;
    public void Jump() => OnJump?.Invoke(Vector3.up * jumpForce.y);
    public void Land() => OnLand?.Invoke(Vector3.zero);

    private void Update()
    {
        Move(CalculateMovementDirection());
    }
    
    private void FixedUpdate()
    {
        var movement = new Vector3(_moveInput.x, 0, _moveInput.y);
        _rb.MovePosition(_rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private void Move(Vector3 direction)
    {
        if (direction.sqrMagnitude > 0.01f)
        {
            playerModel.rotation = Quaternion.LookRotation(direction);
            transform.position += direction * (Time.deltaTime * moveSpeed);
        }
    }

    private Vector3 CalculateMovementDirection()
    {
        var cameraForward = _mainCamera.transform.forward.With(y: 0);
        var cameraRight = _mainCamera.transform.right.With(y: 0);

        return cameraForward * _moveInput.y + cameraRight * _moveInput.x;
    }
}
