
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{  
   public FiniteStateMachine stateMachine;

   public D_Entity entityData;

   //lop thuc the
   public Animator anim {get; private set; }
   public AnimationToStateMachine atsm {get; private set;}
   public int lastDamageDirection{get; private set;}
   public Core Core{get; private set;}

   [SerializeField]
   private Transform wallCheck;
   [SerializeField]
   private  Transform ledgeCheck;
   [SerializeField]
   private Transform playerCheck;
   [SerializeField]
   private Transform groundCheck; //kiem tra mat dat

   private float currentHealth;
   private float currentStunResistance;
   private float lastDamageTime;
   private Vector2 velocityWorkspace;

   protected bool isStunned;
   protected bool isDead;

   //tat ca nhung enemy co the override lai 
   public virtual void Awake()
   {
        Core = GetComponentInChildren<Core>();

        currentHealth = entityData.maxHealth;
        currentStunResistance = entityData.stunResistance; // kha nag chong choang 
 
      //   aliveGO  = transform.Find("Alive").gameObject; //khong can phai khai bao
        anim = GetComponent<Animator>();
        atsm = GetComponent<AnimationToStateMachine>();

        stateMachine  = new FiniteStateMachine();
   }

   public virtual void Update()
   {
       stateMachine.currentState.LogicUpdate();
       anim.SetFloat("yVelocity", Core.Movement.RB.velocity.y);
       if(Time.time >= lastDamageTime + entityData.stunRecoveryTime)
       {
            ResetStunResistance();
       }
      
   }

   public virtual void FixedUpdate()
   {
       stateMachine.currentState.PhysicsUpdate();
   }


//tao ham phat hien player , cach hoat dong giong nhu Checkwall va checkLedge
   public virtual bool checkPlayerInMinAgroRange(){
         return Physics2D.Raycast(playerCheck.position, transform.right, entityData.minArgoDistance, entityData.whatIsPlayer);
   }

   public virtual bool checkPlayerInMaxAgroRange(){
         return Physics2D.Raycast(playerCheck.position, transform.right, entityData.maxArgoDistance, entityData.whatIsPlayer);
   }

   public virtual bool CheckPlayerInCloseRangeAction()
   {  
         return Physics2D.Raycast(playerCheck.position, transform.right, entityData.closeRangeActionDistace, entityData.whatIsPlayer);
   }
   
   public virtual void DamageHop(float velocity)
   {
       velocityWorkspace.Set(Core.Movement.RB.velocity.x, velocity);
       Core.Movement.RB.velocity = velocityWorkspace;
   }
   
   public virtual void ResetStunResistance()
   {
      //tro lai trang thai choang
      isStunned = false;
      currentStunResistance = entityData.stunResistance;
   }

   //thiet hai(damage)
   public virtual void Damage(AttackDetails attackDetails)
   {
            lastDamageTime = Time.time;

            currentHealth -= attackDetails.damageAmount; //enemy bi day lui va bi gay sat thuong
            currentStunResistance -= attackDetails.stunDamageAmount;

            DamageHop(entityData.damageHopSpeed);

            Instantiate(entityData.hitParticle, transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));

            if(attackDetails.position.x > transform.position.x)
            {
                  lastDamageDirection = -1; 
            }
            else
            {
                  lastDamageDirection = 1;
            }
            if(currentStunResistance <= 0)
            {
                  isStunned = true;
            }
            if(currentHealth <= 0)
            {
                  isDead = true;
            }
   }
//    public virtual void Flip()
//    {
//          //Flip huong  voi huong x  *= -1
//          facingDirection *= -1;
//          transform.Rotate(0f, 180f, 0f); //xoay doi bien doi dia ly xoay no thanh 0, tren x 180 tren y va sau do la 0
//    }     

   public virtual void OnDrawGizmos() {
      if(Core != null)
      {
            Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * Core.Movement.FacingDirection * entityData.wallCheckDistance));
            Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(Vector2.down * entityData.ledgeCheckDistance));

            Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.closeRangeActionDistace), 0.05f);
            Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.maxArgoDistance), 0.05f);
            Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.minArgoDistance), 0.05f);

      }

   }
}