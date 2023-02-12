using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newStunStateData", menuName = "Data/State Data/Stun State")]
public class D_StunState : ScriptableObject
{
    public float stunTime = 3f; //thoi gian choang

    public float stunKnockbackTime  = 0.2f; //thoi gian bi day lui

    public float stunKnockSpeed = 20f; //toc do bi day lui
    public Vector2 stunKnockbackAngle; //toa do bị day lui 




}
