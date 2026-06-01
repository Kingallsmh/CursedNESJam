using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

//Todo: Jump buffering: allow a certain amount of time before the player hits the ground to jump once the ground has been touched
//Todo: Add Coyote time: allow for some time after the ground has been lost to jump
public class JumpMovement : MonoBehaviour
{
    [SerializeField] Physics2DModule physics2DModule;
    [SerializeField] float verticalSpeed;
    [SerializeField] float coyoteTime = 0.1f;
    [SerializeField] float jumpCutTime = 0.1f;

    [FoldoutGroup("Events")]
    [SerializeField] UnityEvent2 onJump;

    [ShowInInspector]
    [FoldoutGroup("Debug")]
    ButtonState input;
    [ShowInInspector]
    [FoldoutGroup("Debug")]
    bool isGrounded;
    [ShowInInspector]
    [FoldoutGroup("Debug")]
    bool isJumping;
    [ShowInInspector]
    [FoldoutGroup("Debug")]
    bool isJumpFall;
    [ShowInInspector]
    [FoldoutGroup("Debug")]
    bool allowedJump = true;

    Timer timerJumpCut = new Timer();
    Timer timerCoyote = new Timer();

    public bool AllowedJump { get => allowedJump; set => allowedJump = value; }

    private void Update()
    {
        timerJumpCut.Update();
        timerCoyote.Update();
        StateWatch();
    }

    void StateWatch()
    {
        if (IsJumpFalling())
        {
            isJumping = false;
            isJumpFall = true;
        }
    }

    public void SetInput(ButtonState state)
    {
        input = state;
        HandleJump();
    }

    void HandleJump()
    {
        if (allowedJump == false) { return; }
        if (CanJump() && input.IsFirstDown())
        {
            physics2DModule.AddVelocityNeededforTarget(Vector2.up * verticalSpeed);
            isJumping = true;
            timerJumpCut.SetTime(jumpCutTime);
            onJump.Invoke();
        }
        else if (input.IsHeld() == false && CanJumpCut())
        {
            StartCoroutine(CR_JumpCut(timerJumpCut.GetTimeLeft()));
        }
    }

    bool CanJump()
    {
        return (isGrounded || timerCoyote.Done()==false) && isJumping==false && isJumpFall==false;
    }

    bool CanJumpCut()
    {
        return isJumping &&  physics2DModule.CurrentVelocity.y > 0;
    }

    IEnumerator CR_JumpCut(float timeLeft)
    {
        yield return new WaitForSeconds(timeLeft);
        physics2DModule.SetYVelocity(0);
    }

    bool IsJumpFalling()
    {
        return isJumping && physics2DModule.CurrentVelocity.y < 0;
    }

    public void SetGround(RaycastHit2D hit)
    {
        bool previousGrounded = isGrounded;
        isGrounded = hit.normal.magnitude > 0 ? true : false;
        if(previousGrounded == true && isGrounded == false)
        {            
            timerCoyote.SetTime(coyoteTime);
        }
        if (isGrounded == true)
        {
            isJumpFall = false;
        }
    }
}
