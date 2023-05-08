using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TabButtons : MonoBehaviour
{
    public GameObject tabsMenu;
    public void ActivateUI(GameObject UI)
    {
        UI.SetActive(true);

        switch (UI.transform.name)
        {
            case "Item Inventory":
                EventSystem.current.SetSelectedGameObject(UI.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).gameObject);
                break;
            case "Pause":
                EventSystem.current.SetSelectedGameObject(UI.transform.GetChild(1).gameObject);
                break;
            case "Gear Inventory":
                EventSystem.current.SetSelectedGameObject(UI.transform.GetChild(0).GetChild(0).GetChild(0).gameObject);
                break;
            default:
                Debug.Log("Error finding: " + UI.transform.name);
                break;
        }

        tabsMenu.SetActive(false);
    }

    public void QuitGame()
    {
        DataPersistenceManager.instance.SaveGame();
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0);
    }
}
