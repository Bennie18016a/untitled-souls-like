using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Interaction : MonoBehaviour, IDataPersistence
{
    public enum Type { door, keydoor, onesidedoor, fogwall, beacon, chest }
    public Type _type;
    public InputReader ir;
    public TextMenu textMenu;
    private GameObject player;
    public float range = 3f;
    public TMP_Text text;
    private Controls _controls;

    //On enable/disable subcribes and unsubscribes to the function "Interact" for the InteractAction
    private void OnEnable()
    {
        ir.InteractAction += Interact;
    }

    private void OnDisable()
    {
        ir.InteractAction -= Interact;
    }
    /*Once something has been used (mostly doors) we destroy the gameobject. When it is destroyed I:
    Unsubscribe from the action like enable and disable,
    Sets the text gameobject to be false (invisible),
    Set the text text box to be null*/
    private void OnDestroy()
    {
        ir.InteractAction -= Interact;
        text.transform.parent.gameObject.SetActive(false);
        text.text = null;
    }
    //On Start I set the player variable to be the player and controls variable to be a new controls scheme. This is deactivated but allows me to get what button does what
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
        /*If the player is in range I:
        Set the text to be true,
        Set the text of the text to be somethign depending on the type of interaction this is. I do this using a switch statement*/
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
        /*If the player isn't in range, I:
        Set the text gameobject to be false (invisible)
        Set the texts text box to be null*/
        else if (Vector3.Distance(transform.position, player.transform.position) < range + 1.5f || textMenu.InMenu())
        {
            text.transform.parent.gameObject.SetActive(false);
            text.text = null;
        }
    }

    public void Interact()
    {
        //If the player isnt in range, return
        bool inRange = Vector3.Distance(transform.position, player.transform.position) > range;
        if (inRange) return;

        /*If the ineraction is a door:
        Set the text to false (invisible) and text bos to null
        Set that it has been activated
        Sets the gameobject to false (invisible)
        
        If it is a keydoor, we do the same but if they dont have the key return
        
        If it is a onesided door, check they are the right side. If not return.
        We check by using the "IsInArea" function in the "CheckInteraction" script
        
        If it is a fogwall, Check if they are the right side like onesided door.
        Set the players position to be the other side of the door,
        Activates the boss,
        Disables the script to make sure you cant get back out.
        
        If it is a beacon, I enable the beacon menu and set the player to the respawn state.
        
        Finally if its a chest, I open the chest then make this script disables*/

        switch (_type)
        {
            case Type.door:
                text.transform.parent.gameObject.SetActive(false);
                text.text = null;
                activated = true;
                gameObject.SetActive(false);
                break;
            case Type.keydoor:
                if (!GetComponent<CheckInteraction>().HasKey(transform.name))
                {
                    textMenu.OpenMenu("Key required");
                    return;
                }
                text.transform.parent.gameObject.SetActive(false);
                text.text = null;
                activated = true;
                gameObject.SetActive(false);
                break;
            case Type.onesidedoor:
                if (!GetComponentInChildren<CheckInteraction>().IsInArea())
                {
                    textMenu.OpenMenu("This door is opened elsewhere");
                    break;
                }
                text.transform.parent.gameObject.SetActive(false);
                text.text = null;
                activated = true;
                gameObject.SetActive(false);
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
                activated = true;
                this.enabled = false;
                break;
        }
    }

    //Saving and Loading wether the interaction has been interacted with before.

    [SerializeField] private string id;
    public bool activated;

    [ContextMenu("Generate GUID for Enemies")]
    private void GenerateID()
    {
        id = System.Guid.NewGuid().ToString();
    }

    public void LoadData(GameData data)
    {
        data.interactions.TryGetValue(id, out activated);

        if (!activated) return;

        switch (_type)
        {
            default:
                gameObject.SetActive(false);
                break;
            case Type.fogwall:
                return;
            case Type.beacon:
                return;
            case Type.chest:
                GetComponent<Animator>().SetTrigger("Open");
                this.enabled = false;
                break;
        }
    }

    public void SaveData(ref GameData data)
    {
        if (data.interactions.ContainsKey(id)) data.interactions.Remove(id);

        data.interactions.Add(id, activated);
    }
}
