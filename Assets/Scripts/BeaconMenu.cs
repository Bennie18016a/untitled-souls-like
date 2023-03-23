using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeaconMenu : MonoBehaviour
{
    public GameObject beaconMenu;
    public InputReader ir;
    public void OpenMenu()
    {
        beaconMenu.SetActive(true);
        ir.GoToUI();
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStateMachine>().respawnPoint = transform.position;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStateMachine>()
        .SwitchState(new PlayerRespawnState(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStateMachine>()));
    }
}
