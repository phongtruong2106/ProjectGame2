using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected Core core;
    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;

    protected bool isAnimationFinished;
    protected bool isExitingState;

    protected float startTime;

    private string animBoolName; //goi animation name 

    //create contrustor 
    public PlayerState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName)
    {
        this.player = player;
        this.stateMachine = playerStateMachine;
        this.playerData = playerData;
        this.animBoolName = animBoolName;
        core  = player.Core;
    }

    //dau vao
    public virtual void Enter()
    {
        DoCheck();
        player.Anim.SetBool(animBoolName, true); 
        startTime = Time.time;
        // Debug.Log(animBoolName);
        isAnimationFinished = false;
        isExitingState = false;
    }

    //ket thuc
    public virtual void Exit()
    { 
         player.Anim.SetBool(animBoolName, false); 
         isExitingState = true;
    }

    //xu ly Logic cua tat ca phuong thuc
    public virtual void LogicUpdate()
    {

    }

    //thuc hien Tang Vat li
    public virtual void PhysicsUpdate()
    {
        DoCheck();
    }

    //ham kiem tra 
    public virtual void DoCheck()
    {

    }

    public virtual void AnimationTrigger()
    {
    }
    public virtual void AnimationFinishTrigger() => isAnimationFinished = true;
}
