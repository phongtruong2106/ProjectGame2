using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeState : State
{
    protected Movement Movement{get => movement ?? Core.GetCoreComponent(ref movement);}
 
    private CollisionSenses CollisionSenses{  get => collisionSenses ?? Core.GetCoreComponent(ref collisionSenses);}
    private CollisionSenses collisionSenses;
     private Movement movement;
    protected D_ChangeState stateData;

    protected bool isPlayerInMinAgroRange;
    protected bool isDetectingLedge; //vung phat hien ground
    protected bool isDetectingWall; //vung phat hien wall
    protected bool isChargeTimeOver;
    protected bool performCloseRangeAction;
    public ChangeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_ChangeState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

         //kiem tra go bang go thuc the 
        isPlayerInMinAgroRange = entity.checkPlayerInMinAgroRange();
        isDetectingLedge = CollisionSenses.LedgeVertical;
        isDetectingWall = CollisionSenses.WallFront;


        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
    }

    public override void Enter()
    {
        base.Enter();

        isChargeTimeOver = false;
        // isPlayerInMinAgroRange = entity.checkPlayerInMinAgroRange();
        Movement?.SetVelocityX(stateData.chargeSpeed * Movement.FacingDirection);   
    }
    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
         Movement?.SetVelocityX(stateData.chargeSpeed * Movement.FacingDirection);   

        if(Time.time >= startTime + stateData.chargeTime)
        {
            isChargeTimeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        //khi enemies tan cong nguoi choi thi no se dung sac
        base.PhysicsUpdate();
    }
}
