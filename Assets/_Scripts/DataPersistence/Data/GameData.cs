using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int deadCount;
    public Vector3 playerPosition;

    public SerializableDictionary<string, bool> coinsCollected;


    //the values  define in this constructor will be default values
    //the game Start with when there's no data to load 

    public GameData()
    {
        this.deadCount = 0;
        playerPosition =  Vector3.zero;
        coinsCollected = new SerializableDictionary<string, bool>();
    }
}
