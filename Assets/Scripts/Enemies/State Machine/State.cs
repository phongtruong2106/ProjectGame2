using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    //view
    //chieu den trang thai huu han
    protected FiniteStateMachine stateMachine;
    protected Entity entity;
  
    protected float startTime;

    protected string animBoolName;

    public State(Entity entity, FiniteStateMachine stateMachine, string animBoolName) //tiem phu thuoc Entity and FiniteStateMachine
    {
        this.entity = entity;
        this.stateMachine = stateMachine; 
        this.animBoolName = animBoolName; 
    }

    public virtual void Enter() //su dung tu khoa ao, co nghia la chuc nang nay co the duoc xac dinh lai trong cac lop dan xuat va chuc nang nay se bi vo hieu 
    {
        startTime = Time.time;
        entity.anim.SetBool(animBoolName, true);
        DoChecks();
    }

    public virtual void Exit()
    {
        entity.anim.SetBool(animBoolName, false);
    }

    //cap nhat ham xu ly logic Update
    public virtual void LogicUpdate()
    {
        
    }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    public virtual void DoChecks()
    {
        
    }
}
