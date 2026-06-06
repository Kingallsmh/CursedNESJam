using UnityEngine;
using UnityEngine.Events;

public class AngledFacingSprite : MonoBehaviour
{
    [SerializeField] Transform render;
    [SerializeField] BoolEvent onAngled;

    public void SetDirection(Vector2 direction)
    {
        float scale = 1;
        if(direction.x < 0)
        {
            scale = -1;
        }
        onAngled.Invoke(Mathf.Abs(direction.x) > 0);
        render.localScale = new Vector2(scale, 1);
    }
}
