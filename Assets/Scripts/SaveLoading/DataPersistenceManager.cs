using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] public string fileName;
    private GameData gameData;
    private List<IDataPersistence> dataPersistences;
    private FileDataHandler dataHandler;

    public static DataPersistenceManager instance { get; private set; }

    private void Start()
    {
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Data Persistence Manager in the scene.");
            Destroy(this.gameObject);
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);

        this.dataPersistences = FindAllDataPersistances();
        LoadGame(fileName);
    }

    public void OnSceneUnloaded(Scene scene)
    {
        SaveGame();
    }

    private List<IDataPersistence> FindAllDataPersistances()
    {
        IEnumerable<IDataPersistence> dataPersistences = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistences);
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame(string _fileName)
    {
        Debug.Log(fileName);
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        // Load saved data from a file using data handler
        this.gameData = dataHandler.Load();
        // If there is no GameData, create a new gamedata
        if (this.gameData == null)
        {
            Debug.Log("No data was found. Initializing data to defaults.");
            NewGame();
            return;
        }

        // Gives the scripts GameData so that they can update themselves
        foreach (IDataPersistence dataPersistence in dataPersistences)
        {
            dataPersistence.LoadData(gameData);
        }
        Debug.Log("Loaded Game " + fileName);
    }

    public void SaveGame()
    {
        // Gives the scripts GameData so that they can update it
        foreach (IDataPersistence dataPersistence in dataPersistences)
        {
            dataPersistence.SaveData(ref gameData);
        }

        // Save the data to a file using data handler
        dataHandler.Save(gameData);

        Debug.Log("Saved Game " + fileName);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
}