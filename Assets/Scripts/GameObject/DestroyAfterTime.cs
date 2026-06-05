using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField] float time;

    Timer timer;

    private void Awake()
    {
        timer = new Timer();
        timer.SetTime(time);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer.Done())
        {
            Destroy(gameObject);
        }
        timer.Update();
    }
}
