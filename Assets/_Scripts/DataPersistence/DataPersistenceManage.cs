
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistenceManage : MonoBehaviour
{
    
    [Header("File Storage Config")]
    [SerializeField] private string fileName;

    private GameData gameData;
    private List<IDDataPersistence> dataPersistencesObjects;

    private FileDataHandle dataHandler;

    public static DataPersistenceManage instance {get; private set;}

    private void Awake() {
        if(instance != null)
        {
             Debug.LogError("Found more than one Data Persistence Manager in the scene.");
        }
        instance = this;
    }

    private void Start() {
        
        this.dataHandler = ScriptableObject.CreateInstance<FileDataHandle>();
        this.dataHandler.Init(Application.persistentDataPath, fileName);
        this.dataPersistencesObjects = FindAllDataPeristenceObjects();
        LoadGame();
    }

    //That base function 

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        // load any saved data from a file using the data 
        this.gameData= dataHandler.Load();

        //if no data can be loaded, initialize to a new game
        if(this.gameData == null)
        {
            Debug.Log("No data was found. Initializing data to defaults.");
            NewGame();
        }

        // push the loaded data to all other scripts that need it
        //using loop foreach
        foreach (IDDataPersistence dataPersistenceObj in dataPersistencesObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        //pass the data to other scripts so they can update it 
         foreach (IDDataPersistence dataPersistenceObj in dataPersistencesObjects)
        {
            dataPersistenceObj.SaveData(ref gameData);
        }
        //save that data to file using the data handler
        dataHandler.Save(gameData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDDataPersistence> FindAllDataPeristenceObjects()
    {
        //using System link
        IEnumerable<IDDataPersistence>  dataPersistencesObjects = FindObjectsOfType<MonoBehaviour>() .OfType<IDDataPersistence>();
        //call to initialize the list now  and the load  game method will loop through each
        return new List<IDDataPersistence>(dataPersistencesObjects);
    }
}
