using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchMoveState : PlayerGroundedState
{
    public PlayerCrouchMoveState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
    {
    }

    
    public override void Enter()
    {
        base.Enter();

        player.SetColliderHeight(playerData.crouchColliderHeight);
    }
    public override void Exit()
    {
        base.Exit();
        player.SetColliderHeight(playerData.standColliderHeight);
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(!isExitingState)
        {
            Movement?.SetVelocityX(playerData.crouchMovementVelocity * Movement.FacingDirection);
            Movement?.CheckIfShouldFlip(XInput);

            if(XInput==0) //neu dau vao toa do x == 0  => chuen vef trang thai Idle
            {
                stateMachine.ChangeState(player.CrouchIdleState);
            }
            else if(YInput != -1 && !isTouchingCeiling)
            {
                stateMachine.ChangeState(player.MoveState);
            }
            
        }
    }

}
