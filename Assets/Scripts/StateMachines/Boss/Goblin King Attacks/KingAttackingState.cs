using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingAttackingState : BossBaseState
{
    private float AttackTime;
    private string Attack;
    public KingAttackingState(BossStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        int RandomAttack = Random.Range(0, 1) + 1;

        switch (RandomAttack)
        {
            case 1:
                Attack = "Punch";
                break;
        }
    }

    public override void Tick(float deltaTime)
    {
        if (Attack == "Punch" && IsInPunchRange())
        {
            _stateMachine.SwitchState(new KingPunchState(_stateMachine, Random.Range(0, 2)));
        }

        if (AttackTime >= _stateMachine.MaxAttackAttemptTime)
        {
            _stateMachine.SwitchState(new BossChaseState(_stateMachine));
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
