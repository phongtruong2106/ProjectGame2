using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : State
{
    protected D_DeadState stateData;
    public DeadState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_DeadState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        //bat dau voi trang thai cham hat mau , sau do ta se noi voi thuc the mot doi tuong truc tiep vi cham thanh vi tri ma ta dat san, cuoi cung chuyen doi rotation , dieu do cho phep tui tiep tuc 
        GameObject.Instantiate(stateData.deathBloodParticle, entity.transform.position, stateData.deathBloodParticle.transform.rotation);
        GameObject.Instantiate(stateData.deadChunkParticle, entity.transform.position, stateData.deadChunkParticle.transform.rotation);

        entity.gameObject.SetActive(false);
    }
    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
