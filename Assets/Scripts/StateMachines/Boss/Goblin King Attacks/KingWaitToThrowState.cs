using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingWaitToThrowState : BossBaseState
{
    float time;
    bool forceRecived;
    public KingWaitToThrowState(BossStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        _stateMachine.GrabWeaponDamage.Force(-_stateMachine.transform.forward, 10);
        _stateMachine.Animator.CrossFadeInFixedTime("Start_Throw", 0.1f);
    }

    public override void Tick(float deltaTime)
    {
        bool maxWait = time > _stateMachine.MaxWaitToGrabTime;
        if (_stateMachine.PreThrowHandler.InArea() || maxWait)
        {
            _stateMachine.SwitchState(new KingThrowState(_stateMachine));
        }

        if (time > .25 && !forceRecived)
        {
            forceRecived = true;
            _stateMachine.ForceReciver.AddForce(_stateMachine.transform.forward * 15);
        }

        Move(deltaTime);
        time += 1 * deltaTime;
    }
    public override void Exit() { }
}