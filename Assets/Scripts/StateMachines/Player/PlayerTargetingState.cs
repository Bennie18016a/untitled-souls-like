using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    private readonly int TargetingBlendTreeHash = Animator.StringToHash("Targeting Blend Tree");
    private readonly int TargetingForwardHash = Animator.StringToHash("TargetingForward");
    private readonly int TargetingRightHash = Animator.StringToHash("TargetingRight");

    public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        _stateMachine.InputReader.CancelEvent += OnCancel;
        _stateMachine.InputReader.DodgeEvent += OnDodge;
        _stateMachine.InputReader.QuickItemEvent += OnQuickItem;


        _stateMachine.Animator.CrossFadeInFixedTime(TargetingBlendTreeHash, 0.1f);
    }

    public override void Tick(float deltaTime)
    {
        if (!_stateMachine.CanMove) { return; }

        #region Switch States
        if (_stateMachine.InputReader.IsAttacking && _stateMachine.Stamina.CanAction(_stateMachine.Attacks[0].StaminaCost))
        {
            _stateMachine.SwitchState(new PlayerAttackingState(_stateMachine, 0));
            return;
        }
        if (_stateMachine.InputReader.IsHeavyAttacking && _stateMachine.Stamina.CanAction(_stateMachine.Attacks[3].StaminaCost))
        {
            _stateMachine.SwitchState(new PlayerHeavyAttackingState(_stateMachine, 3));
            return;
        }
        if (_stateMachine.InputReader.IsBlocking)
        {
            _stateMachine.SwitchState(new PlayerBlockState(_stateMachine));
            return;
        }
        if (_stateMachine.Targeter.currentTarget == null)
        {
            _stateMachine.SwitchState(new PlayerFreeLookState(_stateMachine));
            return;
        }
        #endregion

        Vector3 movement = CalculateMovement();
        Move(movement * _stateMachine.TargetingMovementSpeed, deltaTime);
        FaceTarget();

        UpdateAnimatior(deltaTime);
    }

    private Vector3 CalculateMovement()
    {
        Vector3 movement = new Vector3();

        movement += _stateMachine.transform.right * _stateMachine.InputReader.MovementValue.x;
        movement += _stateMachine.transform.forward * _stateMachine.InputReader.MovementValue.y;

        return movement;
    }

    private void UpdateAnimatior(float deltaTime)
    {
        if (_stateMachine.InputReader.MovementValue.y == 0)
        {
            _stateMachine.Animator.SetFloat(TargetingForwardHash, 0, 0.01f, deltaTime);
        }
        else
        {
            // If MovementValue.Y is > 0 then value = 1f, else value = -1f
            float value = _stateMachine.InputReader.MovementValue.y > 0 ? 1f : -1f;
            _stateMachine.Animator.SetFloat(TargetingForwardHash, value, 0.01f, deltaTime);
        }

        if (_stateMachine.InputReader.MovementValue.x == 0)
        {
            _stateMachine.Animator.SetFloat(TargetingRightHash, 0, 0.01f, deltaTime);
        }
        else
        {
            float value = _stateMachine.InputReader.MovementValue.x > 0 ? 1f : -1f;
            _stateMachine.Animator.SetFloat(TargetingRightHash, value, 0.01f, deltaTime);
        }
    }

    #region Events
    private void OnCancel()
    {
        _stateMachine.Targeter.Cancel();
        _stateMachine.SwitchState(new PlayerFreeLookState(_stateMachine));
    }

    private void OnDodge()
    {
        if (_stateMachine.InputReader.MovementValue == Vector2.zero) return;
        if (!_stateMachine.Stamina.CanAction(_stateMachine.DodgeStaminaCost)) { return; }

        _stateMachine.SwitchState(new PlayerDodgeState(_stateMachine, _stateMachine.InputReader.MovementValue));
    }

    private void OnQuickItem()
    {
        _stateMachine.UseQuickItem.UseItem(_stateMachine);
    }
    #endregion

    public override void Exit()
    {
        _stateMachine.InputReader.CancelEvent -= OnCancel;
        _stateMachine.InputReader.DodgeEvent -= OnDodge;
        _stateMachine.InputReader.QuickItemEvent -= OnQuickItem;
    }
}