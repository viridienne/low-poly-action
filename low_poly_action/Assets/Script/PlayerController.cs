using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Control and link the components of the player
/// </summary>
public class PlayerController : EntityController
{
    private ControlModel controlModel;

    protected ConfigMovementSO movementConfig => ConfigCenter.Instance.GetConfigMovement();
    
    private Vector3 moveDir;
    private Vector2 moveInput;
    private float rawMoveValue;
    
    private Vector2 currentVelocity;
    private Vector2 targetVelocity;
    private MovementType movementType;

    protected override void Start()
    {
        base.Start();
        controlModel = GetComponentInChildren<ControlModel>();
        controlAnimator.RegisterAnimator(controlModel.Animator);
        
        PlayerCamera.Instance.RegisterFollow(transform);
        PlayerCamera.Instance.RegisterLookAt(controlModel.LookAtTarget.transform);
    }

    protected override void Update()
    {
        base.Update();
        moveInput = ReceiveInput.Instance.MovementInputValue;

        UpdateMoveDirection();
        UpdateMovementState();
        
        if (controlMovement)
        {
            controlMovement.HandleMovement();
            UpdateRotate();
        }
        if(controlAnimator)
        {
            LerpingVelocity();
        }
    }
    
    public void LerpingVelocity()
    {
        currentVelocity = Vector2.Lerp(currentVelocity, targetVelocity, Time.deltaTime * movementConfig.blendSpeed);
        controlAnimator.UpdateAnimation(currentVelocity.x, currentVelocity.y);
    }
    
    public void UpdateMoveDirection()
    {
        var _camera = PlayerCamera.Instance.transform;
        moveDir = _camera.forward * moveInput.y;
        moveDir += _camera.right * moveInput.x;
        moveDir.Normalize();
        moveDir.y = 0;
        controlMovement.UpdateMoveDirection(moveDir);
    }
    
    public void UpdateMovementState()
    {
        
        var _absX = Mathf.Abs(moveInput.x);
        var _absY = Mathf.Abs(moveInput.y);

        rawMoveValue = Mathf.Clamp01(_absX + _absY);
        targetVelocity = new Vector2(_absX, _absY);
        
        switch (rawMoveValue)
        {
            case <= 0.5f and > 0:
                rawMoveValue = 0.5f;
                controlMovement.UpdateState(MovementState.Walking);
                break;
            case <= 1 and > 0.5f:
                rawMoveValue = 1;
                controlMovement.UpdateState(MovementState.Running);
                break;
        }
    }

    public void UpdateRotate()
    {
        Vector3 _rotationDir;
        var _cam = PlayerCamera.Instance.Tf;
        _rotationDir = _cam.forward * moveInput.y;
        _rotationDir += _cam.right * moveInput.x;
        _rotationDir.Normalize();
        _rotationDir.y = 0;
        
        if (_rotationDir == Vector3.zero)
        {
            _rotationDir = transform.forward;
        }
        controlMovement.SetRotationDir(_rotationDir);
    }
}
