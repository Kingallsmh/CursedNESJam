using UnityEngine;

public class ReticleFollower : MonoBehaviour
{
    [SerializeField] Transform reticle;
    [SerializeField] float xMovementPositionDistance = 1;
    [SerializeField] float yMovementPositionSpeed = 1;
    [SerializeField] Vector2 reticleRange;

    [SerializeField] float minXInputValue;
    [SerializeField] float minYInputValue;

    Vector2 input;
    float currentYPosition;

    // Update is called once per frame
    void Update()
    {
        Vector2 reticlePosition;
        reticlePosition.x = HandleXPosition();
        reticlePosition.y = HandleYPosition();
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
