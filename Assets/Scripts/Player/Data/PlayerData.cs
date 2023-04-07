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
    [Header("wall Jump State")]
    public float wallJumpVelocity = 20;
    //thoi gian hoan nhay tuong
    public float wallJumpTime = 0.4f;
    public Vector2 wallJumpAngle = new Vector2(1,2);

    [Header("In Air State")]
    public float coyoteTime = 0.2f;
    public float variableJumpHeightMultiplier = 0.5f;

    [Header("wall Slide State")]
    public float wallSlideVelocity = 3f;

    [Header("Wall Climb State")]
    public float wallClimbVelocity = 3f;

    [Header("Ledge Climb State")]
    public Vector2 startOffset;
    public Vector2 stopOffset;

    [Header("Dash State")]
    public float dashCoolDown = 0.5f; //khoan thoi gian dash bi han che 
    public float maxHoldTime = 1f; //thoi gian toi da giu trang thai Dash
    public float holdTimeScale = 0.25f; //ti le thoi gian giu trang Dash
    public float dashTime = 0.2f; //htoi gian thuc hien Dash
    public float dashVelocity = 10f; //toc do thuc hien khi Dash
    public float darg = 5f; //do troi khi thoi hien dash
    public float dashEndYMultiplier = 0.2f; // ket thuc 
    public float distBetweenAfterImages = 0.5f; //khoang cach giua cach hoat anh voi nhau

    [Header("Crouch States")]
    public float crouchMovementVelocity = 5f;
    public float crouchColliderHeight = 0.8f;
    public float standColliderHeight = 0.3374966f;

    

    [Header("Check Variable")]
    public float groundCheckRadius = 0.3f;
    public float groundCheckCeilingRadius = 0.6f;
    public float wallCheckDistance = 0.5f;

}
