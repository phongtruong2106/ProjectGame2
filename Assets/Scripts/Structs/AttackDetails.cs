using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct AttackDetails
{
    public Vector2 position;
    public float damageAmount;
    //can mot luong sat thuong de gay choang
    public float stunDamageAmount; 
}

[System.Serializable]
public struct WeaponAttackDetails
{
    public string attackName;
    public float movementSpeed;
    public float damageAmount;
}