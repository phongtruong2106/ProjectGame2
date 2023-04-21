using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//tao menu acid ta co the chi dinh ten tep , no la du lieu co so trong truog hop 
[CreateAssetMenu(fileName = "newMoveStateData", menuName = "Data/State Data/Move Data")]
public class D_MoveState : ScriptableObject
{
    public float movementSpeed = 3f;

}
