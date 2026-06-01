using UnityEngine;

public class AngledFacingSprite : MonoBehaviour
{
    [SerializeField] Transform render;

    public void SetDirection(Vector2 direction)
    {
        float scale = 1;
        if(direction.x < 0)
        {
            scale = -1;
        }
        render.localScale = new Vector2(scale, 1);
    }
}
