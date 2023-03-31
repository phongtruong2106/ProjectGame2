using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchIdleState : PlayerGroundedState
{
    public PlayerCrouchIdleState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocityZero();
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
            if(XInput != 0) //neu dau vao cua toa do x khac 0 ( dang duy chuyen) -> chuyen sang trang thai move
            {
                stateMachine.ChangeState(player.CrouchMoveState);
            }
            else if(YInput != -1 && !isTouchingCeiling)
            {
                stateMachine.ChangeState(player.IdieState);
            }
        }
    }

}
