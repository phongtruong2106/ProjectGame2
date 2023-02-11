using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//tao menu acid ta co the chi dinh ten tep , no la du lieu co so trong truog hop 
[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entity Data/Base Data")]
public class D_Entity : ScriptableObject
{
    public float maxHealth = 30f;
    public float damageHopSpeed = 3f;
    public float wallCheckDistance = 0.2f;
    public float ledgeCheckDistance = 0.4f;

    public float maxArgoDistance = 1f;
    public float minArgoDistance = 0.5f;

    public float closeRangeActionDistace = 1f;
    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;

}
