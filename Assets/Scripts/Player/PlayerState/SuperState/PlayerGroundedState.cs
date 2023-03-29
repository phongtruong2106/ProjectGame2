using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int XInput;
    private bool JumpInput;
    private bool grabInput;
    private bool isGrounded;
    private bool isTouchingWall;
    private bool isTouchingLedge;
    private bool dashInput;
    public PlayerGroundedState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
    {

    }

    public override void DoCheck()
    {
        base.DoCheck();
        isGrounded = player.CheckIfGrounded();
        isTouchingWall = player.CheckIfTouchingWall();
        isTouchingLedge = player.CheckIfTouchingLedge();
    }

    public override void Enter()
    {
        base.Enter();

        //kiem tra tinh trang tiep dat
        player.JumpState.ResetAmountOfJumpsLeft();
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // dinh nghia cac phan tu
        XInput = player.InputHandler.NormInputX;
        JumpInput = player.InputHandler.JumpInput;
        grabInput = player.InputHandler.GrabInput;
        dashInput = player.InputHandler.DashInput;

        //xu ly nghiep vu logic cua ca phan tu da duoc dinh nghia
        if(JumpInput && player.JumpState.CanJump())
        {
            stateMachine.ChangeState(player.JumpState); //thuc hien jump chuyen trang thai nhay
        } 
        else if(!isGrounded)
        {
            player.InAirState.StartCoyoteTime();
            // player.JumpState.DecreaseAmountOfJumpsLeft();
            stateMachine.ChangeState(player.InAirState);
        }else if(isTouchingWall && grabInput && isTouchingLedge)
        {
            stateMachine.ChangeState(player.WallGrabState);
        }
         else if(dashInput && player.DashState.CheckIfCanDash()) //thuc hien chuyen doi trang thai Dash 
        {
            stateMachine.ChangeState(player.DashState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
