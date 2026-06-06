using Unity.VisualScripting;
using UnityEngine;

public class SpriteYAdjustZ : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float zOffset;

    // Update is called once per frame
    void Update()
    {
        float zValue = target.position.y + zOffset;
        Vector3 pos = target.position;
        pos.z = zValue;
        target.position = pos;
    }
}
