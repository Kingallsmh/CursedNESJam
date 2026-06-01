using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Windows;

public class Physics2DModule : BaseModule
{
    [FoldoutGroup("References")]
    [SerializeField] Rigidbody2D rb;  
    
    Vector3 velocityToApply;

    public Vector3 VelocityToApply { get => velocityToApply; }
    public Vector3 CurrentVelocity { get => rb.linearVelocity; }
    public Rigidbody2D Rb { get => rb; }

    public void AddVelocity(Vector3 velocity)
    {
        AddVelocity(new Vector2(velocity.x, velocity.y));
    }

    public void AddVelocity(Vector2 velocity)
    {
        rb.linearVelocity += (velocity);
    }

    public void AddVelocityNeededforTarget(Vector2 velocity)
    {
        float speedDif = velocity.magnitude - GetVelocityMagnitudeInDirection(velocity);
        AddVelocity(velocity.normalized * speedDif);
    }

    public void SetVelocity(Vector3 velocity)
    {
        rb.linearVelocity = velocity;
    }

    public void SetYVelocity(float yVelocity)
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, yVelocity);
    }

    public void SetXVelocity(float xVelocity)
    {
        rb.linearVelocity = new Vector2(xVelocity, rb.linearVelocity.y);
    }

    public float GetVelocityMagnitudeInDirection(Vector3 direction)
    {
        return Vector3.Dot(CurrentVelocity, direction.normalized);
    }
}
