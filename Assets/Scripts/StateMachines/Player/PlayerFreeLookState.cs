using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
    private readonly int FreeLookBlendTreeHash = Animator.StringToHash("Freelook Blend Tree");
    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        _stateMachine.InputReader.TargetEvent += OnTarget;

        _stateMachine.Animator.CrossFadeInFixedTime(FreeLookBlendTreeHash, 0.1f);
    }

    public override void Tick(float deltaTime)
    {
        if (!_stateMachine.CanMove) { return; }
        Vector3 movement = CalculateMovement();

        Move(movement * _stateMachine.FreeLookMovementSpeed, deltaTime);
        FaceMovementDirection(movement, deltaTime);
    }
    private void FaceMovementDirection(Vector3 movement, float deltaTime)
    {
        _stateMachine.transform.rotation = Quaternion.Lerp(
            _stateMachine.transform.rotation,
            Quaternion.LookRotation(movement),
            deltaTime * _stateMachine.RotationDamping
        );
    }

    private Vector3 CalculateMovement()
    {
        Vector3 forward = _stateMachine.MainCameraTransform.forward;
        Vector3 right = _stateMachine.MainCameraTransform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        return forward * _stateMachine.InputReader.MovementValue.y + right * _stateMachine.InputReader.MovementValue.x;
    }

    private void OnTarget()
    {
        if (!_stateMachine.Targeter.SelectTarget()) return;
        _stateMachine.SwitchState(new PlayerTargetingState(_stateMachine));
    }
    public override void Exit()
    {
        _stateMachine.InputReader.TargetEvent -= OnTarget;
    }
}



