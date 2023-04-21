using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttackState : AttackState
{
    protected D_RangeAttaclState stateData;
    protected GameObject projectile;
    protected Projectitle projectitleScript;
    public RangeAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_RangeAttaclState stateData) : base(entity, stateMachine, animBoolName, attackPosition)
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
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public override void TriggerAttack()
    {
        base.TriggerAttack();

        projectile =  GameObject.Instantiate(stateData.projectile, attackPosition.position, attackPosition.rotation);
        projectitleScript = projectile.GetComponent<Projectitle>();
        projectitleScript.FireProjectile(stateData.projectileSpeed, stateData.projectitleTravelDistance, stateData.projectileDamage);
    }
}
