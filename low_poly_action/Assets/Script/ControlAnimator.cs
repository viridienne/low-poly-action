using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class ControlAnimator : MonoBehaviour
{
    [ShowInInspector]private Animator animator;
    private static readonly int VelocityX = Animator.StringToHash("velocityX");
    private static readonly int VelocityZ = Animator.StringToHash("velocityZ");

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetBasicBlend(float _veloX, float _veloZ)
    {
        animator.SetFloat(VelocityX, _veloX);
        animator.SetFloat(VelocityZ, _veloZ);
    }
}
