using System;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(PlayerInput))]
public class ReceiveInput : MonoBehaviour
{
    [SerializeField] private ControlMovement movementController;
    private Vector2 inputValue;

    public void OnMove(InputAction.CallbackContext _context)
    {
        inputValue  = _context.ReadValue<Vector2>();
    }

    private void Update()
    {
        if (movementController)
        {
            movementController.Move(inputValue);
        }
    }
}
