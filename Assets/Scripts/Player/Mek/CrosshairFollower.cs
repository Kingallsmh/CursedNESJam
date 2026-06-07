using Sirenix.OdinInspector;
using UnityEngine;

/// <summary>
/// Move the crosshair to follow the player's x axis while allowing 
/// crosshairs to y axis to be controlled independently. 
/// Settings:
/// xMovementPositionDistance: The x axis can position slightly ahead of the player's x position.
/// yMovementPositionSpeed: How fast the reticle can be moved up or down
/// reticleRange: The Y position is globally clamped between given range.
/// </summary>
public class CrosshairFollower : MonoBehaviour
{
    [SerializeField] Transform reticle;
    [FoldoutGroup("Settings")]
    [SerializeField] float xMovementPositionDistance = 1;
    [FoldoutGroup("Settings")]
    [SerializeField] float yMovementPositionSpeed = 1;
    [FoldoutGroup("Settings")]
    [SerializeField] Vector2 reticleRange;

    Vector2 input;
    float currentYPosition;

    // Update is called once per frame
    void Update()
    {
        Vector3 reticlePosition;
        reticlePosition.x = HandleXPosition();
        reticlePosition.y = HandleYPosition();
        reticlePosition.z = reticle.position.z;
        reticle.position = reticlePosition;
    }

    float HandleXPosition()
    {
        return transform.position.x + (input.x * xMovementPositionDistance);
    }

    float HandleYPosition()
    {
        float addedValue = yMovementPositionSpeed * input.y * Time.deltaTime;
        currentYPosition = Mathf.Clamp(currentYPosition + addedValue, reticleRange.x, reticleRange.y);
        return currentYPosition;
    }

    public void SetInput(Vector2 direction)
    {
        input.x = direction.x == 0 ? 0 : Mathf.Sign(direction.x);
        input.y = direction.y == 0 ? 0 : Mathf.Sign(direction.y);
    }
}
