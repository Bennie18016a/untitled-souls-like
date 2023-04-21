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
            Collider[] colldiers = Physics.OverlapSphere(_stateMachine.transform.position, _stateMachine.ShockDistance);

            foreach (Collider collider in colldiers)
            {
                Debug.Log(collider.transform.name);
                if (!collider.CompareTag("Player")) continue;
                collider.transform.GetComponent<ForceReciver>().AddForce((collider.transform.position - _stateMachine.transform.position).normalized * 25);
            }
            hasnotshot = true;
        }
        _previousFrameTime = normalizedTime;

    }
    public override void Exit() { _stateMachine.ShockCooldown = 0; }
}
