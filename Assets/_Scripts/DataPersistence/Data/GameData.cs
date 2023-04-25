using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int deadCount;


    //the values  define in this constructor will be default values
    //the game Start with when there's no data to load 

    public GameData()
    {
        this.deadCount = 0;
    }
}
