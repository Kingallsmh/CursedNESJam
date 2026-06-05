using Sirenix.OdinInspector;
using UnityEngine;

public class PixelPositioningControl : MonoBehaviour
{
    [SerializeField] float pixelsPerUnit = 10f;
    Transform ourTransform;
    [SerializeField, ReadOnly]
    Vector3 lastPosition;
    [SerializeField, ReadOnly]
    Vector3 realPosition;

    private void Awake()
    {
        ourTransform = transform;
        lastPosition = ourTransform.position;
        realPosition = ourTransform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        UpdatePixelLocation();
    }

    [Button]
    void UpdatePixelLocation()
    {
        Vector3 currentPosition = ourTransform.position; //0.54 , 0.7
        Vector3 difFromLast = currentPosition - lastPosition;   //0.5 , 0.7    = 0.04, 0
        realPosition += difFromLast;

        float x = (Mathf.Round(realPosition.x * pixelsPerUnit) / pixelsPerUnit); //0.5
        float y = (Mathf.Round(realPosition.y * pixelsPerUnit) / pixelsPerUnit); //0.7

        Vector3 pixelLocation = new Vector3(x, y, currentPosition.z);
        ourTransform.position = pixelLocation;
        lastPosition = ourTransform.position;
    }
}
