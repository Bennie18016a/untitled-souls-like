using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgeState : PlayerBaseState
{
    private readonly int DodgeBlendTreeHash = Animator.StringToHash("Dodge Blend Tree");
    private readonly int DodgeForwardHash = Animator.StringToHash("DodgeForward");
    private readonly int DodgeRightHash = Animator.StringToHash("DodgeRight");

    private Vector3 dodgeDirectionInput;
    private float remainingDodgeTime;
    private const float CrossFadeDuration = 0.1f;

    public PlayerDodgeState(PlayerStateMachine _stateMachine, Vector3 dodgeDirectionInput) : base(_stateMachine)
    {
        this.dodgeDirectionInput = dodgeDirectionInput;
    }

    public override void Enter()
    {
        remainingDodgeTime = _stateMachine.DodgeDuration;

        _stateMachine.Animator.SetFloat(DodgeForwardHash, dodgeDirectionInput.y);
        _stateMachine.Animator.SetFloat(DodgeRightHash, dodgeDirectionInput.x);
        _stateMachine.Animator.CrossFadeInFixedTime(DodgeBlendTreeHash, CrossFadeDuration);


        _stateMachine.Health.Invunerable(true);

    }

    public override void Tick(float deltaTime)
    {
        Vector3 movement = new Vector3();

        movement += _stateMachine.transform.right * dodgeDirectionInput.x * _stateMachine.DodgeLength / _stateMachine.DodgeDuration;
        movement += _stateMachine.transform.forward * dodgeDirectionInput.y * _stateMachine.DodgeLength / _stateMachine.DodgeDuration;

        Move(movement, deltaTime);

        FaceTarget();

        remainingDodgeTime -= deltaTime;

        if (remainingDodgeTime <= 0f)
        {
            _stateMachine.SwitchState(new PlayerTargetingState(_stateMachine));
        }

    }

    public override void Exit()
    {
        _stateMachine.Health.Invunerable(false);
    }
}
