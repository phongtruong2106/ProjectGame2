
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistenceManage : MonoBehaviour
{

    private GameData gameData;
    private List<IDDataPersistence> dataPersistencesObjects;
    public static DataPersistenceManage instance {get; private set;}

    private void Awake() {
        if(instance != null)
        {
             Debug.LogError("Found more than one Data Persistence Manager in the scene.");
        }
        instance = this;
    }

    private void Start() {
        
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
        //TODO - load any saved data from a file using the data 
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

        Debug.Log("Loaded  death count = " + gameData.deadCount);
    }

    public void SaveGame()
    {
        //pass the data to other scripts so they can update it 
         foreach (IDDataPersistence dataPersistenceObj in dataPersistencesObjects)
        {
            dataPersistenceObj.SaveData(ref gameData);
        }

        Debug.Log("Save death count = " + gameData.deadCount);

        //TODO - save that data to file using the data handler
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
