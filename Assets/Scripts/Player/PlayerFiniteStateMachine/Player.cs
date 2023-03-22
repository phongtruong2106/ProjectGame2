using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region  State Variables
    public PlayerStateMachine StateMachine{get; private set;}

    //trang thai Idie
    public PlayerIdleState IdieState{get; private set;}
    //trang thai move
    public PlayerMoveState MoveState{get; private set;}
    //trang thai nhay
    public PlayerJumpState JumpState{get; private set;}
    //trang thai khong trung
    public PlayerInAirState InAirState{get; private set;}
    public PlayerLandState LandState{get; private set;}
    [SerializeField]
    private PlayerData playerData;
    #endregion

    #region Components
    //trang thai animation
    public Animator Anim {get; private set;}
    public PlayerInputHandel InputHandler{get; private set;}

    public Rigidbody2D RB{get; private set;}
    #endregion
    
    #region Check Tranforms 
    [SerializeField]
    private Transform groundCheck;
    #endregion

    #region Other Variable
    public Vector2 CurrentVelocity{get; private set;}
    public int FacingDirection{get; private set;}
    private Vector2 workspace;
    #endregion

    #region Unity Callback Functions
    private void Awake() {
        //bat cu khi nao tro choi bat dau , se co state Machine cho player 
        StateMachine = new PlayerStateMachine();

        IdieState = new PlayerIdleState(this, StateMachine, playerData, "idie");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "inAir");
        InAirState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
        LandState = new PlayerLandState(this, StateMachine, playerData, "land");

    }

    private void Start() {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandel>();
        RB = GetComponent<Rigidbody2D>();

        FacingDirection  = 1;

        StateMachine.Initialize(IdieState);
    }

    private void Update() {
        CurrentVelocity =RB.velocity;
        StateMachine.CurrentState.LogicUpdate(); //cham trang thai hien tai 
    }

    //tao ban cap nhat co dinh 
    private void FixedUpdate() {
        StateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion
    
    #region  Set Functions
    public void SetVelocityX(float velocity) //huong duy chuyen truc x
    {
        workspace.Set(velocity, CurrentVelocity.y);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void SetVelocityY(float velocity) //huong duy chuyen truc Y
    {
        workspace.Set(CurrentVelocity.x, velocity);
        RB.velocity = workspace;
        CurrentVelocity = workspace;
    }
    #endregion
    
    #region Check Functions
    public bool CheckIfTouchingGround() //kiem tra Cham ground
    {
        return Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.whatIsGround);
    }
    public void CheckIfShouldFlip(int XInput)
    {
        if(XInput != 0 && XInput != FacingDirection)
        {
            Flip();
        }
    }
    #endregion
    
    #region Other Functions

    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

    private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
    private void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    #endregion
}
