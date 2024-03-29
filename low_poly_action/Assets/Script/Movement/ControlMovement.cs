using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlMovement : CharacterControlMovement
{
    private PlayerManager _playerManager;
    
    private Transform tf;
    public Transform TF => tf;

    private Vector2 moveValue;
    private Vector3 moveDir;
    private Vector3 rotationDir;

    [SerializeField] private ConfigMovementSO configMovement;
    protected override void Awake()
    {
        tf = transform;
        _playerManager = GetComponent<PlayerManager>();
    }

    public void Move(Vector2 _vector2)
    {
        var _current = tf.position;
        var _new = new Vector3(_current.x + _vector2.x, _current.y, _current.z + _vector2.y);
        tf.position = Vector3.Lerp(_current, _new, configMovement.walkSpeed * Time.deltaTime);
        if (_vector2.x != 0 || _vector2.y != 0)
        {
            //tf.rotation = Quaternion.Euler(0f, (float)(System.Math.Atan2((_vector2.x - 0), (_vector2.y - 0)) * 180 / 3.14), 0f);
        }
    }

    public void GetMovementInputValue()
    {
        moveValue = ReceiveInput.Instance.movementInputValue;
    }
    public void HandleAllMovement() //MOVEMENT BASE ON CAMERA PERSPECTIVE
    {
        //Grounded movement handle
        HandleGroundMovement();
        //Aerial movement handle
        //Rotation handle
        HandleRotation();
    }

    private void HandleGroundMovement()
    {
        GetMovementInputValue();
        var transform1 = PlayerCamera.Instance.transform;
        moveDir = transform1.forward * moveValue.y;
        moveDir += transform1.right * moveValue.x;
        moveDir.Normalize();
        moveDir.y = 0;
        if (ReceiveInput.Instance.moveAmount <= 0.5f)
        {
            _playerManager._characterController.Move(moveDir * (configMovement.walkSpeed * Time.deltaTime));
        }
        else if (ReceiveInput.Instance.moveAmount <= 1)
        {
            _playerManager._characterController.Move(moveDir * (configMovement.runSpeed * Time.deltaTime));
        }
    }

    private void HandleAerialMovement() 
    {
        
    }

    private void HandleRotation()
    {
        rotationDir = Vector3.zero;
        var cam = PlayerCamera.Instance._camera.transform;
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
}
