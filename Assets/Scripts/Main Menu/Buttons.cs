using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject loadGame;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void StartGame()
    {
        mainMenu.SetActive(false);
        loadGame.SetActive(true);

        EventSystem.current.SetSelectedGameObject(loadGame.transform.GetChild(0).gameObject);
    }

    public void Back()
    {
        mainMenu.SetActive(true);
        loadGame.SetActive(false);

        EventSystem.current.SetSelectedGameObject(mainMenu.transform.GetChild(1).GetChild(0).gameObject);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
