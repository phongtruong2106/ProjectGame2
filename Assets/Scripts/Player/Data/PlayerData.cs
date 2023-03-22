using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="newPlayerData", menuName ="Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    // Start is called before the first frame update
    [Header("Move State")]
    public float movementVelocity = 10f;
    [Header("Jump State")]
    public float jumpVelocity = 15f;
    public int amountOfJumps = 1;

    [Header("In Air State")]
    public float coyoteTime = 0.2f;

    [Header("Check Variable")]
    public float groundCheckRadius = 0.3f;
    public LayerMask whatIsGround;
}
