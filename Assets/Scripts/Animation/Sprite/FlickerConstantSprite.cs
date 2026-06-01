using UnityEngine;

public class FlickerConstantSprite : MonoBehaviour
{
    [SerializeField] SpriteRenderer rend;
    [SerializeField] float flickerTime;

    Timer time = new Timer();

    // Update is called once per frame
    void Update()
    {
        if (time.Done())
        {
            rend.enabled = !rend.enabled;
            time.SetTime(flickerTime);
        }
        else
        {
            time.Update();
        }
    }
}
