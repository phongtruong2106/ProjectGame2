using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Entity //quy dinh Enemy1 la mot entity
{
    public E1_IdleState idleState {get; private set; }
    public E1_MoveState moveState {get; private set; }
    public E1_PlayerDeceted playerDecetedState{get; private set; }
    public E1_ChangeState chargeState {get; private set;}
    public E1_LookForPlayer lookForPlayerState {get; private set;}

    public E1_MeleeAttackState meleeAttackState{get; private set;}

    [SerializeField]
    private D_IdieState idieStateData;
    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_PlayerDeceted playerDecetedData;
    [SerializeField]
    private D_ChangeState chargeStateData;
    [SerializeField]
    private D_LookForPlayer lookForPlayerData;
    [SerializeField] 
    private D_MeleeAttack meleeAttackStateData;

    [SerializeField]
    private Transform meleeAttackPosition;

    public override void Start() {
        //instance
        base.Start();

        moveState = new E1_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new E1_IdleState(this, stateMachine, "idle", idieStateData, this);
        playerDecetedState = new E1_PlayerDeceted(this, stateMachine, "playerDetected", playerDecetedData, this);
        chargeState = new E1_ChangeState(this, stateMachine, "charge", chargeStateData, this); 
        lookForPlayerState = new E1_LookForPlayer(this, stateMachine, "lookForPlayer", lookForPlayerData, this);
        meleeAttackState = new E1_MeleeAttackState(this, stateMachine, "meleeAttack",meleeAttackPosition, meleeAttackStateData,this);

        stateMachine.Initialize(moveState);
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
    }
}
