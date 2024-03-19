using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public CharacterController _characterController;
    public Animator _animator;
    protected virtual void Awake()
    {
        DontDestroyOnLoad(this);
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {

    }
    protected virtual void LateUpdate()
    {

    }
}
