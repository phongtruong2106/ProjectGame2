using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_ChangeState : ChangeState
{
    private Enemy1 enemy;
    public E1_ChangeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_ChangeState stateData, Enemy1 enemy) : base(entity, stateMachine, animBoolName, stateData)
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
        if(performCloseRangeAction)
        {
                stateMachine.ChangeState(enemy.meleeAttackState);
        }

        else if(!isDetectingLedge || isDetectingWall)
        {
            stateMachine.ChangeState(enemy.lookForPlayerState);

        }
        else if(isChargeTimeOver)
        {
            //neu hoat dong tam gan thi trang thai dot cua may sang trang thai tan cong can chien dot cua ke thu
       
            if(isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(enemy.playerDecetedState);
            }
            else
            {
                stateMachine.ChangeState(enemy.lookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
