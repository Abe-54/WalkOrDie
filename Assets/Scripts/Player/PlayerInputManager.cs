using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    public Vector2 RawMoveInput;
    public int NormInputX;
    public int NormInputY;
    public bool JumpInput;
    public bool JumpInputStop;
    public bool ShootInput;

    [SerializeField]
    private float _inputHoldTime = 0.2f;

    private float _jumpInputStartTime;

    private void Update()
    {
        CheckJumpInputHoldTime();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        RawMoveInput = context.ReadValue<Vector2>();

        if (Mathf.Abs(RawMoveInput.x) > 0.5f)
        {
            NormInputX = (int)(RawMoveInput * Vector2.right).normalized.x;
        }
        else
        {
            NormInputX = 0;
        }

        if (Mathf.Abs(RawMoveInput.y) > 0.5f)
        {
            NormInputY = (int)(RawMoveInput * Vector2.up).normalized.y;
        }
        else
        {
            NormInputY = 0;
        }
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            JumpInput = true;
            JumpInputStop = false;
            _jumpInputStartTime = Time.time;
        }

        if (context.canceled)
        {
            JumpInputStop = true;
        }
    }

    public void OnShootInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ShootInput = true;
        }

        if (context.canceled)
        {
            ShootInput = false;
        }
    }

    public void UseJumpInput() => JumpInput = false;

    private void CheckJumpInputHoldTime()
    {
        if (Time.time >= _jumpInputStartTime + _inputHoldTime)
        {
            JumpInput = false;
        }
    }
}