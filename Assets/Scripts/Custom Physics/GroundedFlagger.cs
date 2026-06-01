using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GroundedFlagger : MonoBehaviour
{
    [SerializeField] Collider2D entityCollider;
    [SerializeField] float skinWidth = 0.1f;
    [SerializeField] float groundCheckLength = 0.1f;
    [SerializeField] LayerMask layersToGround;

    [FoldoutGroup("Events")]
    [SerializeField] UnityEvent2 onGrounded;
    [FoldoutGroup("Events")]
    [SerializeField] UnityEvent2 onUngrounded;
    [FoldoutGroup("Events")]
    [SerializeField] UnityEvent<RaycastHit2D> onGroundDetails;

    bool lastGrounded;

    private void OnEnable()
    {
        RaycastHit2D groundHit = CheckForGround();
        bool isGroundFound = groundHit.collider;
        if (groundHit.collider != null)
        {
            onGrounded.Invoke();
        }
        else
        {
            onUngrounded.Invoke();
        }
        lastGrounded = isGroundFound;
    }

    private void FixedUpdate()
    {
        GroundedUpdate();
    }

    public void GroundedUpdate()
    {
        RaycastHit2D groundHit = CheckForGround();
        bool isGroundFound = groundHit.collider;
        onGroundDetails.Invoke(groundHit);

        if (isGroundFound == lastGrounded) { return; }
        if (groundHit.collider != null)
        {
            onGrounded.Invoke();
        }
        else
        {
            onUngrounded.Invoke();
        }
        lastGrounded = isGroundFound;
    }

    RaycastHit2D CheckForGround()
    {
        Bounds groundBounds = entityCollider.bounds;
        groundBounds.Expand(-skinWidth);
        return Physics2D.BoxCast(groundBounds.center,
            groundBounds.size, 0f, Vector2.down, groundCheckLength, layersToGround);
    }
}
