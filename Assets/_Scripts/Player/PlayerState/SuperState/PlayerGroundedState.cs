using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int XInput;
    protected int YInput;

    protected bool isTouchingCeiling;

    protected Movement Movement{get => movement ?? core.GetCoreComponent(ref movement);}
 
    private CollisionSenses CollisionSenses{  get => collisionSenses ?? core.GetCoreComponent(ref collisionSenses);}
    private CollisionSenses collisionSenses;
     private Movement movement;

    private bool jumpInput;
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

        if(CollisionSenses)
        {

            isGrounded = CollisionSenses.Ground;
            isTouchingWall = CollisionSenses.WallFront;
            isTouchingLedge = CollisionSenses.LedgeHorizontal;
            //dinh nghia  cho isTouchingCeiling
            isTouchingCeiling = CollisionSenses.Celling;
        }
    }

    public override void Enter()
    {
        base.Enter();

        //kiem tra tinh trang tiep dat
        player.JumpState.ResetAmountOfJumpsLeft();
        //thuc hien lan dau tien su dung dash => tai lai Dash khi nguoi thuc hien dash    
        player.DashState.ResetCanDash();
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
        YInput = player.InputHandler.NormInputY;
        jumpInput = player.InputHandler.JumpInput;
        grabInput = player.InputHandler.GrabInput;
        dashInput = player.InputHandler.DashInput;

        if(player.InputHandler.AttackInputs[(int)CombatInput.primary] && !isTouchingCeiling){
            stateMachine.ChangeState(player.PrimaryAttackState);
        }
        else if(player.InputHandler.AttackInputs[(int)CombatInput.secondary] && !isTouchingCeiling)
        {
            stateMachine.ChangeState(player.SecondaryAttackState);
        }
        //xu ly nghiep vu logic cua ca phan tu da duoc dinh nghia
        else if(jumpInput && player.JumpState.CanJump() && !isTouchingCeiling)
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
         else if(dashInput && player.DashState.CheckIfCanDash() && !isTouchingCeiling) //thuc hien chuyen doi trang thai Dash 
        {
            stateMachine.ChangeState(player.DashState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
