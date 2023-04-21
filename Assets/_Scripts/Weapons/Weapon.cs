using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected SO_weaponData weaponData;
     protected Animator baseAnimator;
     protected Animator weaponAnimator;
     protected PlayerAttackState state;
     protected Core core;

     

     protected int attackCounter; //bo dem 

     protected virtual void Awake()
     {
        baseAnimator = transform.Find("Base").GetComponent<Animator>();
        weaponAnimator = transform.Find("Weapon").GetComponent<Animator>();

        gameObject.SetActive(false);
     }

     public virtual void EnterWeapon()
     {
        gameObject.SetActive(true);

        if(attackCounter >= weaponData.amountOfAttacks) //neu toi so3 thi attackCounter tro ve 0
        {
           attackCounter = 0;
        }

        baseAnimator.SetBool("attack",true);
        weaponAnimator.SetBool("attack", true);


        baseAnimator.SetInteger("attackCounter", attackCounter);
        weaponAnimator.SetInteger("attackCounter", attackCounter); //thiet lap bo dem attack
     }

     public virtual void ExitWeapon()
     {
        baseAnimator.SetBool("attack",false);
        weaponAnimator.SetBool("attack", false);

        attackCounter++; //cong don den so thu tu cua lan danh animation vidu 0 -> thuc hien don danh thu 1 , 0 tang len 1 -> thuc hien don danh thu 2

        gameObject.SetActive(false);
     }

    #region Animation Trigger

    public virtual void AnimationFinishTrigger()
    {
        state.AnimationFinishTrigger();
    }

   public virtual void AnimationStartMovementTrigger()
   {
      state.SetPlayerVelocity(weaponData.movementSpeed[attackCounter]);
   }
   public virtual void AnimationStopMovementTrigger()
   {
      state.SetPlayerVelocity(0f);
   }

   public virtual void AnimationTurnOffFlipTrigger()
   {
      state.SetFlipCheck(false);
   }

   public virtual void AnimationTurnOnFlipTrigger()
   {
      state.SetFlipCheck(true);
   }

   public virtual void AnimationActionTrigger()
   {
      state.SetFlipCheck(true);
   }
    #endregion

    public void InitializeWeapon(PlayerAttackState state, Core core)
    {
        this.state =state;
        this.core = core;
    }

}
