using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingKickState : BossBaseState
{
    private float _previousFrameTime;

    public KingKickState(BossStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        _stateMachine.Animator.CrossFadeInFixedTime("Kick", 0.1f);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        float normalizedTime = GetNormalizedTime(_stateMachine.Animator);

        if (normalizedTime >= _previousFrameTime && normalizedTime < 1f)
        {
            Debug.Log("Attacking");
        }
        else
        {
            _stateMachine.SwitchState(new BossChaseState(_stateMachine));
        }
        _previousFrameTime = normalizedTime;

    }
    public override void Exit() { }
}