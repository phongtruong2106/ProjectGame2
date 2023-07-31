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
    public PlayerWallSlideState WallSlideState{get; private set;}
    public PlayerWallGrabState WallGrabState{get; private set;}
    public PlayerWallClimbState WallClimbState{get; private set;}
    public PlayerWallJumpState WallJumpState{get; private set;}
    public PlayerLedgeClimbState LedgeClimbState{get; private set;}
    public PlayerDashState DashState{get; private set;}
    public PlayerCrouchIdleState CrouchIdleState{get; private set;}
    public PlayerCrouchMoveState CrouchMoveState{get; private set;}

    //Attack State
    public PlayerAttackState PrimaryAttackState{get; private set;}
    public PlayerAttackState SecondaryAttackState{get; private set;}


    [SerializeField]
    private PlayerData playerData;
    #endregion

    #region Components
        //trang thai animation
        public Core Core{get; private set;}
        public Animator Anim {get; private set;}
        public PlayerInputHandel InputHandler{get; private set;}
        
        public Rigidbody2D RB{get; private set;}
        public Transform DashDirectionIndicator{get; private set;}
        public BoxCollider2D MovementCollider {get; private set;}
        public PlayerInventory Inventory{get; private set;}

    #endregion
    #region Other Variable
  
    private Vector2 workspace;
    #endregion

    #region Unity Callback Functions
        private void Awake() {
            //bat cu khi nao tro choi bat dau , se co state Machine cho player 

            Core = GetComponentInChildren<Core>();
            StateMachine = new PlayerStateMachine();

            IdieState = new PlayerIdleState(this, StateMachine, playerData, "idie");
            MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
            JumpState = new PlayerJumpState(this, StateMachine, playerData, "inAir");
            InAirState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
            LandState = new PlayerLandState(this, StateMachine, playerData, "land");    
            WallSlideState = new PlayerWallSlideState(this, StateMachine, playerData, "wallSlide");
            WallGrabState = new PlayerWallGrabState(this, StateMachine, playerData, "wallGrab");
            WallClimbState = new PlayerWallClimbState(this, StateMachine, playerData, "wallClimb");
            WallJumpState = new PlayerWallJumpState(this,StateMachine, playerData, "inAir");
            LedgeClimbState = new PlayerLedgeClimbState(this, StateMachine, playerData,"ledgeClimbState");
            DashState = new PlayerDashState(this, StateMachine, playerData, "inAir");
            CrouchIdleState = new PlayerCrouchIdleState(this, StateMachine, playerData, "crouchIdle");
            CrouchMoveState = new PlayerCrouchMoveState(this, StateMachine, playerData, "crouchMove");
            PrimaryAttackState = new PlayerAttackState(this, StateMachine, playerData, "attack");
            
        }

        private void Start() {
            Anim = GetComponent<Animator>();
            InputHandler = GetComponent<PlayerInputHandel>();
            RB = GetComponent<Rigidbody2D>();
            DashDirectionIndicator = transform.Find("DashDirectionIndicator"); //tim den Object Co ten DashDirectionIndicator
            MovementCollider = GetComponent<BoxCollider2D>();
            Inventory = GetComponent<PlayerInventory>();


            PrimaryAttackState.SetWeapon(Inventory.weapons[(int)CombatInput.primary]);
            // SecondaryAttackState.SetWeapon(Inventory.weapons[(int)CombatInput.primary]);

            StateMachine.Initialize(IdieState);
        }

        private void Update() {
           Core.LogicUpDate();
            StateMachine.CurrentState.LogicUpdate(); //cham trang thai hien tai 
        }

        //tao ban cap nhat co dinh 
        private void FixedUpdate() {
            StateMachine.CurrentState.PhysicsUpdate();
        }
    #endregion  
    #region Other Functions

        public void SetColliderHeight(float height)
        {
            Vector2 center = MovementCollider.offset;
            workspace.Set(MovementCollider.size.x, height);

            //dieu chinh chieu cao nhu mong muon
            center.y += (height - MovementCollider.size.y) / 2;

            MovementCollider.size = workspace;
            MovementCollider.offset = center;
        }
        private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

        private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
      
    #endregion
}
