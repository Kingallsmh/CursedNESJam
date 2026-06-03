using UnityEngine;

/// <summary>
/// This is the enemy movement abstract class, since all the enemy 
/// movement styles/types pull from similar/the same approach. Pretty neato!
/// </summary>
public abstract class EnemyMovementStyle
{
    #region Variables
    public float Speed { get; set; }
    public float PreviousSpeed { get; set; }
    public Vector2 CurrentDirection { get; set; }
    public Rigidbody2D RigidbodyRef { get; set; }
    public Collision2D CollisionHitRef { get; set; }

    bool m_doUpdate = true;
    #endregion

    #region Methods
    public virtual void SetUp()
    {
        // Handle direction applied here, etc
        // Basics is nothing, but we build off of it in other ones
    }

    public virtual void UpdateMovement()
    {
        if (!m_doUpdate) return;

        RigidbodyRef.linearVelocity = Speed * CurrentDirection;
    }

    public virtual void CollidedHandling()
    {
        // Can override, otherwise just swap movement direction
        CurrentDirection *= -1f;
    }

    public void SetCollisionHitDetails(Collision2D hit) => CollisionHitRef = hit;

    public void SetNewSpeed(float newSpeed)
    {
        Speed = newSpeed;
    }

    // Not used yet, but could be handy
    public void PauseMovement(bool pause)
    {
        if (pause)
        {
            PreviousSpeed = Speed;
            Speed = 0f;
            m_doUpdate = false;
            return;
        }

        Speed = PreviousSpeed;
        m_doUpdate = true;
    }

    // Not used yet, but could be handy?
    public void ResetAndStopMovement()
    {
        Speed = 0f;
        PreviousSpeed = 0f;
        CurrentDirection = Vector2.zero;
        RigidbodyRef.angularVelocity = 0f;
    }

    public void SetDirection(Vector2 newDirection)
    {
        CurrentDirection = newDirection;
    }

    public void SetRandomDirection()
    {
        float randomX = Random.Range(-1.5f, 1.5f);
        float randomY = Random.Range(-1.5f, 1.5f);

        randomX = StopRandomFromGivingZeroValue(randomX);
        randomY = StopRandomFromGivingZeroValue(randomY);

        SetDirection(new Vector2(randomX, randomY));
    }

    float StopRandomFromGivingZeroValue(float number)
    {
        if (number >= 0 && number < 0.65f)
            return 0.65f;

        if (number <= 0 && number > -0.65f)
            return -0.65f;

        return 1f;
    }
    #endregion
}
