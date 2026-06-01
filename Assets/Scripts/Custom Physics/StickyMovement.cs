using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyMovement : MonoBehaviour
{
    [SerializeField] Physics2DModule physics2D;
    [SerializeField] bool isSticky;

    [SerializeField] Collider2D entityCollider;
    [SerializeField] float skinWidth = 0.1f;
    [SerializeField] float groundCheckLength = 0.1f;
    [SerializeField] LayerMask layersToGround;

    Vector2 input;
    Vector2 groundSlope;

    void FixedUpdate()
    {
        if (isSticky == false) { return; }
        RaycastHit2D hit = CheckForGround((Vector2.down + input*groundSlope) * groundCheckLength);
        StickToFloor(hit);
    }

    RaycastHit2D CheckForGround(Vector2 direction)
    {
        Bounds groundBounds = entityCollider.bounds;
        groundBounds.Expand(-skinWidth);
        Bounds debugBounds = entityCollider.bounds;
        debugBounds.center += (Vector3)direction;
        DrawBounds(debugBounds);
        return Physics2D.BoxCast(groundBounds.center,
            groundBounds.size, 0f, direction.normalized, direction.magnitude, layersToGround);
    }

    public void StickToFloor(RaycastHit2D hit)
    {
        if (isSticky == false || hit.collider == null) { return; }
        Vector2 pos = physics2D.Rb.position;
        pos.y = hit.point.y;
        physics2D.Rb.position = (pos);
    }

    public void SetInput(Vector2 input)
    {
        this.input = input.normalized;
    }

    public void SetGround(RaycastHit2D hit)
    {
        groundSlope = hit.normal;
    }

    //void StickToFloor()
    //{
    //    RaycastHit2D[] hits = raycaster.GetRaycastHit2D(Vector2.down * raycastLength, rayAmount, layertoHit);
    //    float yPos = 0;
    //    bool hasUpdated = false;
    //    for(int i = 0; i < hits.Length; i++)
    //    {
    //        if(hits[i].collider == null) continue;
    //        float newYPos = hits[i].point.y;
    //        if (hasUpdated == false) 
    //        {
    //            yPos = newYPos;
    //            hasUpdated = true;

    //            Debug.Log(hits.Length);
    //        }            
    //        else if (newYPos > yPos)
    //        {
    //            yPos = newYPos;
    //        }            
    //    }

    //    if (hasUpdated)
    //    {
    //        Vector3 pos = physics2D.Rb.position;
    //        pos.y = yPos;
    //        physics2D.Rb.position = pos;
    //    }
    //}

    void DrawBounds(Bounds b, float delay = 0)
    {
        // bottom
        var p1 = new Vector3(b.min.x, b.min.y, b.min.z);
        var p2 = new Vector3(b.max.x, b.min.y, b.min.z);
        var p3 = new Vector3(b.max.x, b.min.y, b.max.z);
        var p4 = new Vector3(b.min.x, b.min.y, b.max.z);

        Debug.DrawLine(p1, p2, Color.blue, delay);
        Debug.DrawLine(p2, p3, Color.red, delay);
        Debug.DrawLine(p3, p4, Color.yellow, delay);
        Debug.DrawLine(p4, p1, Color.magenta, delay);

        // top
        var p5 = new Vector3(b.min.x, b.max.y, b.min.z);
        var p6 = new Vector3(b.max.x, b.max.y, b.min.z);
        var p7 = new Vector3(b.max.x, b.max.y, b.max.z);
        var p8 = new Vector3(b.min.x, b.max.y, b.max.z);

        Debug.DrawLine(p5, p6, Color.blue, delay);
        Debug.DrawLine(p6, p7, Color.red, delay);
        Debug.DrawLine(p7, p8, Color.yellow, delay);
        Debug.DrawLine(p8, p5, Color.magenta, delay);

        // sides
        Debug.DrawLine(p1, p5, Color.white, delay);
        Debug.DrawLine(p2, p6, Color.gray, delay);
        Debug.DrawLine(p3, p7, Color.green, delay);
        Debug.DrawLine(p4, p8, Color.cyan, delay);
    }
}
