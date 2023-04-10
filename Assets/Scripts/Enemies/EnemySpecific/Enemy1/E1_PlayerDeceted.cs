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
        } //phat hien legde sao do cham thuc the khac khi dang phat hien 
        else if(!isDetectingLedge)
        {
            Core.Movement.Flip(); // khi phat hien thi quay nguoc 
            //khi phat hien ledge chuyen sang doi trang thai duy chuyen
            stateMachine.ChangeState(enemy1.moveState);

        }
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
