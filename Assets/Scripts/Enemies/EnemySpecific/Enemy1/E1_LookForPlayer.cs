using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_LookForPlayer : LookForPlayerState
{
    private Enemy1 enemy;

    public E1_LookForPlayer(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_LookForPlayer stateData, Enemy1 enemy1) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy1;
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
        if(isPlayerInMinArgoRange)
        {
            stateMachine.ChangeState(enemy.playerDecetedState);
        }
        else if(isAllTurnsTimeDone)
        {
            stateMachine.ChangeState(enemy.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
