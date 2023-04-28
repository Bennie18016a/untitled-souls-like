using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class Beacons : MonoBehaviour
{
    public InputReader ir;
    public GameObject MainMenu, LevelUp;
    bool inLevelUp;

    //Subcribes and Unsubscribes the function "TryAddLevel" on Enable and Disable
    private void OnEnable()
    {
        ir.LevelUpAction += TryAddLevel;
    }

    private void OnDisable()
    {
        ir.LevelUpAction += TryAddLevel;
    }

    public void TryAddLevel()
    {
        /*To make sure the player CAN level up:
        Gets the player and checks if they are in the level up menu, if not return,
        Checks to see if player has enough to level up, if not return,
        Checks to see if player is level 20, if they are return*/
        GameObject player = GameObject.FindWithTag("Player");
        if (!inLevelUp) return;
        if (player.GetComponent<KeyInventory>().currency < player.GetComponent<KeyInventory>().nextLevelUpCost) return;
        if (player.GetComponent<KeyInventory>().level == 20) return;

        /*To add the level to the correct stat:
        Store the name of currently selected button and check it in a switch statement
        If it has one of the stats it will add to that stat, otherwise it debugs the name of the selected button*/
        string name = EventSystem.current.currentSelectedGameObject.name;
        switch (name.ToLower())
        {
            case "health":
                player.GetComponent<Stats>().Health++;
                break;
            case "magic":
                player.GetComponent<Stats>().Magic++;
                break;
            case "strength":
                player.GetComponent<Stats>().Strength++;
                break;
            case "dexterity":
                player.GetComponent<Stats>().Dexterity++;
                break;
            case "knowledge":
                player.GetComponent<Stats>().Knowledge++;
                break;
            default:
                Debug.Log("Error... " + name);
                break;
        }
        /*Once the leveling has happened:
        Adds 1 to the level,
        Minuses the currency of how much it cost,
        set bool "hasSpent" to true to add dynamic currency dropping*/

        player.GetComponent<KeyInventory>().level++;
        player.GetComponent<KeyInventory>().currency -= player.GetComponent<KeyInventory>().nextLevelUpCost;
        player.GetComponent<KeyInventory>().hasSpent = true;
    }

    private void Update()
    {
        //Bool "inLevelUp" is true when "LevelUp" gameobject is active
        inLevelUp = LevelUp.activeInHierarchy;
    }

    public void CloseMenu()
    {
        //When closing the menu we switch the inputreader back to "Player" (Gameplay) controls and sets the menu to false (invisible)
        ir.GoToUI();
        gameObject.SetActive(false);
    }

    public void LevelUpFunc()
    {
        //Turns the main menu to false (invisible), level up menu to true (visible) and the currently selected button to "Health" level up button
        MainMenu.SetActive(false);
        LevelUp.SetActive(true);
        EventSystem.current.SetSelectedGameObject(LevelUp.transform.GetChild(1).GetChild(0).gameObject);
    }
}
