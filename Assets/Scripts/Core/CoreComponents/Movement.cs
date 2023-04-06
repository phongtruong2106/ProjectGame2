using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : CoreComponent
{
    public Rigidbody2D RB{get; private set;}
    public Vector2 CurrentVelocity{get; private set;}
    private Vector2 workspace;

    protected override void Awake()
    {
        base.Awake();

        RB = GetComponentInParent<Rigidbody2D>();
    }

    public void LogicUpdate()
    {
        CurrentVelocity = RB.velocity;
    }

    #region  Set Functions

        public void SetVelocityZero()
        {
            RB.velocity = Vector2.zero;
            CurrentVelocity = Vector2.zero;
        }
        public void SetVelocity(float velocity,Vector2 angle, int direction) //cai dat van toc
        {
            // lam binh thuong hoa angle
            angle.Normalize();
            workspace.Set(angle.x * velocity * direction, angle.y *velocity);
            RB.velocity= workspace;
            CurrentVelocity = workspace;
        } 

        public void SetVelocity(float velocity, Vector2 direction)
        {
            workspace = direction * velocity;
            RB.velocity = workspace;
            CurrentVelocity = workspace;
        }

        public void SetVelocityX(float velocity) //cai dat van toc huong duy chuyen truc x
        {
            workspace.Set(velocity, CurrentVelocity.y);
            RB.velocity = workspace;
            CurrentVelocity = workspace;
        }

        public void SetVelocityY(float velocity) //cai dat van toc huong duy chuyen truc Y
        {
            workspace.Set(CurrentVelocity.x, velocity);
            RB.velocity = workspace;
            CurrentVelocity = workspace;
        }
    #endregion
}
