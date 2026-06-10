using UnityEngine;
using UnityEngine.Events;

public class OnDeathHandler : MonoBehaviour
{
    [SerializeField] UnityEvent2 onDeath;

    public void OnStatChanged(float value)
    {
        if(value <= 0)
        {
            onDeath.Invoke();
        }
    }

    public void TryDestroy(GameObject givenObject)
    {
        Destroy(givenObject);
    }
}
