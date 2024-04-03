using UnityEngine;

public class ControlMovement : MonoBehaviour
{
    private Transform tf;
    public Transform TF => tf;
    
    private CharacterController characterController;
    private Vector2 moveValue;
    private Vector3 moveDir;
    private Vector3 rotationDir;
    private float moveSpeed;
    private MovementState state;
    private Quaternion qE;
    private float freeRotationY;
    private float currentRotationY;
    private ConfigMovementSO configMovement => ConfigCenter.Instance.GetConfigMovement();
    private void Start()
    {
        tf = transform;
        characterController = gameObject.GetOrAddComponent<CharacterController>();
    }

    public void HandleMovement()
    {
        HandleGroundMovement();
        HandleFreeRotation();
    }

    public void UpdateState(MovementState _newState)
    {
        state = _newState;
    }
    public void UpdateMoveDirection(Vector3 _moveValue)
    {
        moveDir = _moveValue;
    }

    public void UpdateRotation(Vector3 _rotationDir)
    {
        rotationDir = _rotationDir;
    }

    private void HandleGroundMovement()
    {
        switch (state)
        {
            case MovementState.Idle:
                moveSpeed = 0;
                break;
            case MovementState.Walking:
                moveSpeed = configMovement.walkSpeed;
                break;
            case MovementState.Running:
                moveSpeed = configMovement.runSpeed;
                break;
        }
        
        characterController.Move(moveDir * (moveSpeed * Time.deltaTime));
    }
    

    private void HandleLockRotation()
    {
        rotationDir = Vector3.zero;
        var cam = PlayerCamera.Instance.Tf;
        rotationDir = cam.forward * moveValue.y;
        rotationDir += cam.right * moveValue.x;
        rotationDir.Normalize();
        rotationDir.y = 0;
        
        if (rotationDir == Vector3.zero)
        {
            rotationDir = transform.forward;
        }

        var newDir = Quaternion.LookRotation(rotationDir);
        var camDir = Quaternion.Slerp(transform.rotation, newDir, configMovement.rotationSpeed * Time.deltaTime);
        transform.rotation = camDir;
    }
    
    public void HandleFreeRotation()
    {
        qE = Quaternion.LookRotation(rotationDir, transform.up);
        
        var _dir = Quaternion.Slerp( transform.rotation , qE, configMovement.rotationSpeed * Time.deltaTime);
        transform.rotation = _dir;
    }
}
