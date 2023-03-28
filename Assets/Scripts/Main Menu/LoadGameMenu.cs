using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadGameMenu : MonoBehaviour
{
    public GameObject game1;
    public GameObject game2;
    public GameObject game3;

    private void Start()
    {
        bool _game1, _game2, _game3;
        _game1 = File.Exists(Path.Combine(Application.persistentDataPath, "game1.json"));
        _game2 = File.Exists(Path.Combine(Application.persistentDataPath, "game2.json"));
        _game3 = File.Exists(Path.Combine(Application.persistentDataPath, "game3.json"));

        if (_game1) { game1.GetComponentInChildren<TMP_Text>().text = "Game #1 | Load"; }
        else { game1.GetComponentInChildren<TMP_Text>().text = "Game #1 | Create"; }
        if (_game2) { game2.GetComponentInChildren<TMP_Text>().text = "Game #2 | Load"; }
        else { game2.GetComponentInChildren<TMP_Text>().text = "Game #2 | Create"; }
        if (_game3) { game3.GetComponentInChildren<TMP_Text>().text = "Game #3 | Load"; }
        else { game3.GetComponentInChildren<TMP_Text>().text = "Game #3 | Create"; }
    }

    public void LoadGame(string saveName)
    {
        DataPersistenceManager.instance.fileName = saveName;
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(1);
    }
}
