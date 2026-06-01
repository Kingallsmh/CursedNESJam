using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class JetpackMovement : MonoBehaviour
{
    [SerializeField] Physics2DModule physics2DModule;
    [SerializeField] float verticalSpeedTarget;
    [SerializeField] float verticalAccel;

    [SerializeField] UnityEvent2 onUp;
    [SerializeField] UnityEvent2 onStop;

    bool allowedJet = true;
    ButtonState input;

    private void FixedUpdate()
    {
        if (input.IsHeld())
        {
            if (physics2DModule.GetVelocityMagnitudeInDirection(Vector3.up) + verticalAccel > verticalSpeedTarget)
            {
                physics2DModule.AddVelocityNeededforTarget(Vector2.up * verticalSpeedTarget);
            }
            else
            {
                physics2DModule.AddVelocity(Vector2.up * verticalAccel);
            }
        }
        
        if (input.IsHeld()) { onUp.Invoke(); }
        else { onStop.Invoke(); }
    }

    public void SetInput(ButtonState button)
    {
        if (allowedJet == false) { return; }
        input = button;
    }
}
