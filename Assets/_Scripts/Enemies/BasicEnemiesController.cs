
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemiesController : MonoBehaviour
{
    //khai bao list trang thai
    private enum State
    {
        Walking,
        Knockback,
        Dead
    }

    //lay trang thai da liet ke tren enum
    private State currentState; //hoat dong so nguyen : walking  = 1, Knockback = 2, dead = 3

    //khai bao khoang cach
    [SerializeField]
    private float
        groundCheckDistance, // khoang cach ground
        wallCheckDistance,  //khoang cach wall
        movementSpeed,  //toc do 
        maxHealth, // mau toi da
        knockbackDuration,
        lastTouchDamageTime,//lan cuoi cham vao
        touchDamageCoolDown, //thoi gian hoi 
        touchDamage, //luong damage gay ra
        touchDamageWidth,
        touchDamageHeight; 

    [SerializeField] 
    private Transform
            groundCheck,
            wallCheck,
            touchDamageCheck;
    //tuan tu hoa
    [SerializeField]
    private LayerMask
             whatisGround,
             whatisPlayer;
    [SerializeField]
    private Vector2 knockbackSpeed;
    //tham chieu particle
    [SerializeField] 
    private GameObject
            hitParticle,
            deathChunkParticle,
            deathBloodParticle;
    //tao mau
    private float 
            currentHealth,
            knockbackStartTime;

    private float[] attackDetails = new float[2];
    
    private int 
            facingDirection,
            damageDirection;

    private Vector2 
            movement,
            touchDamageBotLeft,
            touchDamageTopRight;


    private bool
        groundDetected,
        wallDetected;
    private GameObject alive;
    private Rigidbody2D aliverb;
    private Animator aliveAnim;

    private void Start() {
        //tham chieu den Object
        alive = transform.Find("Alive").gameObject;
        aliverb = alive.GetComponent<Rigidbody2D>();
        aliveAnim = alive.GetComponent<Animator>();

        currentHealth = maxHealth; //mau vua them = vua mau toi da
        facingDirection = 1;
    }
    private void Update() {
        //nhan biet trang thai nao duoc hoat dong sau do dua vao day de update cac ham jhac nhau cho cac trang thai khac nhau
        //thuc hien viec nay bang cach su dung switch
        switch(currentState){
            case State.Walking:
            //neu la trang thai walk
                UpdateWalkingState();
                break;
            case State.Knockback:
                UpdateKnockbackState();
                break;
            case State.Dead:
                UpdateDeadState();
                break;

        }
    }
    //Walking State ---------------------
    private void EnterWalkingState(){

    }

    //trang thai cap nhat
    private void UpdateWalkingState(){
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatisGround);
        wallDetected = Physics2D.Raycast(wallCheck.position, Vector2.right, wallCheckDistance, whatisGround);
        CheckTouchDamage();
        //neu khong phat hien ra ground co nghia la tia raycast ban xuong khong phat hien , quay lai huong khac
        if(!groundDetected || wallDetected){
                //walk flip
                Flip();
        }
        else
        {
            //neu phat hien dyuong thi se duy chuyen
            movement.Set(movementSpeed * facingDirection, aliverb.velocity.y);
            aliverb.velocity = movement;
        }
    }

    //thoat khoi trang thai chay
    private void ExitWalkingState(){

    }
    //KNOCKBACK STATE ---------------------

    private void EnterKnockbackState(){
        knockbackStartTime = Time.time;
        movement.Set(knockbackSpeed.x * damageDirection, knockbackSpeed.y);
        aliverb.velocity = movement;
        aliveAnim.SetBool("Knockback", true);
    }

    private void UpdateKnockbackState(){
        //thoi gian lon hon hoac = voi thoi gian go lai + voi thoi gian bat dau
        if(Time.time >= knockbackStartTime + knockbackDuration){
            SwitchState(State.Walking);
        }
    }

    private void ExitKnockbackState(){
            aliveAnim.SetBool("Knockback", false);

    }

    //DEAD STATE ---------------------

    private void EnterDeadState(){
        Instantiate(deathChunkParticle, alive.transform.position, deathChunkParticle.transform.rotation);
        Instantiate(deathBloodParticle, alive.transform.position, deathBloodParticle.transform.rotation);
        //Spawn chunks and bloodS
        Destroy(gameObject);
    }   

    private void UpdateDeadState(){

    }

    private void ExitDeadState(){

    }

    //ham chuyen doi cac trang thai khac nhau
    //OTHER FUNCTION-------------------------
        //khai bao damage
    private void Damage(float[] attackDetails){ // chuc nag message den function  gui tham so 

        currentHealth -= attackDetails[0];
        Instantiate(hitParticle, alive.transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f))); //khoi tao hit, cham khoi tao no tren vi tri bien doi truc tiep va khoi tao no o mot vong quay ngau nhien

        if(attackDetails[1] > alive.transform.position.x){
            damageDirection = -1;
        }
        else
        {
            damageDirection = 1;
        }

        //Hit particle
        if(currentHealth > 0.0f)
        {
            SwitchState(State.Knockback);
        }
        else if(currentHealth <= 0.0f)
        {
            SwitchState(State.Dead);
        }
         
    }

    //chuc nang sat thuong
    private void CheckTouchDamage(){
        //kiem tra hoi chieu
        //neu thoi gian lon hon hoac bang thoi gian gay sat thuong lan cham cuoi cung + touchdamageCooldown
        if(Time.time >= lastTouchDamageTime + touchDamageCoolDown){
            //xac dinh hai goc cua khu vuc 
            touchDamageBotLeft.Set(touchDamageCheck.position.x - (touchDamageWidth / 2), touchDamageCheck.position.y - (touchDamageHeight /2)); //do rong / 2  heck goc x+ chieu cao /2 check goc y
             touchDamageTopRight.Set(touchDamageCheck.position.x + (touchDamageWidth / 2), touchDamageCheck.position.y + (touchDamageHeight /2));

             Collider2D hit =Physics2D.OverlapArea(touchDamageBotLeft, touchDamageTopRight, whatisPlayer);

             //neu hit khong null
             if(hit != null){
                lastTouchDamageTime = Time.time;
                attackDetails[0] = touchDamage;
                attackDetails[1] = alive.transform.position.x;
                hit.SendMessage("Damage", attackDetails);
             }
        }
    }
    private void Flip(){
        facingDirection *= -1;
        alive.transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    private void SwitchState(State state){
        switch(currentState){
            case State.Walking:
                ExitWalkingState();
                break;
            case State.Knockback:
                ExitKnockbackState();
                break;
            case State.Dead:
                ExitDeadState();
                break;
        }

        switch(state){
            case State.Walking:
                EnterWalkingState();
                break;
            case State.Knockback:
                EnterKnockbackState();
                break;
            case State.Dead:
                EnterDeadState();
                break;
        }

        currentState = state;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));

        Vector2 botLeft = new Vector2(touchDamageCheck.position.x - (touchDamageWidth / 2), touchDamageCheck.position.y - (touchDamageHeight /2));
        Vector2 botRight = new Vector2(touchDamageCheck.position.x + (touchDamageWidth / 2), touchDamageCheck.position.y - (touchDamageHeight /2));
        Vector2 topRight = new Vector2(touchDamageCheck.position.x + (touchDamageWidth / 2), touchDamageCheck.position.y + (touchDamageHeight /2));
        Vector2 topLeft =  new Vector2(touchDamageCheck.position.x - (touchDamageWidth / 2), touchDamageCheck.position.y + (touchDamageHeight /2));

        Gizmos.DrawLine(botLeft, botRight);
        Gizmos.DrawLine(botRight, topRight);
        Gizmos.DrawLine(topRight, topLeft);
        Gizmos.DrawLine(topLeft, botLeft);

    }
}
