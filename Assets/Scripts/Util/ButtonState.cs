using System;
using UnityEngine;

[Serializable]
public struct ButtonState
{
    [SerializeField] bool isPressed;
    [SerializeField] bool wasPressed;

    public bool IsPressed { get => isPressed; set => isPressed = value; }
    public bool WasPressed { get => wasPressed; set => wasPressed = value; }


    public void UpdatePressed(bool isPressed)
    {
        UpdatePressed();
        this.isPressed = isPressed;
    }

    public void UpdatePressed()
    {
        wasPressed = this.isPressed;
    }

    public void Reset()
    {
        isPressed = false;
        wasPressed = false;
    }

    public bool IsFirstDown()
    {
        return isPressed == true && wasPressed == false;
    }

    public bool IsHeld()
    {
        return isPressed == true && wasPressed == true;
    }

    public bool IsFirstUp()
    {
        return isPressed == false && wasPressed == true;
    }

    public bool IsUp()
    {
        return isPressed == false && wasPressed == false;
    }

    //public void SetState(InputAction ctx)
    //{
    //    isDown = ctx.WasPressedThisFrame();
    //    isHeld = ctx.IsPressed();
    //    isUp = ctx.WasReleasedThisFrame();
    //}

    //public void SetState(InputAction.CallbackContext ctx)
    //{
    //    isDown = ctx.action.WasPressedThisFrame();
    //    isHeld = ctx.action.IsPressed();
    //    isUp = ctx.action.WasReleasedThisFrame();
    //}
}
