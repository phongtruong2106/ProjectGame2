using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDecetedState : State
{
    protected D_PlayerDeceted stateData;
    protected bool isPlayerInMinArgnRange;
    protected bool isPlayerInMaxArgnRange;
    protected bool performLongRangeAction;
    protected bool performCloseRangeAction;
    protected bool isDetectingLedge;

    public PlayerDecetedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName,D_PlayerDeceted stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isPlayerInMinArgnRange = entity.checkPlayerInMinAgroRange();
        isPlayerInMaxArgnRange = entity.checkPlayerInMaxAgroRange();
        isDetectingLedge = entity.checkLedge();
        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
    }

    public override void Enter()
    {
        base.Enter();

        performLongRangeAction = false;
        entity.SetVelocity(0f);
    }
    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate(); 
        //neu thoi gian co lon hon or = thoi gian bat dau + voi du lieu trang thai 
        if(Time.time >= startTime + stateData.longRangeActionTime){
                //hanh dong tam xa
                performLongRangeAction = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
