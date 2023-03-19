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
        int RandomAttack = Random.Range(0, 3) + 1;

        switch (RandomAttack)
        {
            case 1:
                Attack = "Punch";
                break;
            case 2:
                Attack = "Kick";
                break;
            case 3:
                Attack = "Grab";
                break;
        }

        Debug.Log(Attack);
    }

    public override void Tick(float deltaTime)
    {
        MoveToPlayer(deltaTime);
        FacePlayer();

        switch (Attack)
        {
            case "Punch":
                if (!IsInPunchRange() || !IsInfront()) return;
                _stateMachine.SwitchState(new KingPunchState(_stateMachine, Random.Range(0, 2)));
                break;
            case "Kick":
                if (!IsInKickRange() || !IsInfront()) return;
                _stateMachine.SwitchState(new KingKickState(_stateMachine));
                break;
            case "Grab":
                if (!IsInGrabRange() || !IsInfront()) return;
                _stateMachine.SwitchState(new KingWaitToThrowState(_stateMachine));
                break;
        }

        if (AttackTime >= _stateMachine.MaxAttackAttemptTime)
        {
            _stateMachine.SwitchState(new BossChaseState(_stateMachine));
        }

        AttackTime += 1 * Time.deltaTime;
    }

    public override void Exit()
    {
        _stateMachine.NavMeshAgent.ResetPath();
        _stateMachine.NavMeshAgent.velocity = Vector3.zero;
    }
}
