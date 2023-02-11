using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_PlayerDeceted : PlayerDecetedState
{
    private Enemy1 enemy1;
    public E1_PlayerDeceted(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDeceted stateData, Enemy1 enemy1) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy1 = enemy1;
        
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
            stateMachine.ChangeState(enemy1.meleeAttackState);
        }
        else if(performLongRangeAction)
        {
            stateMachine.ChangeState(enemy1.chargeState);
        }
        else if(!isPlayerInMaxArgnRange)
        {
            stateMachine.ChangeState(enemy1.lookForPlayerState); //phat hien nguoi choi
        }
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
