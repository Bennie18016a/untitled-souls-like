using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStrafeState : EnemyBaseState
{
    private readonly int LocomotionBlendTreeHash = Animator.StringToHash("Locomotion");
    private readonly int SpeedHash = Animator.StringToHash("Speed");
    private const float AnimatorDampTime = 0.1f;

    private Vector3 newPos;
    private Vector3 pos;
    public EnemyStrafeState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        _stateMachine.Animator.CrossFadeInFixedTime(LocomotionBlendTreeHash, 0.1f);

        newPos = _stateMachine.transform.position + Random.insideUnitSphere * _stateMachine.StrafeRange;
        if (newPos.y != 0) newPos.y = 0;
        while (Vector3.Distance(newPos, _stateMachine.Player.transform.position) <= 1f && Vector3.Distance(newPos, _stateMachine.Player.transform.position) >= 4f)
        {
            newPos = _stateMachine.transform.position + Random.insideUnitSphere * _stateMachine.StrafeRange;
            if (newPos.y != 0) newPos.y = 0;
        }
    }

    public override void Tick(float deltaTime)
    {
        if (!IsInStafeRange())
        {
            _stateMachine.SwitchState(new EnemyChasingState(_stateMachine));
        }
        if (IsInAttackRange())
        {
            RandomBeheaviour();
        }

        MoveToDest(newPos, deltaTime);
        FacePlayer();

        if (AtDest())
        {
            RandomBeheaviour();
        }

        pos = _stateMachine.transform.position;
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

        return Vector3.Distance(_stateMachine.transform.position, newPos) < 1.1f;
    }

    private void RandomBeheaviour()
    {
        int random = Random.Range(0, 3) + 1;

        if (random < 3)
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
