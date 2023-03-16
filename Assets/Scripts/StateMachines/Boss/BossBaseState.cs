using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossBaseState : State
{
    protected BossStateMachine _stateMachine;

    public BossBaseState(BossStateMachine stateMachine)
    {
        this._stateMachine = stateMachine;
    }

    protected void Move(float deltaTime)
    {
        Move(Vector3.zero, deltaTime);
    }

    protected void Move(Vector3 motion, float deltaTime)
    {
        _stateMachine.CharacterController.Move((motion + _stateMachine.ForceReciver.movement) * deltaTime);
    }

    protected void MoveToPlayer(float deltaTime)
    {
        _stateMachine.NavMeshAgent.nextPosition = _stateMachine.transform.position;
        if (!_stateMachine.NavMeshAgent.isOnNavMesh) { return; }

        _stateMachine.NavMeshAgent.destination = _stateMachine.Player.transform.position;

        Move(_stateMachine.NavMeshAgent.desiredVelocity.normalized * _stateMachine.MovementSpeed, deltaTime);
    }

    protected void FacePlayer()
    {
        if (_stateMachine.Player == null) return;

        Vector3 lookPos = _stateMachine.Player.transform.position - _stateMachine.transform.position;
        lookPos.y = 0;

        _stateMachine.transform.rotation = Quaternion.LookRotation(lookPos);
    }

    #region Distance Checks
    protected bool IsInStafeRange()
    {
        float playerDistanceSqr = (_stateMachine.Player.transform.position - _stateMachine.transform.position).sqrMagnitude;

        return playerDistanceSqr <= _stateMachine.StrafeDistance * _stateMachine.StrafeDistance;
    }

    protected bool IsInPunchRange()
    {
        float playerDistanceSqr = (_stateMachine.Player.transform.position - _stateMachine.transform.position).sqrMagnitude;

        return playerDistanceSqr <= _stateMachine.PunchDistance * _stateMachine.PunchDistance;
    }
    #endregion
}
