using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class DeleteSave : MonoBehaviour
{
    public string game;
    bool isGame;

    private void Update()
    {
        isGame = File.Exists(Path.Combine(Application.persistentDataPath, string.Format("{0}.json", game)));

        this.GetComponent<Button>().interactable = isGame;
    }

    public void DeleteGame()
    {
        File.Delete(Path.Combine(Application.persistentDataPath, string.Format("{0}.json", game)));
        EventSystem system = EventSystem.current;

        switch (game)
        {
            case "game1":
                GameObject.Find("Game 1").GetComponentInChildren<TMP_Text>().text = "Game #1 | Create";
                system.SetSelectedGameObject(GameObject.Find("Game 1"));
                break;
            case "game2":
                GameObject.Find("Game 2").GetComponentInChildren<TMP_Text>().text = "Game #2 | Create";
                system.SetSelectedGameObject(GameObject.Find("Game 2"));
                break;
            case "game3":
                GameObject.Find("Game 3").GetComponentInChildren<TMP_Text>().text = "Game #3 | Create";
                system.SetSelectedGameObject(GameObject.Find("Game 3"));
                break;
        }
    }
}
