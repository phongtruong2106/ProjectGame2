using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Entity
{
    public E2_MoveState moveState{get; private set;}
    public E2_IdieState idieState{get; private set;}
    public E2_PlayerDetectedState playerDetectedState{get; private set;}
    public E2_MeleeAttackState meleeAttackState{get; private set;}
    public E2_LookForPlayerState lookForPlayerState{get; private set;}
    public E2_StunState stunState{get; private set;}
    public E2_DeadState deadState{get; private set;}
    public E2_DodgeState dodgeState{get; private set;}
    public E2_RangeAttackState rangeAttackState{get; private set;}

    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_IdieState idleStateData;
    [SerializeField]
    private D_PlayerDeceted playerDetectesStateData;
    [SerializeField]
    private D_MeleeAttack meleeAttackStateData;
    [SerializeField]
    private D_LookForPlayer lookForPlayerStateData;
    [SerializeField]
    private D_StunState stunStateData;
    [SerializeField]
    private D_DeadState deadStateData;
    [SerializeField]
    public D_DodgeState dodgeStateData;
    [SerializeField]
    private D_RangeAttaclState rangeAttaclStateData;



    [SerializeField]
    private Transform meleeAttackPosition;
    [SerializeField]
    private Transform rangedAttackPosition;

    public override void Awake() {
        base.Awake();

        moveState = new E2_MoveState(this, stateMachine, "move", moveStateData, this);
        idieState = new E2_IdieState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new E2_PlayerDetectedState(this, stateMachine,"playerDetected", playerDetectesStateData, this);
        meleeAttackState = new E2_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);
        lookForPlayerState = new E2_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        stunState = new E2_StunState(this, stateMachine, "stun", stunStateData, this);
        deadState = new E2_DeadState(this, stateMachine, "dead",deadStateData, this);
        dodgeState = new E2_DodgeState(this, stateMachine, "dodge", dodgeStateData, this);
        rangeAttackState = new E2_RangeAttackState(this, stateMachine, "rangedAttack",rangedAttackPosition ,rangeAttaclStateData, this);

        stateMachine.Initialize(moveState);
    }

    public override void Damage(AttackDetails attackDetails)
    {
        base.Damage(attackDetails);

        if(isDead)
        {
            stateMachine.ChangeState(deadState);
        }
        else if(isStunned && stateMachine.currentState != stunState)
        {
            stateMachine.ChangeState(stunState);
        }
        else if(checkPlayerInMinAgroRange())
        {
            stateMachine.ChangeState(rangeAttackState);
        }
        else if(!checkPlayerInMinAgroRange())
        {
            lookForPlayerState.SetTurnImmediately(true);
            stateMachine.ChangeState(lookForPlayerState);
        }
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
    }
}
