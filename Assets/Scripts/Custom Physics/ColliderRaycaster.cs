using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderRaycaster : MonoBehaviour
{
    [SerializeField] BoxCollider2D boxCollider;

    [FoldoutGroup("Properties")]
    [SerializeField] float skinWidth = 0.015f;
    [FoldoutGroup("Properties")]
    [SerializeField] LayerMask defaultLayers;
    [FoldoutGroup("Properties")]
    [SerializeField] int horizontalRaycount = 4;
    [FoldoutGroup("Properties")]
    [SerializeField] int verticalRaycount = 4;

    float horizontalRaySpacing;
    float verticalRaySpacing;
    RayCastOrigins rayCastOrigins;

    private void Update()
    {
        UpdateRaycastOrigins();
        CalculateRaySpacing();

        //for(int i = 0; i < verticalRaycount; i++)
        //{
        //    Debug.DrawRay(rayCastOrigins.bottomLeft + Vector2.right * verticalRaySpacing * i, Vector2.up * -2, Color.red);
        //}
    }

    void UpdateRaycastOrigins()
    {
        Bounds bounds = boxCollider.bounds;
        bounds.Expand(skinWidth * -2);

        rayCastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        rayCastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        rayCastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        rayCastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    void CalculateRaySpacing()
    {
        Bounds bounds = boxCollider.bounds;
        bounds.Expand(skinWidth * -2);

        horizontalRaycount = Mathf.Clamp(horizontalRaycount, 2, int.MaxValue);
        verticalRaycount = Mathf.Clamp(verticalRaycount, 2, int.MaxValue);

        horizontalRaySpacing = bounds.size.y / (horizontalRaycount - 1);
        verticalRaySpacing = bounds.size.x / (verticalRaycount - 1);
    }

    public RaycastHit2D[] GetRaycastHit2D(Vector2 direction, int rays)
    {
        return GetRaycastHit2D(direction, rays, defaultLayers);
    }

    public RaycastHit2D[] GetRaycastHit2D(Vector2 direction, int rays, LayerMask layer)
    {
        RaycastHit2D[] hits = new RaycastHit2D[rays];
        Bounds bounds = boxCollider.bounds;
        Vector2 perpendicularDirection = Vector2.Perpendicular(direction.normalized);
        Vector2 pos1 = (Vector2)bounds.center + (direction.normalized * bounds.extents) + (direction.normalized * -skinWidth) +
        (perpendicularDirection * bounds.extents) + (perpendicularDirection * 0.1f);
        for (int i = 0; i < rays; i++)
        {
            Vector2 pos = pos1 - (perpendicularDirection * bounds.size + (perpendicularDirection * 0.2f)) * ((float)i / ((float)rays - 1));
            Debug.DrawRay(pos, direction, Color.yellow);
            hits[i] = Physics2D.Raycast(pos, direction, direction.magnitude, layer);
        }
        return hits;
    }

    //public RaycastHit2D[] GetRaycastHit2D(Vector2 direction, int rays, LayerMask layer)
    //{        
    //    RaycastHit2D[] hits = new RaycastHit2D[rays];
    //    Bounds bounds = boxCollider.bounds;
    //    bounds.Expand(skinWidth * -2);
    //    Vector2 perpendicularDirection = Vector2.Perpendicular(direction.normalized);
    //    Vector2 pos1 = (Vector2)bounds.center + (direction.normalized * bounds.extents) +
    //    (perpendicularDirection * bounds.extents);
    //    for (int i = 0; i < rays; i++) 
    //    {
    //        Vector2 pos = pos1 - (perpendicularDirection * bounds.size) * ((float)i / ((float)rays - 1));
    //        Debug.DrawRay(pos, direction, Color.yellow);
    //        hits[i] = Physics2D.Raycast(pos, direction, direction.magnitude, layer);
    //    }
    //    return hits;
    //}    

    struct RayCastOrigins
    {
        public Vector2 topLeft, topRight, bottomLeft, bottomRight;
    }
}
