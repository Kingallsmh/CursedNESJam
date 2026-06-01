using Sirenix.OdinInspector;
using UnityEngine;

public class SpriteDistanceScaler : MonoBehaviour
{
    [SerializeField] Transform scaledObject;
    [SerializeField] Vector2 distanceRange;
    [SerializeField] Vector2 sizeRange;

    [SerializeField, ReadOnly] float DEBUG_DistanceValue;

    // Update is called once per frame
    void Update()
    {
        float distanceValue = Mathf.Lerp(distanceRange.x, distanceRange.y, (distanceRange.x+scaledObject.position.z)/distanceRange.y)/distanceRange.y;
        DEBUG_DistanceValue = distanceValue;
        float scaleValue = Mathf.Lerp(sizeRange.x, sizeRange.y, distanceValue);
        scaledObject.localScale = Vector2.one * scaleValue;
    }
}
