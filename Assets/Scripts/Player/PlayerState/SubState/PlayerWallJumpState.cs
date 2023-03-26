using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerAbilityState
{
    private int wallJumpDirection;
    public PlayerWallJumpState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //van toc dat diem cua nguoi choi 
        player.InputHandler.UseJumpInput();
        player.JumpState.ResetAmountOfJumpsLeft(); //lap lai so lan nhay
        player.SetVelocity(playerData.wallJumpVelocity, playerData.wallJumpAngle, wallJumpDirection);
        player.CheckIfShouldFlip(wallJumpDirection); //check doi huong 
        player.JumpState.DecreaseAmountOfJumpsLeft();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //Set hoat anh 
        player.Anim.SetFloat("yVelocity", player.CurrentVelocity.y);
        player.Anim.SetFloat("xVelocity", Mathf.Abs(player.CurrentVelocity.x));

        if(Time.time >= startTime + playerData.wallJumpTime) // thoi gian nhay tuong lay tu playerData
        {
            isAbilityDone = true;
        }
        
    }

    public void DetermineWallJumpDirection(bool isTouchingWall)
    {
        if(isTouchingWall)
        {
            wallJumpDirection = -player.FacingDirection; //khi cham tuong huong nhan vat mac dinh
        }
        else
        {
            wallJumpDirection = player.FacingDirection; //doi huong nhan vat
        }
    }
}
