using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class InvincibilityHandler : MonoBehaviour
{
    [SerializeField] HitboxModule hitbox;
    [SerializeField] float totalTime;

    [FoldoutGroup("Events")]
    [SerializeField] UnityEvent2 onStart;
    [FoldoutGroup("Events")]
    [SerializeField] UnityEvent2 onEnd;

    Coroutine currentRoutine;

    StatImpact additionalTime;

    public void StartInvincibility()
    {
        if(currentRoutine != null) { Debug.LogWarning("Trying start another Invincibility while still invincible!"); return; }
        currentRoutine = StartCoroutine(InvincibilityRoutine());
    }

    public void EndInvincibility()
    {
        hitbox.IsHittable = true;
        StopCoroutine(currentRoutine);
    }

    public float GetTotalTime()
    {
        return additionalTime.CurrentValue + totalTime;
    }

    IEnumerator InvincibilityRoutine()
    {
        hitbox.IsHittable = false;
        onStart.Invoke();
        yield return new WaitForSeconds(totalTime);
        hitbox.IsHittable = true;
        onEnd.Invoke();
    }
}
