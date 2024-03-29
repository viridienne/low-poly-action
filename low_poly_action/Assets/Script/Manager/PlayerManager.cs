using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : CharacterManager
{
    [SerializeField] private ControlMovement _controlMovement;
    [SerializeField] public ControlAnimator _controlAnimator;
    [SerializeField] private ControlCombat _controlCombat;

    protected override void Update()
    {
        base.Update();
        if (_controlMovement)
        {
            _controlMovement.HandleAllMovement();
        }
    }
    protected override void LateUpdate()
    {
        base.LateUpdate();
        PlayerCamera.Instance.HandleCamera();
    }
}
