using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunState : State
{
    protected D_StunState stateData;
    protected bool isStunTimeOver; //dung choang
    protected bool isGrounded;
    protected bool isMovementStopted;
    protected bool performCloseRangeAction;
    protected bool isPlayerInMinAgroRange;
    public StunState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_StunState stateData) : base(entity, stateMachine, animBoolName)
    {
        //gan du lieu trang thai
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = Core.CollisionSenses.Ground;
        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction(); //check Player trong vung Phat hien
        isPlayerInMinAgroRange = entity.checkPlayerInMinAgroRange();
    }
    

    public override void Enter()
    {
        base.Enter();
        isStunTimeOver = false; //ngay luc nay se khong bi choang => viec choang se bi tam hoan
        isMovementStopted = false; //khi bat dau thi se duy chuyen binh thuong => tai day bien nay se de la false
        Core.Movement.SetVelocity(stateData.stunKnockSpeed, stateData.stunKnockbackAngle, entity.lastDamageDirection);
    }

    public override void Exit()
    {
        base.Exit();
        entity.ResetStunResistance();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(Time.time >= startTime +  stateData.stunTime)
        {
            isStunTimeOver = true;
        }

        if(isGrounded && Time.time >= startTime + stateData.stunKnockbackTime && !isMovementStopted)
        {
            isMovementStopted = true; 
            Core.Movement.SetVelocityX(0f);

        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
