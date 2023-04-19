using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchAttackingState : BossBaseState
{
    private float AttackTime;
    private string Attack;
    public WitchAttackingState(BossStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        if (_stateMachine.Health.GetHealth() < (_stateMachine.Health.GetMaxHealth() * 0.75))
        {
            _stateMachine.Phase = 2;
        }

        int RandomAttack = Random.Range(0, 3) + 1;

        switch (RandomAttack)
        {
            case 1:
                if (_stateMachine.Phase != 2) _stateMachine.SwitchState(new WitchAttackingState(_stateMachine));
                Attack = "Tread";
                break;
            case 2:
                Attack = "Knife";
                break;
            case 3:
                if (_stateMachine.Phase != 2) _stateMachine.SwitchState(new WitchAttackingState(_stateMachine));
                Attack = "Life";
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
            case "Tread":
                if (!IsInfront() || !IsInTreadRange()) break;
                _stateMachine.SwitchState(new WitchJumpState(_stateMachine, Attack));
                break;
            case "Knife":
                if (!IsInfront() || !IsInKnifeRange()) break;
                _stateMachine.SwitchState(new WitchKnifeAttackState(_stateMachine));
                break;
            case "Life":
                if (!IsInfront() || !IsInLifeRange()) break;
                _stateMachine.SwitchState(new WitchLifeAttackState(_stateMachine));
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
        _stateMachine.AttackCooldown = 0;
    }
}
