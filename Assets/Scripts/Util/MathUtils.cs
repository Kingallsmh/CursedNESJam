using UnityEngine;

public static class MathUtils
{
    public static Vector3 GetTransformDirectionBasedInput(Vector3 input, Transform pointOfView, Vector3 upDirection)
    {
        Vector3 cameraForward = pointOfView.forward;
        Vector3 flattened = Vector3.ProjectOnPlane(cameraForward, upDirection);
        Quaternion cameraOrientation = Quaternion.LookRotation(flattened, upDirection);
        return cameraOrientation * input;
    }

    public static Vector3 GetTransformDirectionBasedInput(Vector3 input, Transform pointOfView)
    {
        return GetTransformDirectionBasedInput(input, pointOfView, Vector3.up);
    }

    public static Vector3 GetComponentFromVector3(Vector3 directionFrom, Vector3 directionAppliedTo)
    {
        Vector3 vectorC = directionAppliedTo.normalized;
        float length = Vector3.Dot(directionFrom, vectorC);
        return vectorC * length;
    }
}
