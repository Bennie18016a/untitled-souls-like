using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingWaitToThrowState : BossBaseState
{
    float time;

    public KingWaitToThrowState(BossStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        _stateMachine.Animator.CrossFadeInFixedTime("Start_Throw", 0.1f);
    }

    public override void Tick(float deltaTime)
    {
        if (_stateMachine.PreThrowHandler.InArea())
        {
            _stateMachine.SwitchState(new KingThrowState(_stateMachine));
        }

        if (time > _stateMachine.MaxWaitToGrabTime)
        {
            _stateMachine.SwitchState(new BossChaseState(_stateMachine));
        }

        Move(deltaTime);
        time += 1 * deltaTime;
    }
    public override void Exit() { }
}