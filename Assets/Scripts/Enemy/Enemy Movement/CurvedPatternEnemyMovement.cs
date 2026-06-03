using Sirenix.OdinInspector;
using UnityEngine;
using System;

public class CurvedPatternEnemyMovement : EnemyMovementStyle
{
    #region Variables
    [Title("Movement Style: ")]
    [SerializeField]
    MovementAxis m_direction;
    [SerializeField]
    EnemyMovementCurveImpact m_curveImpact;

    [Title("Curve Impact Details: ")]
    [SerializeField]
    float m_curveImpactAmount = 1f;
    [SerializeField]
    float m_curveSpeedAmount = 0.001f;
    [SerializeField]
    AnimationCurve m_movementCurve;

    Keyframe m_lastKey;
    float m_lastKeyTime;
    float m_resultAmount;
    float m_currentTime;
    Vector2 m_resultVector = Vector2.zero;
    #endregion

    #region Methods
    public override void SetUp()
    {
        switch (m_direction)
        {
            case MovementAxis.HORIZONTAL_POS:
                CurrentDirection = Vector2.right;
                break;
            case MovementAxis.HORIZONTAL_NEG:
                CurrentDirection = Vector2.left;
                break;
            case MovementAxis.VERTICAL_POS:
                CurrentDirection = Vector2.up;
                break;
            case MovementAxis.VERTICAL_NEG:
                CurrentDirection = Vector2.down;
                break;
        }
    }

    public override void UpdateMovement()
    {
        m_currentTime += Time.deltaTime * m_curveSpeedAmount;
        KeepTimeWithinCurveDuration();

        CalculateCurveImpactOnMovement();
        RigidbodyRef.linearVelocity = Speed * CurrentDirection + m_resultVector;
    }

    void CalculateCurveImpactOnMovement()
    {
        // If we wanted to add more to this, we could change the lerp or play around with this calculation line
        m_resultAmount = Mathf.Lerp(-m_curveImpactAmount, m_curveImpactAmount, m_movementCurve.Evaluate(m_currentTime));
        m_resultVector = Vector2.zero;

        switch (m_curveImpact)
        {
            case EnemyMovementCurveImpact.AXIS_X:
                m_resultVector.x = m_resultAmount;
                // Keeps it mirrored
                if (CurrentDirection.x < 0)
                    m_resultVector *= -1f;
                break;
            case EnemyMovementCurveImpact.AXIS_Y:
                m_resultVector.y = m_resultAmount;
                // Keeps it mirrored
                if (CurrentDirection.y < 0)
                    m_resultVector *= -1f;
                break;
        }
    }

    void KeepTimeWithinCurveDuration()
    {
        m_lastKey = m_movementCurve[m_movementCurve.length - 1];
        m_lastKeyTime = m_lastKey.time;

        if (m_currentTime >= m_lastKeyTime)
            m_currentTime = 0f;
    }
    #endregion
}

[Serializable]
enum EnemyMovementCurveImpact
{
    AXIS_X,
    AXIS_Y
}
