using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_IdleState : IdieState
{
    private Enemy1 enemy;
    public E1_IdleState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_IdieState stateData, Enemy1 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
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

        if(isPlayerInMinArgnRange)
        {
            stateMachine.ChangeState(enemy.playerDecetedState);
        }
        //neu het thoi gian cho . tro lai de duy chuyen, vi vay co the noi, trang thai cham doi trang thai ngay bay gio
        else if(isIdleTimeOver)
        {
            stateMachine.ChangeState(enemy.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
