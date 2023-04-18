using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    
    public PlayerMoveState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();
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
        Movement?.CheckIfShouldFlip(XInput);
        Movement?.SetVelocityX(playerData.movementVelocity * XInput);

        if(!isExitingState)
        {
            if(XInput == 0) //khi van toc tro ve 0 thi doi trang thai idie
            {
                stateMachine.ChangeState(player.IdieState);
            }
            else if(YInput == -1)
            {
                stateMachine.ChangeState(player.CrouchMoveState);
            }

        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
