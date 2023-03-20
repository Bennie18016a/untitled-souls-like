using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDrinkState : PlayerBaseState
{

    private float _previousFrameTime;
    public PlayerDrinkState(PlayerStateMachine stateMachine) : base(stateMachine) { }
    public override void Enter()
    {
        _stateMachine.Animator.CrossFadeInFixedTime("Drink", 0.1f);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        float normalizedTime = GetNormalizedTime(_stateMachine.Animator);

        if (normalizedTime >= _previousFrameTime && normalizedTime < 1f)
        {
        }
        else
        {
            ReturnToLocomotion();
        }

        _previousFrameTime = normalizedTime;
    }
    public override void Exit()
    {
        _stateMachine.Stamina.SetNaturalStamina(true);
    }
}