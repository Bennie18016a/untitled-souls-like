using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    private GameData gameData;
    private List<IDataPersistence> dataPersistences;
    private FileDataHandler dataHandler;

    public static DataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Data Persistence Manager in the scene.");
        }
        instance = this;
    }

    private void Start()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistences = FindAllDataPersistances();
        LoadGame();
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

    public void LoadGame()
    {
        // Load saved data from a file using data handler
        this.gameData = dataHandler.Load();
        // If there is no GameData, create a new gamedata
        if (this.gameData == null)
        {
            Debug.Log("No data was found. Initializing data to defaults.");
            NewGame();
        }

        // Gives the scripts GameData so that they can update themselves
        foreach (IDataPersistence dataPersistence in dataPersistences)
        {
            dataPersistence.LoadData(gameData);
        }
        Debug.Log("Loaded Game");
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

        Debug.Log("Saved Game");
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
}