using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChaseState : BossBaseState
{
    private readonly int LocomotionBlendTreeHash = Animator.StringToHash("Locomotion");
    private readonly int SpeedHash = Animator.StringToHash("Speed");
    private const float AnimatorDampTime = 0.1f;

    private Vector3 randomDirection;
    private bool isWalkingRandomly;

    public BossChaseState(BossStateMachine stateMachine) : base(stateMachine) { }
    public override void Enter()
    {
        _stateMachine.Animator.CrossFadeInFixedTime(LocomotionBlendTreeHash, 0.1f);
    }

    public override void Tick(float deltaTime)
    {
        if (!_stateMachine.Active) { return; }

        _stateMachine.AttackCooldown += 1 * deltaTime;

        int random = Random.Range(0, 4);
        if (random == 0)
        {
            switch (_stateMachine.thisBoss)
            {
                case BossStateMachine.Boss.Goblin_King:
                    _stateMachine.SwitchState(new KingAttackingState(_stateMachine));
                    break;
                case BossStateMachine.Boss.Fallen_Witch:
                    if (_stateMachine.AttackCooldown < _stateMachine.AttackCooldownMax) break;
                    _stateMachine.SwitchState(new WitchAttackingState(_stateMachine));
                    break;
            }
        }

        if (_stateMachine.MinDistanceFromPlayer != 0 || _stateMachine.MaxDistanceFromPlayer != 0)
        {
            float Distance = Vector3.Distance(_stateMachine.transform.position, _stateMachine.Player.transform.position);

            if (Distance < _stateMachine.MinDistanceFromPlayer)
            {
                Vector3 moveDirection = _stateMachine.transform.position - _stateMachine.Player.transform.position;
                moveDirection.y = 0;
                moveDirection = moveDirection.normalized * _stateMachine.MovementSpeed * deltaTime;
                _stateMachine.CharacterController.Move(moveDirection);

                if (Distance > _stateMachine.MaxDistanceFromPlayer)
                {
                    Vector3 clampPosition = _stateMachine.Player.transform.position - moveDirection.normalized * _stateMachine.MaxDistanceFromPlayer;
                    _stateMachine.CharacterController.Move(clampPosition - _stateMachine.transform.position);
                }

                isWalkingRandomly = false;
            }
            else if (Distance > _stateMachine.MaxDistanceFromPlayer)
            {
                Vector3 moveDirection = _stateMachine.Player.transform.position - _stateMachine.transform.position;
                moveDirection.y = 0; // ensure the enemy doesn't move up or down
                moveDirection = moveDirection.normalized * _stateMachine.MovementSpeed * Time.deltaTime;
                _stateMachine.CharacterController.Move(moveDirection);

                isWalkingRandomly = false;
            }
            else
            {
                if (!isWalkingRandomly)
                {
                    randomDirection = Random.insideUnitCircle.normalized * _stateMachine.MovementSpeed;
                    randomDirection.y = -7.532f;
                    isWalkingRandomly = true;
                }

                Vector3 moveDirection = _stateMachine.transform.position + randomDirection * deltaTime;
                _stateMachine.CharacterController.Move(moveDirection - _stateMachine.transform.position);
            }
        }
        else
        {
            MoveToPlayer(deltaTime);
        }

        FacePlayer();

        _stateMachine.Animator.SetFloat(SpeedHash, 1f, AnimatorDampTime, deltaTime);
    }

    public override void Exit()
    {
        _stateMachine.NavMeshAgent.ResetPath();
        _stateMachine.NavMeshAgent.velocity = Vector3.zero;
    }
}
