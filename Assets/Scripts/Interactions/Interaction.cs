using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Interaction : MonoBehaviour
{
    public enum Type { door, keydoor, onesidedoor, fogwall, beacon, chest }
    public Type _type;
    public InputReader ir;
    public TextMenu textMenu;
    private GameObject player;
    public float range = 3f;
    public TMP_Text text;
    private Controls _controls;

    private void OnEnable()
    {
        ir.InteractAction += Interact;
    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _controls = new Controls();
    }

    private void Update()
    {
        ShowText();
    }

    private void ShowText()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < range)
        {
            text.transform.parent.gameObject.SetActive(true);
            if (_type == Type.fogwall)
            {
                text.text = string.Format("{0}: Enter the fog", _controls.Player.Interact.GetBindingDisplayString());
            }
            else if (_type == Type.beacon)
            {
                text.text = string.Format("{0}: Rest at beacon", _controls.Player.Interact.GetBindingDisplayString());
            }
            else if (_type == Type.chest)
            {
                text.text = string.Format("{0}: Open Chest", _controls.Player.Interact.GetBindingDisplayString());
            }
            else
            {
                text.text = string.Format("{0}: Open Door", _controls.Player.Interact.GetBindingDisplayString());
            }
        }
        else if (Vector3.Distance(transform.position, player.transform.position) < range + 1.5f || textMenu.InMenu())
        {
            text.transform.parent.gameObject.SetActive(false);
            text.text = null;
        }
    }

    public void Interact()
    {
        bool inRange = Vector3.Distance(transform.position, player.transform.position) > range;
        if (inRange) return;

        switch (_type)
        {
            case Type.door:
                Destroy(gameObject);
                break;
            case Type.keydoor:
                if (!GetComponent<CheckInteraction>().HasKey(transform.name))
                {
                    textMenu.OpenMenu("Key required");
                    return;
                }
                Destroy(gameObject);
                break;
            case Type.onesidedoor:
                if (!GetComponentInChildren<CheckInteraction>().IsInArea())
                {
                    textMenu.OpenMenu("This door is opened elsewhere");
                    break;
                }
                Destroy(gameObject);
                break;
            case Type.fogwall:
                if (!transform.GetChild(1).GetComponent<CheckInteraction>().IsInArea()) return;
                player.transform.position = transform.GetChild(0).transform.position;
                GetComponent<FogWall>().boss.GetComponent<BossStateMachine>().Active = true;
                text.transform.parent.gameObject.SetActive(false);
                text.text = null;
                this.enabled = false;
                break;
            case Type.beacon:
                player.GetComponent<PlayerStateMachine>().SwitchState(new PlayerRespawnState(player.GetComponent<PlayerStateMachine>()));
                GetComponent<BeaconMenu>().OpenMenu();
                break;
            case Type.chest:
                if (TryGetComponent<ChestLoot>(out ChestLoot chestLoot)) chestLoot.OpenChest();
                text.transform.parent.gameObject.SetActive(false);
                text.text = null;
                Destroy(this);
                break;
        }
    }

    private void OnDisable()
    {
        ir.InteractAction -= Interact;
    }

    private void OnDestroy()
    {
        ir.InteractAction -= Interact;
        text.transform.parent.gameObject.SetActive(false);
        text.text = null;
    }
}
