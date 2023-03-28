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
        EventSystem.current.SetSelectedGameObject(UI.transform.GetChild(1).gameObject);
        tabsMenu.SetActive(false);
    }

    public void QuitGame()
    {
        DataPersistenceManager.instance.SaveGame();
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0);
    }
}
