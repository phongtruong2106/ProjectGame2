using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSenses : CoreComponent
{
    #region Check Transforms

    public Transform GroundCheck {get => groundCheck; private set => groundCheck = value;}

    public Transform WallCheck {get => wallCheck; private set => wallCheck = value;}

    public Transform LegdeCheck {get => ledgeCheck; private set => ledgeCheck = value;}

    public Transform CeillingCheck {get => cellingCheck; private set => cellingCheck = value;}

    
    public float GroundCheckRadius {get => groundCheckRadius; set => groundCheckRadius = value;}
    public float WallCheckDistance {get => wallCheckDistance; set => wallCheckDistance = value;}

    public LayerMask WhatisGround{get => whatIsGround; set => whatIsGround = value;}

    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform ledgeCheck;
    [SerializeField] private Transform cellingCheck;

    [SerializeField] private float groundCheckRadius;
    [SerializeField] private float wallCheckDistance;

    [SerializeField] private LayerMask whatIsGround;
    #endregion
    #region Check Functions
    public bool Celling //kiem tra tran nhan( hoac 1 thu tuong tu nhu tran nha)
    {
        get =>  Physics2D.OverlapCircle(cellingCheck.position, groundCheckRadius, whatIsGround);
    }
    public bool Grounded //kiem tra Cham ground
    {
        get => Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    public bool WallFront //kiem tra cham wall
    {
        get => Physics2D.Raycast(wallCheck.position, Vector2.right * core.Movement.FacingDirection, wallCheckDistance, whatIsGround);   
    }

    public bool Ledge
    {
        get => Physics2D.Raycast(ledgeCheck.position, Vector2.right * core.Movement.FacingDirection, wallCheckDistance, whatIsGround);
    }

    //kiem tra cham wall back
    public bool WallBack
    {
        get => Physics2D.Raycast(wallCheck.position, Vector2.right * -core.Movement.FacingDirection, wallCheckDistance, whatIsGround);
    }
    #endregion

}
