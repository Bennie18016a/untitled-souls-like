using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawnState : PlayerBaseState
{
    public PlayerRespawnState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        _stateMachine.Health.SetMaxHealth(_stateMachine.Stats.Health * .5f);
        _stateMachine.Stamina.SetMaxStamina(_stateMachine.Stats.Dexterity * .5f);
        
        _stateMachine.SwitchState(new PlayerFreeLookState(_stateMachine));
    }

    public override void Tick(float deltaTime)
    {

    }

    public override void Exit() { }
}
