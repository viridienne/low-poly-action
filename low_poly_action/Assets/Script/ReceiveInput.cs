using System;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(ReceiveInput))]
[RequireComponent(typeof(PlayerInput))]
public class ReceiveInput : MonoBehaviour
{
    public static ReceiveInput Instance;
    [SerializeField] private ControlMovement movementController;
    [SerializeField] private ControlAnimator animatorController;
    public Vector2 inputValue;
    public float moveAmount;


    private void Start()
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
        inputValue  = _context.ReadValue<Vector2>();
        moveAmount = Mathf.Clamp01(Mathf.Abs(inputValue.x) + Mathf.Abs((inputValue.y)));
        
        //CHANGE BETWEEN WALK AND RUN ANIMATION
        switch (moveAmount)
        {
            case <= 0.5f and > 0:
                moveAmount = 0.5f;
                break;
            case <= 1 and > 0.5f:
                moveAmount = 1;
                break;
        }
    }
        
    public void OnNormalAttack(InputAction.CallbackContext _context)
    {
        animatorController.SetNormalAttack();
    }

    // private void Update()
    // {
    //      if (movementController)
    //      {
    //          movementController.Move(inputValue);
    //          animatorController.SetBasicBlend(inputValue.x,inputValue.y);
    //      }
    // }
}
