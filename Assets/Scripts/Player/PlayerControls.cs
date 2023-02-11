
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerControls : MonoBehaviour
{

    //huong quay 
    private int facingDirection = 1;
    private int amountOfJumpsLeft;
    //theo doi huong cuoi cung ma ta da nhay
    private int lastWallJumpDirection;

    //bo dem thoi gian  
    private float jumpTimer;
    private float movementInputDirection;
    private float turnTimer;
    private float wallJumpTimer;
    private float dashTimeLeft;
    private float lastImageXpos;
    private float lastDash = -100f;
    private float knockbackStartTime;
    [SerializeField]
    private float knockbackDuration;

    private Rigidbody2D rb;
    private Animator anim;

    private bool isTouchingWall;
    private bool isWalking;
    private bool isGrounded;
    private bool isFacingRinght = true;
    private bool canNormalJump;
    private bool canWallJump;
    //kiem tra xem co thuc su dang bam vao tuong hay khong
    private bool isWallSliding;
    private bool isAttemptingTojump;
    private bool checkJumpMultiplier;
    //duy chuyen and xoay huong
    private bool canMove;
    private bool canFlip;
    private bool hasWallJumped;
    private bool isTouchingLedge;
    //co the leo go
    private bool canCLimbLedge = false;
    private bool ledgeDetected;
    private bool isDashing;
    private bool knockback;
    
    [SerializeField]
    private Vector2 knockbackSpeed;

    private Vector2 ledgePosBot;
    private Vector2 ledgePos1;
    private Vector2 ledgePos2;


    [Header("Setting Player")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float MovementSpeed;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private float movementForceInAir;
    [SerializeField] private float ariDragMultiplier = 0.95f;
    [SerializeField] private float variableJumpHeightMultiplier = 0.5f;
    [Header("gioi han van toc")]
    [SerializeField] private float wallSlideSpeed;
    [Header("thiet lap timer")]
    [SerializeField] private float jumpTimerSet = 0.15f;
    //chieu cao nhay
    [Header("luc nhay tuong")]
    [SerializeField] private float wallHopeForce;
    [SerializeField] private float wallJumpForce;
    [SerializeField] private float turnTimerSet = 0.1f;
    [Header("khoang cach tuong")]
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private float wallJumpTimerSet = 0.5f;
    //tao toa do
    [SerializeField] private float ledgeClimbXOffset1 = 0f;
    [SerializeField] private float ledgeClimbYOffset1 = 0f;
    [SerializeField] private float ledgeClimbXOffset2 = 0f;
    [SerializeField] private float ledgeClimbYOffset2 = 0f;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float distanceBetweenImages;
    [SerializeField] private float dashCoolDown;


    [SerializeField] private int amountOfJumps = 1;
    [SerializeField] private Transform groundCheck;
    
    //xac dinh huong ma nhay  ra khoi
    [SerializeField] private Vector2 wallHopDirection;
    [SerializeField] private Vector2 wallJumpDirection; 
    
    [SerializeField] private LayerMask whatIsGround;

 
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform ledgeCheck; //check nguoi choi

   

    //thu nghiem
 

    //end thu nghiem


    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        amountOfJumpsLeft = amountOfJumps;
        wallHopDirection.Normalize();
        wallJumpDirection.Normalize();

    }

    private void Update() {
        CheckInput();
        CheckMovementDirection();
        UpdateAnimation();
        CheckIfCanJump();
        //kiem tra moi truong truoc
        CheckIfWallSliding();
        CheckJump();
        CheckLedgeClimd();
        CheckDash();

        CheckKnockback();

    }

    private void FixedUpdate()
    {
        ApplyMovement();
        CheckSurroundings();
    }

    private void CheckIfWallSliding(){
            //neu nos cham vao tuong va nhan vat khong tiep dat vaf van toc nhan vat <0 , nhan vat se nhay len
            if(isTouchingWall && movementInputDirection == facingDirection && rb.velocity.y < 0 && !canCLimbLedge)
            {
                isWallSliding = true;
            }   
            else{
                isWallSliding = false;
            }
    }

    public bool GetDashStatus(){
        return isDashing;
    }

    //kiem tra knockback
    public void Knockback(int direction)
    {
        knockback = true;
        knockbackStartTime = Time.time;
        rb.velocity  = new Vector2(knockbackSpeed.x * direction, knockbackSpeed.y);
    }

    //kiem tra khoang trong
    private void CheckKnockback()
    {
        if(Time.time >= knockbackStartTime + knockbackDuration && knockback)
        {
             knockback = false;
             rb.velocity = new Vector2(0.0f, rb.velocity.y);
        }
    }
    //kiem tra leo go
    private void CheckLedgeClimd(){
        if(ledgeDetected && !canCLimbLedge){
            canCLimbLedge = true;
            if(isFacingRinght)
            {
                //floot tra ve so nguyen lon nhat duoi gia tri maf minh cung cap nen no tra ve ngoai cung ben trai co go o ma
                ledgePos1 = new Vector2(Mathf.Floor(ledgePosBot.x + wallCheckDistance) - ledgeClimbXOffset1, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset1);
                //vi tri go 2
                ledgePos2 = new Vector2(Mathf.Floor(ledgePosBot.x + wallCheckDistance) + ledgeClimbXOffset2, Mathf.Floor(ledgePosBot.y) + ledgeClimbYOffset2);
            }
            else{
                ledgePos1 = new Vector2(Mathf.Ceil(ledgePosBot.x - wallCheckDistance) + ledgeClimbXOffset1, Mathf.Floor(ledgePosBot.y) + ledgeClimbXOffset1);
                ledgePos2 = new Vector2(Mathf.Ceil(ledgePosBot.x - wallCheckDistance) - ledgeClimbXOffset2, Mathf.Floor(ledgePosBot.y) + ledgeClimbXOffset2);
            }

            //vo hieu hoa chay va quay 
            canMove = false;
            canFlip = false;

            //thiet lap animation
            anim.SetBool("canClimbLegde", canCLimbLedge);
        }
    }
    public void FinishLedgeClimd()
    {
            canCLimbLedge = false;
            transform.position= ledgePos2;
            canMove = true;
            canFlip = true;
            ledgeDetected = false;

            anim.SetBool("canClimbLegde", canCLimbLedge);
    }
    private void CheckIfCanJump(){
        if(isGrounded && rb.velocity.y <= 0.01f){
           amountOfJumpsLeft = amountOfJumps; 
        }
        if(isTouchingWall){
            canWallJump = true;
        }
        if(amountOfJumpsLeft <= 0){
            canNormalJump = false;
        }
        else
        {
            canNormalJump = true;
        }
    }

    
    private void CheckSurroundings(){
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        //cham vao tuong
        //kiem tra vi tri buc tuong
        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);
        //cham vao go . vat ly 2d
        isTouchingLedge =Physics2D.Raycast(ledgeCheck.position, transform.right, wallCheckDistance, whatIsGround);
        if(isTouchingWall && !isTouchingLedge && !ledgeDetected){
            ledgeDetected = true;
            ledgePosBot = wallCheck.position;
        }
    }

    //kiem tra huong duy chuyen cua nhan vat
    private void CheckMovementDirection(){
        if(isFacingRinght && movementInputDirection < 0){
                MovFlip();
        }
        else if(!isFacingRinght && movementInputDirection > 0) {
                MovFlip();
        }

        if(Mathf.Abs(rb.velocity.x) >= 0.01f){
            isWalking = true;

        }
        else{
            isWalking = false;
        }
    }
    private void UpdateAnimation(){
        anim.SetBool("isWalking", isWalking);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isWallSliding", isWallSliding);
    }
    private void CheckInput(){
        movementInputDirection = Input.GetAxisRaw("Horizontal");

        //jump
        if(Input.GetButtonDown("Jump")){
            if(isGrounded || (amountOfJumpsLeft > 0 && isTouchingWall)){
                NormalJump();
            }
            else{
                jumpTimer = jumpTimerSet;
                isAttemptingTojump = true;
            }
        }
        if(Input.GetButtonDown("Horizontal") && isTouchingWall){ //add cmt Horizontal
            //neu khong cham dat vaf duy chuyen protection = voi huong doi dien 
            if(!isGrounded && movementInputDirection != facingDirection )
            {
                //du lieu chuyen ve false
                canMove = false;
                canFlip = false;

                turnTimer = turnTimerSet;
            }
        }

        if(turnTimer >= 0){
            turnTimer -= Time.deltaTime;
            if(turnTimer <= 0){
                canMove = true;
                canFlip = true;
            }
        }
        if(checkJumpMultiplier && !Input.GetButton("Jump"))
        {
            checkJumpMultiplier = false;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * variableJumpHeightMultiplier);
        }

        if(Input.GetButtonDown("Dash")){
            
    
            AttempToDash();
        }
    }

    private void AttempToDash(){
        isDashing = true;
        dashTimeLeft = dashTime;
        lastDash = Time.time;

        PlayerAfterImagePool.Instance.GetFromPool();
        lastImageXpos = transform.position.x;
    }

    //doi mat voi huong i]
    public int GetFacingDirection(){
        return facingDirection;
    }
    private void CheckDash(){
        if(isDashing)
        {
            if(dashTimeLeft > 0)
            {
                canMove = false;
                canFlip = false;
                rb.velocity = new Vector2(dashSpeed * facingDirection, rb.velocity.y);
                dashTimeLeft -= Time.deltaTime;

                if(Mathf.Abs(transform.position.x - lastImageXpos) > distanceBetweenImages){
                    PlayerAfterImagePool.Instance.GetFromPool();
                    lastImageXpos = transform.position.x;
                }
                if(dashTimeLeft <= 0 || isTouchingWall){
                    isDashing = false;
                    canMove = true;
                    canFlip = true;
                }
            }
        }
    }
    //goi method duy chuyen
    private void ApplyMovement()
    {
         //neu duocw ground 
        if(!isGrounded &&  !isWallSliding && movementInputDirection == 0 && !knockback){
            rb.velocity = new Vector2(rb.velocity.x * ariDragMultiplier, rb.velocity.y);
        }
        else if(canMove && !knockback)
        {
             rb.velocity = new Vector2(MovementSpeed * movementInputDirection, rb.velocity.y);
        }
      
        
         //gioi han van toc
         if(isWallSliding){
            if(rb.velocity.y < -wallSlideSpeed){
                rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
            }
         }
    }

    public void DisableFlip(){
        canFlip = false;
    }

    public void EnableFlip(){
        canFlip = true;
    }

