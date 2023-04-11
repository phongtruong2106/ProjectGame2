using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    protected D_MoveState stateData;

    protected bool isDetectingWall;
    protected bool isDetectingLedge; 
    protected bool isPlayerInMinArgoRange;
   // protected bool isPlayerInMaxArgoRange;
    public MoveState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_MoveState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;

    }

    public override void DoChecks()
    {
        base.DoChecks();
        
        //kiem tra thuc the wall , ledge  
        isDetectingLedge = Core.CollisionSenses.LedgeVertical;
        isDetectingWall = Core.CollisionSenses.WallFront;
        isPlayerInMinArgoRange =entity.checkPlayerInMinAgroRange();
    }

    //tao du lieu nhap ghi de
    public override void Enter()
    {
        base.Enter();
        Core.Movement.SetVelocityX(stateData.movementSpeed * Core.Movement.FacingDirection);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
         Core.Movement.SetVelocityX(stateData.movementSpeed * Core.Movement.FacingDirection);

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }


}
