using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AggresiveWeapon : Weapon
{
    protected SO_AggressiveWeaponData aggressiveWeaponData;
    private List<IDamageable> detectedDamageable = new List<IDamageable>();

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
        foreach(IDamageable item in detectedDamageable.ToList()) //goi phan tu item duyet tung thanh phan trong detectedDamageAble
        {
            item.Damage(details.damageAmount);
        }

    }
    public void AddToDetected(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();

        if(damageable != null)
        {
            detectedDamageable.Add(damageable);
        }
    }

    public void RemoveFromDetected(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();

        if(damageable != null)
        {
            detectedDamageable.Remove(damageable);
        }
    }
}
