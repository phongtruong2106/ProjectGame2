using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
   private Movement Movement{get => movement ?? Core.GetCoreComponent(ref movement);}
    private Movement movement;
    protected Transform attackPosition;
    protected bool isAnimationFinished;

    protected bool isPlayerInMinArgoRange;
    public AttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition) : base(entity, stateMachine, animBoolName)
    {
        this.attackPosition = attackPosition;
    }

       public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinArgoRange = entity.CheckPlayerInCloseRangeAction();
    }

    public override void Enter()
    {
        base.Enter();

        entity.atsm.attackState = this;
        isAnimationFinished = false;
        Movement?.SetVelocityX(0f);
    } 

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        Movement?.SetVelocityX(0f);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public virtual void TriggerAttack()
    {

    }

    public virtual void FinishAttack()
    {
        isAnimationFinished = true;
    }
}
