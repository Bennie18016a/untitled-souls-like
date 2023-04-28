using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BeaconMenu : MonoBehaviour, IDataPersistence
{
    public GameObject beaconMenu;
    public InputReader ir;

    [SerializeField] private string id;
    private bool active;

    [ContextMenu("Generate GUID for Enemies")]
    private void GenerateID()
    {
        id = System.Guid.NewGuid().ToString();
    }

    public void OpenMenu()
    {
        /*When opening the menu:
        Sets the menu to be visible,
        Sets the player repsawn point to the menu,
        Changes the player to be in the "Respawn" state,
        Makes the currently selected button to be "Exit Beacon" in the beacon menu,
        Switches the inputreader to read "UI" input only*/

        beaconMenu.SetActive(true);
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStateMachine>().respawnPoint = transform.position;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStateMachine>()
        .SwitchState(new PlayerRespawnState(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStateMachine>()));
        GameObject _active = beaconMenu.transform.GetChild(0).GetChild(1).GetChild(0).gameObject;
        EventSystem.current.SetSelectedGameObject(_active);
        ir.GoToUI();

        //Sets all of the other beacons active to false and makes this beacon be active

        GameObject beacons = GameObject.Find("--Beacons--");

        foreach (Transform beacon in beacons.GetComponentInChildren<Transform>())
        {
            if (beacon == transform) continue;

            beacon.GetComponent<BeaconMenu>().active = false;
        }
        active = true;
    }

    //Saving and Loading

    public void LoadData(GameData data)
    {
        data.beacons.TryGetValue(id, out active);

        if (active) GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStateMachine>().respawnPoint = transform.position;
    }

    public void SaveData(ref GameData data)
    {
        if (data.beacons.ContainsKey(id))
        {
            data.beacons.Remove(id);
        }

        data.beacons.Add(id, active);
    }
}
