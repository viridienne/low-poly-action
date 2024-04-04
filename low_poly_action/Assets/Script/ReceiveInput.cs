using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Only store the input value from the player, and can be accessed by other classes, do not handle any logic
/// </summary>
public class ReceiveInput : MonoBehaviour
{
    public static ReceiveInput Instance;
    
    public Vector2 MovementInputValue => movementInputValue;
    public Vector2 LookInputValue => lookInputValue;
    public bool JumpInputValue => jumpInputValue;
    
    private Vector2 movementInputValue;
    private  Vector2 lookInputValue;
    private bool jumpInputValue;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OnMove(InputAction.CallbackContext _context)
    {
        movementInputValue  = _context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext _context)
    {
        lookInputValue  = _context.ReadValue<Vector2>();
    }
    
    public void OnJump(InputAction.CallbackContext _context)
    {
        switch (_context.phase)
        {
            case InputActionPhase.Performed:
                jumpInputValue = true;
                break;
            case InputActionPhase.Canceled:
                jumpInputValue = false;
                break;
        }
    }
}
