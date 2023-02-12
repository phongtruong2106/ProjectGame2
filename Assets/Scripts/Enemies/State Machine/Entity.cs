
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{  
   public FiniteStateMachine stateMachine;

   public D_Entity entityData;
   public int facingDirection{get; private set;}
   //lop thuc the
   public Rigidbody2D rb {get; private set;}
   public Animator anim {get; private set; }
   public GameObject aliveGO {get; private set;}
   public AnimationToStateMachine atsm {get; private set;}

   [SerializeField]
   private Transform wallCheck;
   [SerializeField]
   private  Transform ledgeCheck;
   [SerializeField]
   private Transform playerCheck;

   private float currentHealth;

   private int lastDamageDirection;
   private Vector2 velocityWorkspace;

   //tat ca nhung enemy co the override lai 
   public virtual void Start()
   {
        facingDirection = 1;
        currentHealth = entityData.maxHealth;

        aliveGO  = transform.Find("Alive").gameObject;
        rb = aliveGO.GetComponent<Rigidbody2D>();
        anim = aliveGO.GetComponent<Animator>();
        atsm = aliveGO.GetComponent<AnimationToStateMachine>();

        stateMachine  = new FiniteStateMachine();
   }

   public virtual void Update()
   {
       stateMachine.currentState.LogicUpdate();
      
   }

   public virtual void FixedUpdate()
   {
       stateMachine.currentState.PhysicsUpdate();
   }

   public virtual void SetVelocity(float velocity)
   {
         //thiet lap van toc cua ke thu du tren huong doi mat cua no
         velocityWorkspace.Set(facingDirection * velocity, rb.velocity.y);
         rb.velocity = velocityWorkspace;
   }

   public virtual bool CheckWall()
   {
         return Physics2D.Raycast(wallCheck.position, aliveGO.transform.right, entityData.wallCheckDistance, entityData.whatIsGround);
   }

   public virtual bool checkLedge()
   {
         return Physics2D.Raycast(ledgeCheck.position, Vector2.down, entityData.ledgeCheckDistance, entityData.whatIsGround);
   }


//tao ham phat hien player , cach hoat dong giong nhu Checkwall va checkLedge
   public virtual bool checkPlayerInMinAgroRange(){
         return Physics2D.Raycast(playerCheck.position, aliveGO.transform.right, entityData.minArgoDistance, entityData.whatIsPlayer);
   }

   public virtual bool checkPlayerInMaxAgroRange(){
         return Physics2D.Raycast(playerCheck.position, aliveGO.transform.right, entityData.maxArgoDistance, entityData.whatIsPlayer);
   }

   public virtual bool CheckPlayerInCloseRangeAction()
   {  
         return Physics2D.Raycast(playerCheck.position, aliveGO.transform.right, entityData.closeRangeActionDistace, entityData.whatIsPlayer);
   }
   
   public virtual void DamageHop(float velocity)
   {
       velocityWorkspace.Set(rb.velocity.x, velocity);
       rb.velocity = velocityWorkspace;
   }
   
   //thiet hai(damage)
   public virtual void Damage(AttackDetails attackDetails)
   {
            currentHealth -= attackDetails.damageAmount; //enemy bi day lui va bi gay sat thuong

            DamageHop(entityData.damageHopSpeed);

            if(attackDetails.position.x > aliveGO.transform.position.x)
            {
                  lastDamageDirection = -1; 
            }
            else
            {
                  lastDamageDirection = 1;
            }
   }
   public virtual void Flip()
   {
         //Flip huong  voi huong x  *= -1
         facingDirection *= -1;
         aliveGO.transform.Rotate(0f, 180f, 0f); //xoay doi bien doi dia ly xoay no thanh 0, tren x 180 tren y va sau do la 0
   }

   public virtual void OnDrawGizmos() {
            Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.wallCheckDistance));
            Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(Vector2.down * entityData.ledgeCheckDistance));

            Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.closeRangeActionDistace), 0.05f);
            Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.maxArgoDistance), 0.05f);
            Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.minArgoDistance), 0.05f);

   }
}