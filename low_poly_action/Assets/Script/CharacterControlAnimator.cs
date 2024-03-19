using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControlAnimator : MonoBehaviour
{
    private CharacterManager _characterManager;
    private static readonly int VelocityX = Animator.StringToHash("velocityX");
    private static readonly int VelocityZ = Animator.StringToHash("velocityZ");
    protected virtual void Awake()
    {
        _characterManager = GetComponent<CharacterManager>();
    }

    public void UpdateAnimation(float veloX, float veloY)
    {
        _characterManager._animator.SetFloat(VelocityX, veloX, 0.1f, Time.deltaTime);
        _characterManager._animator.SetFloat(VelocityZ, veloY, 0.1f, Time.deltaTime);
    }

}
