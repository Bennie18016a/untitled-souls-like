using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStrafeState : EnemyBaseState
{
    private Vector3 newPos;
    public EnemyStrafeState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        newPos = _stateMachine.transform.position + Random.insideUnitSphere * _stateMachine.StrafeRange;
        if (newPos.y != 0) newPos.y = 0;
        while (Vector3.Distance(newPos, _stateMachine.Player.transform.position) <= 1f && Vector3.Distance(newPos, _stateMachine.Player.transform.position) >= 4f)
        {
            newPos = _stateMachine.transform.position + Random.insideUnitSphere * _stateMachine.StrafeRange;
            if (newPos.y != 0) newPos.y = 0;
        }

        Debug.Log(newPos);
    }

    public override void Tick(float deltaTime)
    {
        MoveToDest(newPos, deltaTime);
        FacePlayer();

        if (AtDest())
        {
            RandomBeheaviour();
        }
    }

    private void MoveToDest(Vector3 pos, float deltaTime)
    {
        _stateMachine.NavMeshAgent.nextPosition = _stateMachine.transform.position;
        if (!_stateMachine.NavMeshAgent.isOnNavMesh) { return; }

        _stateMachine.NavMeshAgent.destination = newPos;

        Move(_stateMachine.NavMeshAgent.desiredVelocity.normalized * _stateMachine.MovementSpeed, deltaTime);
    }

    private bool AtDest()
    {
        if (newPos == null) return false;

        return Vector3.Distance(_stateMachine.transform.position, newPos) < 1;
    }

    private void RandomBeheaviour()
    {
        int random = Random.Range(0, 2) + 1;
        Debug.Log(random);

        if (random == 1)
        {
            _stateMachine.SwitchState(new EnemyStrafeState(_stateMachine));
        }
        else
        {
            _stateMachine.SwitchState(new EnemyAttackState(_stateMachine));
        }
    }

    public override void Exit()
    {
        _stateMachine.NavMeshAgent.ResetPath();
        _stateMachine.NavMeshAgent.velocity = Vector3.zero;
    }
}
