using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeavyAttackingState : PlayerBaseState
{
    private float _previousFrameTime;
    private bool _appliedForce;
    private Attack _attack;
    public PlayerHeavyAttackingState(PlayerStateMachine stateMachine, int attackIndex) : base(stateMachine)
    {
        _attack = _stateMachine.Attacks[attackIndex];
    }

    public override void Enter()
    {
        _stateMachine.Stamina.SetNaturalStamina(false);
        _stateMachine.WeaponDamage.SetDamage(_attack.AttackDamage + _stateMachine.Stats.Strength, _attack.Knockback);
        _stateMachine.Stamina.TakeStamina(_attack.StaminaCost);
        _stateMachine.Animator.CrossFadeInFixedTime(_attack.AnimationName, _attack.TransitionDuration);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        FaceTarget();

        float normalizedTime = GetNormalizedTime(_stateMachine.Animator);

        if (normalizedTime >= _previousFrameTime && normalizedTime < 1f)
        {
            if (normalizedTime >= _attack.ForceTime) { TryApplyForce(); }
        }
        else
        {
            ReturnToLocomotion();
        }
        _previousFrameTime = normalizedTime;

    }

    private void TryApplyForce()
    {
        if (_appliedForce) { return; }
        _stateMachine.ForceReciver.AddForce(_stateMachine.transform.forward * _attack.Force);
        _appliedForce = true;
    }
    public override void Exit()
    {
        _stateMachine.Stamina.SetNaturalStamina(true);
    }
}