using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Move the transform z position based on y so that they will automatically layer properly
/// </summary>
public class SpriteYAdjustZ : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float zOffset;

    
    void Update()
    {
        float zValue = target.position.y + zOffset;
        Vector3 pos = target.position;
        pos.z = zValue;
        target.position = pos;
    }
}
