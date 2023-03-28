using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandel : MonoBehaviour
{
    private PlayerInput playerInput;
    public Vector2 RawMovementInput{ get; private set;}
    public int NormInputX{get; private set;}
    public int NormInputY{get; private set;}
    public bool JumpInput{get; private set;}
    public bool JumpInputStop{get; private set;}
    public bool GrabInput {get; private set;}



    [SerializeField]
    private float inputHoldTime = 0.2f;

    private float jumpInputStartTime;

    private void Start() {
        playerInput = GetComponent<PlayerInput>();

    }

    private void Update() {
        //kiem tra thoi gian bat dau vao so voi thoi gian hien tai
        CheckJumpInputHoldTime();
    }
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();

        if(Mathf.Abs(RawMovementInput.x) > 0.5f)
        {
            NormInputX =(int)(RawMovementInput * Vector2.right).normalized.x;
        }
        else
        {
            NormInputX = 0;
        }
        if(Mathf.Abs(RawMovementInput.y) > 0.5f)
        {
            NormInputY = (int)(RawMovementInput * Vector2.down).normalized.y;
        }
        else
        {
            NormInputY = 0;
        }
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

    //tao phuong thuc su dung Input Jump
    public void UseJumpInput() => JumpInput = false;

    //kiem tra thoi gian dau vao 
    private void CheckJumpInputHoldTime()
    {
        if(Time.time >= jumpInputStartTime + inputHoldTime)
        {
            JumpInput = false;
        }
    }
}
