using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDashState : PlayerAbilityState
{
    //khoi tao bien dash 
    public bool CanDash{get; private set;}
    private bool isHolding;
    private bool dashInputStop;
    private float lastDashTime;


    private Vector2 dashDirection;
    private Vector2 dashDirectionInput;
    private Vector2 lastAiPos;
    public PlayerDashState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
    {

    }


    public override void Enter()
    {
        base.Enter();

        CanDash = false;
        player.InputHandler.UseDashInput();

        isHolding = true;
        dashDirection =Vector2.right * core.Movement.FacingDirection;

        Time.timeScale = playerData.holdTimeScale;
        startTime = Time.unscaledTime;

        player.DashDirectionIndicator.gameObject.SetActive(true);
        
    }

    public override void Exit()
    {
        base.Exit();
        if(core.Movement.CurrentVelocity.y > 0)
        {
            core.Movement.SetVelocityY(core.Movement.CurrentVelocity.y * playerData.dashEndYMultiplier);
        }
    }
    
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(!isExitingState)
        {
            if(isHolding)
            {
                dashDirectionInput = player.InputHandler.DashDirectionInput; // dinh nghia dashdirectionInput
                dashInputStop = player.InputHandler.DashInputStop; //dinh nghia DashInputStop

                if(dashDirectionInput != Vector2.zero) //neu toa do vector2 khac 0
                {
                    dashDirection  = dashDirectionInput;
                    dashDirection.Normalize();
                    
                }

                float angle = Vector2.SignedAngle(Vector2.right, dashDirection);
                player.DashDirectionIndicator.rotation = Quaternion.Euler(1f, -1f, angle - 45f);

                if(dashInputStop || Time.unscaledTime >= startTime + playerData.maxHoldTime)
                {
                    isHolding = false;
                    Time.timeScale = 1f;
                    startTime = Time.time;
                   core.Movement.CheckIfShouldFlip(Mathf.RoundToInt(dashDirection.x));
                    player.RB.drag = playerData.darg;
                    core.Movement.SetVelocity(playerData.dashVelocity, dashDirection);
                    player.DashDirectionIndicator.gameObject.SetActive(false);
                    PlaceAfterImage();
                }
            }
            else
            {
                core.Movement.SetVelocity(playerData.dashVelocity, dashDirection);
                CheckIfShouldPlaceAfterImange();

                if(Time.time >= startTime + playerData.dashTime)
                {
                    player.RB.drag = 0f;
                    isAbilityDone = true;
                    lastDashTime = Time.time;
                }
            }
        }
    }

    private void CheckIfShouldPlaceAfterImange()
    {
        if(Vector2.Distance(player.transform.position, lastAiPos) >= playerData.distBetweenAfterImages)
        {
            PlaceAfterImage();
        }
    }

    private void PlaceAfterImage()
    {
        PlayerAfterImagePool.Instance.GetFromPool();
        lastAiPos = player.transform.position;
    }

    //function transition to the state, CheckIfCanDash
    public bool CheckIfCanDash()
    {
        return CanDash && Time.time >= lastDashTime + playerData.dashCoolDown; //ham se tra ve true neu dash la true
    }

    public void ResetCanDash() => CanDash = true; //reset Dash khi thuc hien Dash truoc do


}
