using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChaseState : BossBaseState
{
    private readonly int LocomotionBlendTreeHash = Animator.StringToHash("Locomotion");
    private readonly int SpeedHash = Animator.StringToHash("Speed");
    private const float AnimatorDampTime = 0.1f;

    public BossChaseState(BossStateMachine stateMachine) : base(stateMachine) { }
    public override void Enter()
    {
        _stateMachine.Animator.CrossFadeInFixedTime(LocomotionBlendTreeHash, 0.1f);
    }

    public override void Tick(float deltaTime)
    {
        if (!_stateMachine.Active) { return; }

        if (IsInStafeRange())
        {
            _stateMachine.SwitchState(new BossStafeState(_stateMachine));
        }

        MoveToPlayer(deltaTime);
        FacePlayer();

        _stateMachine.Animator.SetFloat(SpeedHash, 1f, AnimatorDampTime, deltaTime);
    }

    public override void Exit()
    {
        _stateMachine.NavMeshAgent.ResetPath();
        _stateMachine.NavMeshAgent.velocity = Vector3.zero;
    }
}
