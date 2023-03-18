using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;

    protected float startTime;

    private string animBoolName; //goi animation name 

    //create contrustor 
    public PlayerState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName)
    {
        this.player = player;
        this.stateMachine = playerStateMachine;
        this.playerData = playerData;
        this.animBoolName = animBoolName;
    }

    //dau vao
    public virtual void Enter()
    {
        DoCheck();
    }

    //ket thuc
    public virtual void Exit()
    {

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
}
