using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Entity
{
    public E2_MoveState moveState{get; private set;}
    public E2_IdieState idieState{get; private set;}

    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_IdieState idleStateData;

    public override void Start() {
        base.Start();

        moveState = new E2_MoveState(this, stateMachine, "move", moveStateData, this);
        idieState = new E2_IdieState(this, stateMachine, "idle", idleStateData, this);

        stateMachine.Initialize(moveState);
    }

    public override void Damage(AttackDetails attackDetails)
    {
        base.Damage(attackDetails);
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }
}
