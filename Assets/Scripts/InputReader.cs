using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static PlayerInputActions;

public interface IInputReader
{
    Vector2 Direction { get; }
    void EnablePlayerActions();
}

[CreateAssetMenu(fileName = "InputReader", menuName = "InputReader")]
public class InputReader : ScriptableObject, IPlayerActions, IInputReader
{
    public event UnityAction<Vector2> Move = delegate { };
    public event UnityAction<bool> Jump = delegate { };

    public PlayerInputActions inputActions;
    
    public Vector2 Direction => inputActions.Player.Move.ReadValue<Vector2>();
    public bool IsJumpKeyPressed => inputActions.Player.Jump.IsPressed();
    
    public event UnityAction<Vector2, bool> Look = delegate { };
    public event UnityAction EnableMouseControlCamera = delegate { };
    public event UnityAction DisableMouseControlCamera = delegate { };
    public event UnityAction<bool> Dash = delegate { };
    public event UnityAction Attack = delegate { };
    public event UnityAction<RaycastHit> Click = delegate { };
    
    public void EnablePlayerActions()
    {
        if (inputActions == null)
        {
            inputActions = new PlayerInputActions();
            inputActions.Player.SetCallbacks(this);
        }
        inputActions.Enable();
    }
    
    public void OnMove(InputAction.CallbackContext context)
    {
        Move?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                Jump?.Invoke(true);
                break;
            
            case InputActionPhase.Canceled:
                Jump?.Invoke(false);
                break;
            
            case InputActionPhase.Disabled:
            case InputActionPhase.Waiting:
            case InputActionPhase.Performed:
                break;
            
            default: throw new ArgumentOutOfRangeException();
        }
    }
    
    public void OnLook(InputAction.CallbackContext context)
    {
        // noop
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        // noop
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        // noop
    }
}
