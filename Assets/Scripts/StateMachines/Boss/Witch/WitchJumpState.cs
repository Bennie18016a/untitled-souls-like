using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchJumpState : BossBaseState
{
    private float _previousFrameTime;
    private string Attack;
    private bool AddedForce;

    public WitchJumpState(BossStateMachine stateMachine, string attack) : base(stateMachine) { Attack = attack; }

    public override void Enter()
    {
        _stateMachine.Animator.CrossFadeInFixedTime("Jump", 0.1f);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        Debug.Log(AddedForce);

        float normalizedTime = GetNormalizedTime(_stateMachine.Animator);

        if (normalizedTime >= _previousFrameTime && normalizedTime < 1f)
        {
            Debug.Log("Attacking");
        }
        else
        {
            switch (Attack)
            {
                case "Tread":
                    _stateMachine.SwitchState(new WitchTreadAttackState(_stateMachine));
                    break;
            }
        }

        if (normalizedTime > _previousFrameTime && normalizedTime > .25f && !AddedForce)
        {
            _stateMachine.ForceReciver.AddForce(Vector3.up * 20);
            AddedForce = true;
            _stateMachine.ForceReciver.useGravity = false;
        }

        _previousFrameTime = normalizedTime;

    }
    public override void Exit() { }
}