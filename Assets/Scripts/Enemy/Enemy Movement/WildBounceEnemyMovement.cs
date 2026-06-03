using Sirenix.OdinInspector;
using UnityEngine;

public class WildBounceEnemyMovement : EnemyMovementStyle
{
    #region Variables
    [Title("Direction Movement Impact: ")]
    [SerializeField]
    bool m_randomStartDirection = true;

    [SerializeField]
    [HideIf(nameof(m_randomStartDirection))]
    Vector2 m_startDirection = Vector2.right;
    #endregion

    #region Methods
    public override void SetUp()
    {
        if (!m_randomStartDirection)
        {
            SetDirection(m_startDirection);
            return;
        }

        SetRandomDirection();
    }

    public override void CollidedHandling()
    {
        Vector2 newDirection = Vector2.Reflect(CurrentDirection, CollisionHitRef.contacts[0].normal).normalized;

        if (CurrentDirection == newDirection)
        {
            Debug.Log("the directions are the same! Ew!");
            newDirection *= -1f;  // Might not be perfect, but stops it from trying to force through something (bugfix)
        }

        SetDirection(newDirection);
    }

    [Button]
    [FoldoutGroup("Testing")]
    public void RandomDirection() => SetRandomDirection();

    [Button]
    [FoldoutGroup("Testing")]
    public void Direction(Vector2 newDirection) => SetDirection(newDirection);
    #endregion
}
