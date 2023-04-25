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

        GameObject areas = GameObject.Find("--Entities--");

        foreach (Transform area in areas.GetComponentInChildren<Transform>())
        {
            EnemyStateMachine[] entities = area.GetComponentsInChildren<EnemyStateMachine>(true);

            foreach (EnemyStateMachine entity in entities)
            {
                entity.transform.position = entity.GetComponent<EnemyStateMachine>().startPos;
                entity.dead = false;
                entity.Health.AddHealth(10000000);
                entity.gameObject.SetActive(true);
            }
        }

        GameObject bosses = GameObject.Find("--Bosses--");

        foreach (Transform boss in bosses.GetComponentInChildren<Transform>())
        {
            boss.GetComponent<BossStateMachine>().Active = false;
            boss.position = boss.GetComponent<BossStateMachine>().startPos;
            boss.GetComponent<Health>().AddHealth(10000000);
        }

        GameObject fogwalls = GameObject.Find("--FogWalls--");

        foreach (Transform fogwall in fogwalls.GetComponentInChildren<Transform>())
        {
            if (fogwall.TryGetComponent<Interaction>(out Interaction interaction))
            {
                interaction.enabled = true;
            }
        }
        _stateMachine.SwitchState(new PlayerFreeLookState(_stateMachine));
    }

    public override void Tick(float deltaTime) { }

    public override void Exit() { }
}
