using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_IdieState : IdieState
{
    private Enemy2 enemy;
    public E2_IdieState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_IdieState stateData, Enemy2 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //TODO: pds
        if(isIdleTimeOver)
        {
            stateMachine.ChangeState(enemy.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
