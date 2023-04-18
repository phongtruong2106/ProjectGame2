using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSenses : CoreComponent
{
    protected Movement Movement{get => movement ?? core.GetCoreComponent(ref movement);}
     private Movement movement;
    #region Check Transforms

    public Transform GroundCheck 
    {
        get => GenericNotImplementedError<Transform>.TryGet(groundCheck, core.transform.name); //T o day la ground check
        private set => groundCheck = value;
    }

    public Transform WallCheck 
    { 
        get => GenericNotImplementedError<Transform>.TryGet(wallCheck, core.transform.name); 
         private set => wallCheck = value;}

    public Transform LedgeCheckHorizontal 
    { 
        get => GenericNotImplementedError<Transform>.TryGet(ledgeCheckHorizontal, core.transform.name); 
         private set => ledgeCheckHorizontal = value;
    }
    public Transform LedgeCheckVertical 
    {
        get => GenericNotImplementedError<Transform>.TryGet(ledgeCheckVertical, core.transform.name); 
        private set => ledgeCheckVertical = value;
    }

    public Transform CeilingCheck 
    {
        get => GenericNotImplementedError<Transform>.TryGet(cellingCheck, core.transform.name); 
        private set => cellingCheck = value;
    }

    
    public float GroundCheckRadius {get => groundCheckRadius; set => groundCheckRadius = value;}
    public float WallCheckDistance {get => wallCheckDistance; set => wallCheckDistance = value;}

    public LayerMask WhatisGround{get => whatIsGround; set => whatIsGround = value;}

    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform ledgeCheckHorizontal;
    [SerializeField] private Transform ledgeCheckVertical;
    [SerializeField] private Transform cellingCheck;

    [SerializeField] private float groundCheckRadius;
    [SerializeField] private float wallCheckDistance;

    [SerializeField] private LayerMask whatIsGround;
    #endregion
    #region Check Functions
    public bool Celling //kiem tra tran nhan( hoac 1 thu tuong tu nhu tran nha)
    {
        get => Physics2D.OverlapCircle(CeilingCheck.position, groundCheckRadius, whatIsGround);
    }
    public bool Ground //kiem tra Cham ground
    {
        get => Physics2D.OverlapCircle(GroundCheck.position, groundCheckRadius, whatIsGround);
    }

    public bool WallFront //kiem tra cham wall
    {
        get => Physics2D.Raycast(WallCheck.position, Vector2.right * Movement.FacingDirection, wallCheckDistance, whatIsGround); 
    }

    public bool LedgeHorizontal
    {
        get => Physics2D.Raycast(LedgeCheckHorizontal.position, Vector2.right * Movement.FacingDirection, wallCheckDistance, whatIsGround);
    }

    public bool LedgeVertical
    {
        get => Physics2D.Raycast(LedgeCheckVertical.position, Vector2.down, wallCheckDistance, whatIsGround);
    }

    //kiem tra cham wall back
    public bool WallBack
    {
        get => Physics2D.Raycast(WallCheck.position, Vector2.right * -Movement.FacingDirection, wallCheckDistance, whatIsGround);
    }
    #endregion

}
