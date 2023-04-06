using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
    
        core.Movement.SetVelocityX(0f); 
    }

    public override void Exit()
    {
        base.Exit();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(!isExitingState)
        {
            if(XInput != 0)
            {
                stateMachine.ChangeState(player.MoveState);
            }
            else if(YInput == -1)
            {
                stateMachine.ChangeState(player.CrouchIdleState);
            }

        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
