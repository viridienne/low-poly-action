using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityController : MonoBehaviour
{
    protected ControlMovement controlMovement;
    protected ControlAnimator controlAnimator;
    protected ControlCombat controlCombat;
    
    public ControlAnimator Animator => controlAnimator;

    protected virtual void Start()
    {
        controlMovement = gameObject.GetOrAddComponent<ControlMovement>();
        controlAnimator = gameObject.GetOrAddComponent<ControlAnimator>();
        controlCombat = gameObject.GetOrAddComponent<ControlCombat>();
    }

    protected virtual void Update()
    {

    }
    protected virtual void LateUpdate()
    {

    }
}
