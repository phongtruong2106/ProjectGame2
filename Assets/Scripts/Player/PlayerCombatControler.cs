using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatControler : MonoBehaviour
{
    [SerializeField]
    private bool combatEnabled;
    [SerializeField]
    private float inputTimer, attack1Radius, attack1Damage;
    [SerializeField]
    private float stunDamageAmount = 1f;
    [SerializeField]
    private Transform attack1HitBoxPos;
    //nhan biet doi tuong nao that su can phat hien
    [SerializeField]
    private LayerMask whatisDamageable;


    private bool gotInput, isAttacking, isFirstAttack;

    //khai bao bien luu tru cuoi cung de luu tru lan cuoi cung
    private float lastInputTime = Mathf.NegativeInfinity;

    private AttackDetails attackDetails;

    private Animator anim;
    private PlayerControls PC;
    private PlayerStates PS;

    private void Start() {
        anim= GetComponent<Animator>();
        anim.SetBool("canAttack", combatEnabled);
        PC = GetComponent<PlayerControls>();
        PS = GetComponent<PlayerStates>();
    } 


    private void Update() 
    {
        CheckCombatInput();
        CheckAttacks();
    }
    //kiem tra dau vao
    private void CheckCombatInput()
    {
        if(Input.GetMouseButtonDown(0)){
            if(combatEnabled){
                gotInput = true;
                lastInputTime = Time.time;
            }
        }
    }

    //kiem tra vo hieu tan cong
    private void CheckAttacks(){
        if(gotInput){
            //perform attack1
            if(!isAttacking){
                gotInput = false;
                isAttacking = true;
                isFirstAttack = !isFirstAttack;
                anim.SetBool("attack1", true);
                anim.SetBool("firstAttack", isFirstAttack);
                anim.SetBool("isAttacking", isAttacking);
            }
        }
        //cho biet rang lieu thoi gian co lon hon hoac bang lan cuoi khong dat thoi gian cong them voi bo dem thoi gian dau vao sau do doi
        if(Time.time >= lastInputTime + inputTimer){
                //wait for me input
                gotInput = false;
        }   
    }

    //kiem tra boxhit tan cong
    private void CheckAttackHitBox()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attack1HitBoxPos.position, attack1Radius, whatisDamageable);

        attackDetails.damageAmount = attack1Damage; //tan cong 1 
        attackDetails.position =transform.position;//tan cong vi tri x
        //check funtion attack hit box
        attackDetails.stunDamageAmount = stunDamageAmount;

        foreach (Collider2D collider in detectedObjects)
        {
            collider.transform.parent.SendMessage("Damage", attackDetails);
            //Instantiate hit particle
        }
    }

    private void FinishAttack1(){
        isAttacking = false;
        anim.SetBool("isAttacking", isAttacking);
        anim.SetBool("attack1", false);

    }

    private void Damage(AttackDetails attackDetails)
    {
        if(!PC.GetDashStatus())
        {
            int direction;

            PS.DecreaseHealth(attackDetails.damageAmount);

            //gay sat thuong nguoi choi tai day bang cach su dung attackDetails[0]

            if(attackDetails.position.x < transform.position.x){
                //su hien dien cua ta it hon su hien dien cua dich
                direction = 1;
            }
            else{
                direction = -1;
            }

            PC.Knockback(direction);  
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attack1HitBoxPos.position, attack1Radius);
    }
}
