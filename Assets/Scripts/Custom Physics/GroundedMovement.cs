using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class GroundedMovement : MonoBehaviour
{
    [SerializeField] Physics2DModule physics2DModule;
    [SerializeField] float horizontalSpeed;
    [FoldoutGroup("Events")]
    [SerializeField] UnityEvent2 onMove;
    [FoldoutGroup("Events")]
    [SerializeField] UnityEvent2 onStop;
    [FoldoutGroup("Events")]
    [SerializeField] FloatEvent onXAxisMove;
    [FoldoutGroup("Events")]
    [SerializeField] Vector2Event onMovingDirection;

    bool inAir;
    Vector2 input;
    Vector2 groundSlope = Vector2.up;
    [ShowInInspector]
    [FoldoutGroup("Debug")] float speedDif;
    [ShowInInspector]
    [FoldoutGroup("Debug")] bool isInputLocked;

    private void FixedUpdate()
    {        
        Movement();
    }

    void Movement()
    {
        Quaternion normalDirection = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.left, Vector2.Perpendicular(Vector2.up)));
        if (inAir == false)
        {
            normalDirection = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.left, Vector2.Perpendicular(groundSlope)));
        }
        
        Vector3 projectedVelocity = normalDirection * input * horizontalSpeed;
        
        if (input.x == 0)
        {
            float currentMagnitude = physics2DModule.GetVelocityMagnitudeInDirection(normalDirection * Vector2.right);
            projectedVelocity = normalDirection * Vector2.right * -currentMagnitude;
            physics2DModule.AddVelocity(projectedVelocity);
        }
        else
        {
            speedDif = projectedVelocity.magnitude - physics2DModule.GetVelocityMagnitudeInDirection(projectedVelocity);
            physics2DModule.AddVelocity(normalDirection * input * speedDif);
        }

        onMovingDirection.Invoke(input);
        onXAxisMove.Invoke(input.x);
        if (input.magnitude > 0) { onMove.Invoke(); }
        else { onStop.Invoke(); }
    }

    public void SetInput(Vector2 input)
    {
        if (isInputLocked) { return; }
        input.y = 0;
        this.input = input;
    }

    public void SetGround(RaycastHit2D hit)
    {
        SetGround(hit.normal);
    }

    public void SetGround(Vector2 slope)
    {
        groundSlope = slope;
        if (groundSlope.magnitude == 0)
        {
            groundSlope = Vector2.up;
        }
    }

    public void SetInAir(bool value)
    {
        inAir = value;
    }

    public void SetMovementLock(bool value)
    {
        if(isInputLocked == value) { return; }
        isInputLocked = value;
        input = Vector2.zero;
    }
}
