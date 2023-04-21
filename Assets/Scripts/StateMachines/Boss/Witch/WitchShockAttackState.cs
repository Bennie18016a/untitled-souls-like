using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchShockAttackState : BossBaseState
{
    private float _previousFrameTime;
    bool hasnotshot;

    public WitchShockAttackState(BossStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        _stateMachine.Animator.CrossFadeInFixedTime("Shock", 0.1f);
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
            // Return to locomotion once the animation is finished playing
            _stateMachine.SwitchState(new BossChaseState(_stateMachine));
        }

        if (normalizedTime >= _previousFrameTime && normalizedTime > .25f && !hasnotshot)
        {
            // Shoot the shockwave
            hasnotshot = true;
        }
        _previousFrameTime = normalizedTime;

    }
    public override void Exit() { }
}
