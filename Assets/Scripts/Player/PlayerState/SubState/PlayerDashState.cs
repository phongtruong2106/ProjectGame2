using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerAbilityState
{
    //khoi tao bien dash 
    public bool CanDash{get; private set;}

    private float lastDashTime;
    public PlayerDashState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
    {

    }


    public override void Enter()
    {
        base.Enter();

        CanDash = false;
    }

    public override void Exit()
    {
        base.Exit();
    }
    
    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    //function transition to the state, CheckIfCanDash
    public bool CheckIfCanDash()
    {
        return CanDash && Time.time >= lastDashTime + playerData.dashCoolDown; //ham se tra ve true neu dash la true
    }

    public void ResetCanDash() => CanDash = true; //reset Dash khi thuc hien Dash truoc do


}
