using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.DefaultInputActions;

public interface IInputReader
{
    Vector2 Direction { get; }
    void EnablePlayerActions();
}

public class InputReader : ScriptableObject, IPlayerActions, IInputReader
{
    public event UnityAction<Vector2> Move = delegate { };
    public event UnityAction<Vector2, bool> Look = delegate { };
    public event UnityAction EnableMouseControlCamera = delegate { };
    public event UnityAction DisableMouseControlCamera = delegate { };
    public event UnityAction<bool> Jump = delegate { };
    public event UnityAction<bool> Dash = delegate { };
    public event UnityAction Attack = delegate { };
    public event UnityAction<RaycastHit> Click = delegate { };
    
    public void OnMove(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public Vector2 Direction { get; }
    public void EnablePlayerActions()
    {
        throw new System.NotImplementedException();
    }
}
