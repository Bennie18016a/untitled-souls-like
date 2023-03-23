using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawnState : PlayerBaseState
{
    public PlayerRespawnState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        _stateMachine.Health.SetMaxHealth(95 + _stateMachine.Stats.Health * 5);
        _stateMachine.Stamina.SetMaxStamina(40 + _stateMachine.Stats.Dexterity * 10);

        _stateMachine.Health.AddHealth(10000000);
        _stateMachine.Stamina.AddStamina(10000000);

        _stateMachine.UseQuickItem.ResetQuickItems();
        _stateMachine.transform.position = _stateMachine.respawnPoint;

        GameObject entities = GameObject.Find("--Entities--");

        foreach (Transform entity in entities.GetComponentInChildren<Transform>())
        {
            entity.position = entity.GetComponent<EnemyStateMachine>().startPos;
            entity.GetComponent<Health>().AddHealth(10000000);
            entity.gameObject.SetActive(true);
        }


        _stateMachine.SwitchState(new PlayerFreeLookState(_stateMachine));
    }

    public override void Tick(float deltaTime) { }

    public override void Exit() { }
}
