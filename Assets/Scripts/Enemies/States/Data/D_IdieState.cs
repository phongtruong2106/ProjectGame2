using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newIdleStateData", menuName = "Data/State Data/Idle Data")]
public class D_IdieState : ScriptableObject
{
    public float minIdleTime = 0.5f;
    public float maxIdleTime = 1f;
}
