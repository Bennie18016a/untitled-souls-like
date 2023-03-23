using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeaconMenu : MonoBehaviour
{
    public void OpenMenu()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStateMachine>().respawnPoint = transform.position;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStateMachine>()
        .SwitchState(new PlayerRespawnState(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStateMachine>()));
    }
}
