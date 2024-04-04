using UnityEngine;
using UnityEngine.InputSystem;

public class ControlMovement : MonoBehaviour
{
    private Transform tf;
    public Transform TF => tf;
    
    private CharacterController characterController;
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
        HandleRotation();
    }

    public void UpdateState(MovementState _newState)
    {
        state = _newState;
    }
    public void UpdateMoveDirection(Vector3 _moveValue)
    {
        moveDir = _moveValue;
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
    
    public void SetRotationDir(Vector3 _dir)
    {
        rotationDir = _dir;
    }
    private void HandleRotation()
    {
        if(rotationDir == Vector3.zero) return;
        
        var _target = Quaternion.LookRotation(rotationDir);
        var _current = transform.rotation;
        var _dir = Quaternion.Slerp(_current, _target, configMovement.rotationSpeed * Time.deltaTime);
        transform.rotation = _dir;
    }
}
