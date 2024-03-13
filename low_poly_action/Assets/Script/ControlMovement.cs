using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlMovement : MonoBehaviour
{
    private Transform tf;
    public Transform TF => tf;

    [SerializeField] private ConfigMovementSO configMovement;
    private void Awake()
    {
        tf = transform;
    }

    public void Move(Vector2 _vector2)
    {
        var _current = tf.position;
        var _new = new Vector3(_current.x + _vector2.x, _current.y, _current.z + _vector2.y);
        tf.position = Vector3.Lerp(_current, _new, configMovement.walkSpeed * Time.deltaTime);
        if (_vector2.x != 0 || _vector2.y != 0)
        {
            tf.rotation = Quaternion.Euler(0f, (float)(System.Math.Atan2((_vector2.x - 0), (_vector2.y - 0)) * 180 / 3.14), 0f);
        }
    }

}
