using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingPunchState : BossBaseState
{
    private float _previousFrameTime;
    private int direction;

    public KingPunchState(BossStateMachine stateMachine, int attackIndex) : base(stateMachine)
    {
        direction = attackIndex;
    }

    public override void Enter()
    {
        if (direction == 1)
        {
            _stateMachine.Animator.CrossFadeInFixedTime("Punch Right", 0.01f);
        }
        else
        {
            _stateMachine.Animator.CrossFadeInFixedTime("Punch Left", 0.01f);
        }
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        FacePlayer();

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