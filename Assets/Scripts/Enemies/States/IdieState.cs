using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdieState : State
{
    protected D_IdieState stateData;

    protected bool flipAfterIdle;
    protected bool isIdleTimeOver;
    protected bool isPlayerInMinArgnRange;

    //theo doi thoi gian nhan roi
    protected float idleTime;
    public IdieState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_IdieState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isPlayerInMinArgnRange = entity.checkPlayerInMinAgroRange();
    }

    public override void Enter()
    {
        base.Enter();
        Core.Movement.SetVelocityX(0f); //cho ke thu ngungduy chuyen 
        isIdleTimeOver = false;
        SetRandomIdleTime();
    }

    public override void Exit()
    {
        base.Exit();

        if(flipAfterIdle)
        {
            Core.Movement.Flip();
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Core.Movement.SetVelocityX(0f);
        if(Time.time >= startTime + idleTime)
        {
            isIdleTimeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void SetFlipAfterIdle(bool flip)
    {
        flipAfterIdle = flip;
    }

    private void SetRandomIdleTime()
    {
        idleTime = Random.Range(stateData.minIdleTime, stateData.maxIdleTime);
    }
    
}
