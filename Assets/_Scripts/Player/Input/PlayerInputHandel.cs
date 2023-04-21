using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandel : MonoBehaviour
{
    private PlayerInput playerInput;
    private Camera cam;

    public Vector2 RawDashDirectionInput{get; private set;} //Ve Huong Dash dau vao
    public Vector2 RawMovementInput{ get; private set;}
    public Vector2Int DashDirectionInput{get; private set;}

    public int NormInputX{get; private set;}
    public int NormInputY{get; private set;}
    public bool JumpInput{get; private set;}
    public bool JumpInputStop{get; private set;}
    public bool GrabInput {get; private set;}
    public bool DashInput{get; private set;}
    public bool DashInputStop{get; private set;}

    public bool[] AttackInputs{get; private set;}


    [SerializeField]
    private float inputHoldTime = 0.2f;

    private float jumpInputStartTime;
    private float dashInputStartTime;

    private void Start() {
        playerInput = GetComponent<PlayerInput>();

        int count = Enum.GetValues(typeof(CombatInput)).Length;
        AttackInputs = new bool[count];

        cam = Camera.main;

    }

    private void Update() {
        //kiem tra thoi gian bat dau vao so voi thoi gian hien tai
        CheckJumpInputHoldTime();
        CheckDashHoldTime();
    }

    public void OnPrimaryAttackInput(InputAction.CallbackContext context)
    {
        if(context.started) //neu thanh phan nay duoc goi
        {
                AttackInputs[(int)CombatInput.primary] = true; //lay phan tju trong combat Input 

        }
        if(context.canceled)
        {
                AttackInputs[(int)CombatInput.primary] = false; 
        }
    }

    public void OnSecondaryAttackInput(InputAction.CallbackContext context)
    {
        if(context.started) //neu thanh phan nay duoc goi
        {
                AttackInputs[(int)CombatInput.secondary] = true; //lay phan tju trong combat Input 

        }
        if(context.canceled)
        {
                AttackInputs[(int)CombatInput.secondary] = false; 
        }
    }
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();

        NormInputX = Mathf.RoundToInt(RawMovementInput.x);
        NormInputY = Mathf.RoundToInt(RawMovementInput.y);
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            JumpInput = true; //khi nhan vao thi Jump = true
            JumpInputStop = false;
            jumpInputStartTime = Time.time;
        }
        if(context.canceled)
        {
            JumpInputStop = true;
        }
    }

    public void OnGrabInput(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            GrabInput = true;
        }
        if(context.canceled)
        {
            GrabInput = false;
        }
    }

    //Create Function input Dash
    public void OnDashInput(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            DashInput = true;
            DashInputStop = false;
            dashInputStartTime = Time.time;
        }
        else if(context.canceled)
        {
            DashInputStop = true;
        }
    }

    //dau vao huong Dash
    public void OnDashDirectionInput(InputAction.CallbackContext context)
    {   
          RawDashDirectionInput = context.ReadValue<Vector2>();

        if(playerInput.currentControlScheme == "Keyboard")
        {
            RawDashDirectionInput = cam.ScreenToWorldPoint(new Vector3(cam.pixelWidth - RawDashDirectionInput.x, cam.pixelHeight - RawDashDirectionInput.y, 1.9f)) - transform.position;
        }

        DashDirectionInput = Vector2Int.RoundToInt(RawDashDirectionInput.normalized);
    }

    //tao phuong thuc su dung Input Jump
    public void UseJumpInput() => JumpInput = false;

    // tao phuong thuc su dung DASHiNPUT
    public void UseDashInput() => DashInput = false; 

    //kiem tra thoi gian dau vao 
    private void CheckJumpInputHoldTime()
    {
        if(Time.time >= jumpInputStartTime + inputHoldTime)
        {
            JumpInput = false;
        }
    }

    //kiem tra thoi gian dau vao Dash
    private void CheckDashHoldTime()
    {
        if(Time.time >= dashInputStartTime + inputHoldTime)
        {
            DashInput = false;
        }
    }
}

public enum CombatInput
{
    primary,
    secondary
}
