using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : CoreComponent
{
    public Rigidbody2D RB{get; private set;}
    public Vector2 CurrentVelocity{get; private set;}
    public int FacingDirection{get; private set;}
    public bool CanSetVelocity{get; set;}
    private Vector2 workspace;


    protected override void Awake()
    {
        base.Awake();

        RB = GetComponentInParent<Rigidbody2D>();

        FacingDirection = 1;

        CanSetVelocity = true;
    }

    public override void LogicUpdate()
    {
        CurrentVelocity = RB.velocity;
    }

    #region Set Functions

        public void SetVelocityZero()
        {
            workspace = Vector2.zero;
            SetFinalVelocity();
        }
        public void SetVelocity(float velocity,Vector2 angle, int direction) //cai dat van toc
        {
            // lam binh thuong hoa angle
            angle.Normalize();
            workspace.Set(angle.x * velocity * direction, angle.y *velocity);
            SetFinalVelocity();
        } 

        public void SetVelocity(float velocity, Vector2 direction)
        {
            workspace = direction * velocity;
            SetFinalVelocity();
        }

        public void SetVelocityX(float velocity) //cai dat van toc huong duy chuyen truc x
        {
            workspace.Set(velocity, CurrentVelocity.y);
            SetFinalVelocity();
        }

        public void SetVelocityY(float velocity) //cai dat van toc huong duy chuyen truc Y
        {
            workspace.Set(CurrentVelocity.x, velocity);
            SetFinalVelocity();
        }

        private void SetFinalVelocity()
        {
            if(CanSetVelocity)
            {
                RB.velocity =workspace;
                CurrentVelocity = workspace;
            }
        }
        
        public void CheckIfShouldFlip(int XInput)
        {
            if(XInput != 0 && XInput != FacingDirection)
            {
                Flip();
            }
        }

        public void Flip()
        {
            FacingDirection *= -1;
            RB.transform.Rotate(0.0f, 180.0f, 0.0f);
        }
    #endregion
}
