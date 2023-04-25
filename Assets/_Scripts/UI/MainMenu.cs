using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void OnNewGameClicked()
    {
        DataPersistenceManage.instance.NewGame();
    }


    public void OnLoadGameClicked()
    {
        DataPersistenceManage.instance.LoadGame();
    }

    public void OnSaveGameClicked()
    {
        DataPersistenceManage.instance.SaveGame();
    }
}