//movement flip player
    private void MovFlip()
    {

        //doi chieu nhay chieu cham tuong
        if(!isWallSliding && canFlip && !knockback)
        {
            facingDirection *=  -1;
            isFacingRinght = !isFacingRinght;  //neu sai  -> bien doi xoay
            transform.Rotate(0.0f, 180.0f, 0.0f);

        }
    }
    //jump
    private void CheckJump()
    {
        if(jumpTimer > 0){
            //wall Jump
            if(!isGrounded && isTouchingWall && movementInputDirection != 0 && movementInputDirection != facingDirection)
            {
                    WallJump();
            }
            else if(isGrounded){
                    NormalJump();
            }
            if(isAttemptingTojump)
            {
                jumpTimer -= Time.deltaTime;
            }
            if(wallJumpTimer > 0)
            {
                if(hasWallJumped && movementInputDirection == -lastWallJumpDirection)
                {
                    rb.velocity = new Vector2(rb.velocity.x, 0.0f);
                    hasWallJumped = false;
                }
                else if(wallJumpTimer <= 0){
                    hasWallJumped = false;
                }
                else{
                    wallJumpTimer -= Time.deltaTime;
                }
            }
        }
    }
    
    //tao 2 chuc nang nhay
    ///nhay thuong
    private void NormalJump(){
        if(canNormalJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            amountOfJumpsLeft--;
            jumpTimer = 0;
            isAttemptingTojump = false;
            checkJumpMultiplier = true;
        }
    }
    ///nhay tuong
    private void WallJump(){
         if(canWallJump)
         {
            rb.velocity = new Vector2(rb.velocity.x, 0.0f);
            //chi dinh truot truong 
            isWallSliding = false;
            amountOfJumpsLeft = amountOfJumps;
            amountOfJumpsLeft--;
            Vector2 forcetoAdd = new Vector2(wallJumpForce * wallJumpDirection.x * movementInputDirection, wallJumpForce * wallJumpDirection.y);
            rb.AddForce(forcetoAdd, ForceMode2D.Impulse);
            jumpTimer = 0;
            isAttemptingTojump = false;
            checkJumpMultiplier = true;
            //hen giop quay = 0 va chuuyen huong = true
            turnTimer = 0;
            canMove = true;
            canFlip = true;
            hasWallJumped = true;
            wallJumpTimer = wallJumpTimerSet;
            lastWallJumpDirection = -facingDirection;

        }
    }
    private  void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        //ve duong bien
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
        Gizmos.DrawLine(ledgePos1, ledgePos2);
    }
}
