using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    private float AttackTime;
    public EnemyAttackState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter() { }

    public override void Tick(float deltaTime)
    {
        if (IsInAttackRange())
        {
            _stateMachine.SwitchState(new EnemyAttackingState(_stateMachine));
        }
        if (AttackTime >= _stateMachine.MaxAttackAttemptTime)
        {
            if (IsInStafeRange())
            {
                _stateMachine.SwitchState(new EnemyStrafeState(_stateMachine));
            }
            else if (IsInChaseRange())
            {
                _stateMachine.SwitchState(new EnemyChasingState(_stateMachine));
            }
            else
            {
                _stateMachine.SwitchState(new EnemyIdleState(_stateMachine));
            }
        }

        MoveToPlayer(deltaTime);
        FacePlayer();

        AttackTime += 1 * Time.deltaTime;
    }

    public override void Exit()
    {
        _stateMachine.NavMeshAgent.ResetPath();
        _stateMachine.NavMeshAgent.velocity = Vector3.zero;
    }
}
