using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockState : PlayerBaseState
{
    private readonly int FreeBlockBlendTree = Animator.StringToHash("FreeBlock Blend Tree");
    private readonly int TargetBlockBlendTree = Animator.StringToHash("TargetBlock Blend Tree");
    private readonly int FreeBlockHash = Animator.StringToHash("FreeBlock");
    private readonly int TargetFoward = Animator.StringToHash("TargetForward");
    private readonly int TargetRight = Animator.StringToHash("TargetRight");
    private const float AnimatorDampTime = 0.1f;
    private string StartcurrentState;
    private string currentState;


    public PlayerBlockState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        _stateMachine.Health.Invunerable(true);
        _stateMachine.Stamina.SetNaturalStamina(false);
        _stateMachine.InputReader.QuickItemEvent += OnQuickItem;
        _stateMachine.InputReader.DodgeEvent += OnDodge;

        if (_stateMachine.Targeter.currentTarget == null)
        {
            _stateMachine.Animator.CrossFadeInFixedTime(FreeBlockBlendTree, 0.1f);
            StartcurrentState = "Freelook";
            currentState = "Freelook";
        }
        else
        {
            _stateMachine.Animator.CrossFadeInFixedTime(TargetBlockBlendTree, 0.1f);
            StartcurrentState = "Target";
            currentState = "Target";
        }
    }

    public override void Tick(float deltaTime)
    {
        if (!_stateMachine.CanMove) { return; }
        #region Switch States
        if (_stateMachine.InputReader.IsAttacking)
        {
            if (!_stateMachine.Stamina.CanAction(_stateMachine.Attacks[0].StaminaCost)) { return; }
            _stateMachine.SwitchState(new PlayerAttackingState(_stateMachine, 0));
            return;
        }
        if (_stateMachine.InputReader.IsHeavyAttacking && _stateMachine.Stamina.CanAction(_stateMachine.Attacks[3].StaminaCost))
        {
            _stateMachine.SwitchState(new PlayerHeavyAttackingState(_stateMachine, 3));
            return;
        }
        if (!_stateMachine.InputReader.IsBlocking)
        {
            ReturnToLocomotion();
        }
        #endregion

        if (_stateMachine.Targeter.currentTarget == null)
        {
            currentState = "Freelook";
        }
        else
        {
            currentState = "Target";
        }

        #region Movement
        if (currentState == "Freelook")
        {
            if (StartcurrentState != currentState)
            {
                _stateMachine.Animator.CrossFadeInFixedTime(FreeBlockBlendTree, 0.1f);
                StartcurrentState = "Freelook";
                currentState = "Freelook";
            }
            Vector3 movement = CalculateMovementFree();
            Move(movement * _stateMachine.BlockMovementSpeed, deltaTime);

            #region Animations
            if (_stateMachine.InputReader.MovementValue == Vector2.zero)
            {
                _stateMachine.Animator.SetFloat(FreeBlockHash, 0, AnimatorDampTime, deltaTime);
                return;
            }
            _stateMachine.Animator.SetFloat(FreeBlockHash, 1, AnimatorDampTime, deltaTime);
            #endregion
            FaceMovementDirection(movement, deltaTime);
        }
        else
        {
            if (StartcurrentState != currentState)
            {
                _stateMachine.Animator.CrossFadeInFixedTime(TargetBlockBlendTree, 0.1f);
                StartcurrentState = "Target";
                currentState = "Target";
            }
            Vector3 movement = CalculateMovementTarget();
            Move(movement * _stateMachine.BlockMovementSpeed, deltaTime);

            FaceTarget();
            UpdateAnimatior(deltaTime);
        }
        #endregion
    }

    private void UpdateAnimatior(float deltaTime)
    {
        if (_stateMachine.InputReader.MovementValue.y == 0)
        {
            _stateMachine.Animator.SetFloat(TargetFoward, 0, 0.01f, deltaTime);
        }
        else
        {
            float value = _stateMachine.InputReader.MovementValue.y > 0 ? 1f : -1f;
            _stateMachine.Animator.SetFloat(TargetFoward, value, 0.01f, deltaTime);
        }

        if (_stateMachine.InputReader.MovementValue.x == 0)
        {
            _stateMachine.Animator.SetFloat(TargetRight, 0, 0.01f, deltaTime);
        }
        else
        {
            float value = _stateMachine.InputReader.MovementValue.x > 0 ? 1f : -1f;
            _stateMachine.Animator.SetFloat(TargetRight, value, 0.01f, deltaTime);
        }
    }

    private void FaceMovementDirection(Vector3 movement, float deltaTime)
    {
        _stateMachine.transform.rotation = Quaternion.Lerp(
            _stateMachine.transform.rotation,
            Quaternion.LookRotation(movement),
            deltaTime * _stateMachine.RotationDamping
        );
    }

    private Vector3 CalculateMovementTarget()
    {
        Vector3 movement = new Vector3();

        movement += _stateMachine.transform.right * _stateMachine.InputReader.MovementValue.x;
        movement += _stateMachine.transform.forward * _stateMachine.InputReader.MovementValue.y;

        return movement;
    }

    private Vector3 CalculateMovementFree()
    {
        Vector3 forward = _stateMachine.MainCameraTransform.forward;
        Vector3 right = _stateMachine.MainCameraTransform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        return forward * _stateMachine.InputReader.MovementValue.y + right * _stateMachine.InputReader.MovementValue.x;
    }

    private void OnQuickItem()
    {
        _stateMachine.UseQuickItem.UseItem(_stateMachine);
    }

    private void OnDodge()
    {
        if (_stateMachine.InputReader.MovementValue == Vector2.zero) return;
        if (!_stateMachine.Stamina.CanAction(_stateMachine.DodgeStaminaCost)) { return; }

        _stateMachine.SwitchState(new PlayerDodgeState(_stateMachine, _stateMachine.InputReader.MovementValue));
    }

    public override void Exit()
    {
        _stateMachine.Health.Invunerable(false);
        _stateMachine.Stamina.SetNaturalStamina(true);
        _stateMachine.InputReader.QuickItemEvent -= OnQuickItem;
        _stateMachine.InputReader.DodgeEvent -= OnDodge;
    }
}
