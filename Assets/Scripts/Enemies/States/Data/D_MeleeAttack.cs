
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newMeleeAttackStateData", menuName = "Data/State Data/Melee Attack Data")]
public class D_MeleeAttack : ScriptableObject
{
    public float attackRadius = 0.5f;
    public float attackDamage = 10f;

    public Vector2 knockbackAngle = Vector2.one;
    public float knockbackStrength = 2.5f;
    public LayerMask whatIsPlayer;
}
