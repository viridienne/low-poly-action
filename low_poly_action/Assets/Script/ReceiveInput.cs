using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

[RequireComponent(typeof(ReceiveInput))]
[RequireComponent(typeof(PlayerInput))]
public class ReceiveInput : MonoBehaviour
{
    public static ReceiveInput Instance;
    [HideInInspector] public PlayerManager playerManager;
    // [SerializeField] private ControlMovement movementController;
    // [SerializeField] private ControlAnimator animatorController;
    [FormerlySerializedAs("inputValue")] 
    public Vector2 movementInputValue;
    public Vector2 lookInputValue;
    public float moveAmount;

    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
    }

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
        movementInputValue  = _context.ReadValue<Vector2>();
        moveAmount = Mathf.Clamp01(Mathf.Abs(movementInputValue.x) + Mathf.Abs((movementInputValue.y)));
        
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
        // VeloX = 0 BECAUSE THIS IS NOT LOCK-ON
        playerManager._controlAnimator.UpdateAnimation(0,moveAmount);
    }
    
    public void OnLook(InputAction.CallbackContext _context)
    {
        lookInputValue  = _context.ReadValue<Vector2>();
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
