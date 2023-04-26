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
        GameObject player = GameObject.FindWithTag("Player");
        if (!inLevelUp) return;
        if (player.GetComponent<KeyInventory>().currency < player.GetComponent<KeyInventory>().nextLevelUpCost) return;
        if(player.GetComponent<KeyInventory>().level == 20) return;

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

        player.GetComponent<KeyInventory>().currency -= player.GetComponent<KeyInventory>().nextLevelUpCost;
        player.GetComponent<KeyInventory>().hasSpent = true;
    }

    private void Update()
    {
        inLevelUp = LevelUp.activeInHierarchy;
    }
    public void CloseMenu()
    {
        ir.GoToUI();
        gameObject.SetActive(false);
    }

    public void LevelUpFunc()
    {
        MainMenu.SetActive(false);
        LevelUp.SetActive(true);
        EventSystem.current.SetSelectedGameObject(LevelUp.transform.GetChild(1).GetChild(0).gameObject);
    }
}
