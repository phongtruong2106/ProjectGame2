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
        public Animator Anim {get; private set;}
        public PlayerInputHandel InputHandler{get; private set;}
        
        public Rigidbody2D RB{get; private set;}
        public Transform DashDirectionIndicator{get; private set;}
        public BoxCollider2D MovementCollider {get; private set;}
        public PlayerInventory Inventory{get; private set;}

    #endregion
    
    #region Check Transforms
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private Transform wallCheck;
    [SerializeField]
    private Transform ledgeCheck;
    [SerializeField]
    private Transform cellingCheck;
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
            WallSlideState = new PlayerWallSlideState(this, StateMachine, playerData, "wallSlide");
            WallGrabState = new PlayerWallGrabState(this, StateMachine, playerData, "wallGrab");
            WallClimbState = new PlayerWallClimbState(this, StateMachine, playerData, "wallClimb");
            WallJumpState = new PlayerWallJumpState(this,StateMachine, playerData, "inAir");
            LedgeClimbState = new PlayerLedgeClimbState(this, StateMachine, playerData,"ledgeClimbState");
            DashState = new PlayerDashState(this, StateMachine, playerData, "inAir");
            CrouchIdleState = new PlayerCrouchIdleState(this, StateMachine, playerData, "crouchIdle");
            CrouchMoveState = new PlayerCrouchMoveState(this, StateMachine, playerData, "crouchMove");
            PrimaryAttackState = new PlayerAttackState(this, StateMachine, playerData, "attack");
            SecondaryAttackState = new PlayerAttackState(this, StateMachine, playerData, "attack");
        }

        private void Start() {
            Anim = GetComponent<Animator>();
            InputHandler = GetComponent<PlayerInputHandel>();
            RB = GetComponent<Rigidbody2D>();
            DashDirectionIndicator = transform.Find("DashDirectionIndicator"); //tim den Object Co ten DashDirectionIndicator
            MovementCollider = GetComponent<BoxCollider2D>();
            Inventory = GetComponent<PlayerInventory>();
            

            FacingDirection  = 1;

            PrimaryAttackState.SetWeapon(Inventory.weapons[(int)CombatInput.primary]);
            // SecondaryAttackState.SetWeapon(Inventory.weapons[(int)CombatInput.primary]);

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

        public void SetVelocityZero()
        {
            RB.velocity = Vector2.zero;
            CurrentVelocity = Vector2.zero;
        }
        public void SetVelocity(float velocity,Vector2 angle, int direction) //cai dat van toc
        {
            // lam binh thuong hoa angle
            angle.Normalize();
            workspace.Set(angle.x * velocity * direction, angle.y *velocity);
            RB.velocity= workspace;
            CurrentVelocity = workspace;
        } 

        public void SetVelocity(float velocity, Vector2 direction)
        {
            workspace = direction * velocity;
            RB.velocity = workspace;
            CurrentVelocity = workspace;
        }

        public void SetVelocityX(float velocity) //cai dat van toc huong duy chuyen truc x
        {
            workspace.Set(velocity, CurrentVelocity.y);
            RB.velocity = workspace;
            CurrentVelocity = workspace;
        }

        public void SetVelocityY(float velocity) //cai dat van toc huong duy chuyen truc Y
        {
            workspace.Set(CurrentVelocity.x, velocity);
            RB.velocity = workspace;
            CurrentVelocity = workspace;
        }
    #endregion
    
    #region Check Functions
    public bool CheckForCelling() //kiem tra tran nhan( hoac 1 thu tuong tu nhu tran nha)
    {
        return Physics2D.OverlapCircle(cellingCheck.position, playerData.groundCheckCeilingRadius, playerData.whatIsGround);
    }
    public bool CheckIfGrounded() //kiem tra Cham ground
    {
        return Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.whatIsGround);
    }

    public bool CheckIfTouchingWall() //kiem tra cham wall
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, playerData.wallCheckDistance, playerData.whatIsGround);   
    }

    public bool CheckIfTouchingLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.right * FacingDirection, playerData.wallCheckDistance, playerData.whatIsGround);
    }

    //kiem tra cham wall back
    public bool CheckIfTouchingWallBack()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * -FacingDirection, playerData.wallCheckDistance, playerData.whatIsGround);
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

        public void SetColliderHeight(float height)
        {
            Vector2 center = MovementCollider.offset;
            workspace.Set(MovementCollider.size.x, height);

            //dieu chinh chieu cao nhu mong muon
            center.y += (height - MovementCollider.size.y) / 2;

            MovementCollider.size = workspace;
            MovementCollider.offset = center;
        }
        public Vector2 DetermineCornerPosition()
        {
            RaycastHit2D xHit = Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, playerData.wallCheckDistance, playerData.whatIsGround);
            float xDist = xHit.distance;
            workspace.Set((xDist + 0.015f) * FacingDirection, 0f);
            //truy cap ve RAYCAST 
            RaycastHit2D yHit = Physics2D.Raycast(ledgeCheck.position + (Vector3)(workspace), Vector2.down, ledgeCheck.position.y - wallCheck.position.y + 0.015f, playerData.whatIsGround);
            float yDist= yHit.distance;

            workspace.Set(wallCheck.position.x + (xDist * FacingDirection), ledgeCheck.position.y - yDist); //thiet lap vi tri chinh xac cua ledge
            return workspace;//tra ve workspace
        }
        private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

        private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
        private void Flip()
        {
            FacingDirection *= -1;
            transform.Rotate(0.0f, 180.0f, 0.0f);
        }
    #endregion
}
