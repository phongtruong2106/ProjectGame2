using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newLookForPlayerStateData", menuName = "Data/State Data/Look For Player Data")]
public class D_LookForPlayer : ScriptableObject
{   
    public int amountOfTurns = 2;
    //khoan thoi gian giua cac luot
    public float timeBetweenTurns = 0.75f;
}
