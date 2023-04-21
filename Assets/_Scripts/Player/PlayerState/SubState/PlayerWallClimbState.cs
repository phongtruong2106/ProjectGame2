using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallClimbState : PlayerTouchingWall
{
    public PlayerWallClimbState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(!isExitingState)
        {
            Movement?.SetVelocityY(playerData.wallClimbVelocity);

            if(yInput != 1)
            {
                stateMachine.ChangeState(player.WallGrabState);
            }
        }
    }


}
