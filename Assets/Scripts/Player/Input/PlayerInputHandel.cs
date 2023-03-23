using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandel : MonoBehaviour
{
    public Vector2 RawMovementInput{ get; private set;}
    public int NormInputX{get; private set;}
    public int NormInputY{get; private set;}
    public bool JumpInput{get; private set;}
    public bool JumpInputStop{get; private set;}

    [SerializeField]
    private float inputHoldTime = 0.2f;

    private float jumpInputStartTime;

    private void Update() {
        //kiem tra thoi gian bat dau vao so voi thoi gian hien tai
        CheckJumpInputHoldTime();
    }
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();

        NormInputX =(int)(RawMovementInput * Vector2.right).normalized.x;
        NormInputY = (int)(RawMovementInput * Vector2.up).normalized.y;
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
