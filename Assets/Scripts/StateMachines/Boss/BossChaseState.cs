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

        _stateMachine.AttackCooldown += 1 * deltaTime;
        Debug.Log(_stateMachine.AttackCooldown);
        int random = Random.Range(0, 4);
        if (random == 0)
        {
            switch (_stateMachine.thisBoss)
            {
                case BossStateMachine.Boss.Goblin_King:
                    _stateMachine.SwitchState(new KingAttackingState(_stateMachine));
                    break;
                case BossStateMachine.Boss.Fallen_Witch:
                    if (_stateMachine.AttackCooldown < _stateMachine.AttackCooldownMax) break;
                    _stateMachine.SwitchState(new WitchAttackingState(_stateMachine));
                    break;
            }
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
