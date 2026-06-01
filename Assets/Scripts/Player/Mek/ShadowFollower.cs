using UnityEngine;

public class ShadowFollower : MonoBehaviour
{
    [SerializeField] Transform shadow;
    [SerializeField] LayerMask floorMask;

    private void Update()
    {
        shadow.position = FloorPoint();
    }

    Vector2 FloorPoint()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 100, floorMask);
        return hit.point;
    }
}
