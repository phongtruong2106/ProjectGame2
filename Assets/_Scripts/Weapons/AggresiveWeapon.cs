using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AggresiveWeapon : Weapon
{
    protected Movement Movement{get => movement ?? core.GetCoreComponent(ref movement);}
     private Movement movement;
    protected SO_AggressiveWeaponData aggressiveWeaponData;
    private List<IDamageable> detectedDamageables = new List<IDamageable>();
    private List<IknockBackable> detectedKnockbackables = new List<IknockBackable>();

    protected override void Awake()
    {
        base.Awake();

        if(weaponData.GetType() == typeof(SO_AggressiveWeaponData))
        {
            aggressiveWeaponData = (SO_AggressiveWeaponData)weaponData;
        }
        else
        {
            Debug.LogError("wRONG data for the weapon");
        }
    }

    public override void AnimationActionTrigger()
    {
        base.AnimationActionTrigger();
        CheckMeleeAttack();
    }

    private void  CheckMeleeAttack()
    {
        WeaponAttackDetails details  = aggressiveWeaponData.AttackDetails[attackCounter];
        foreach(IDamageable item in detectedDamageables.ToList()) //goi phan tu item duyet tung thanh phan trong detectedDamageAble
        {
            item.Damage(details.damageAmount);
        }

        foreach(IknockBackable item in detectedKnockbackables.ToList())
        {
            item.Knockback(details.knockbackAngle, details.knockbackStrenght, Movement.FacingDirection);
        }

    }
    public void AddToDetected(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();

        if(damageable != null)
        {
            detectedDamageables.Add(damageable);
        }
        IknockBackable knockbackable = collision.GetComponent<IknockBackable>();
        if(knockbackable != null)
        {
            detectedKnockbackables.Add(knockbackable);
        }
    }

    public void RemoveFromDetected(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();

        if(damageable != null)
        {
            detectedDamageables.Remove(damageable);
        }

        IknockBackable knockbackable = collision.GetComponent<IknockBackable>();
        if(knockbackable != null)
        {
            detectedKnockbackables.Remove(knockbackable);
        }
    }
}
