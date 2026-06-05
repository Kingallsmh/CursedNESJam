using UnityEngine;
using UnityEngine.Events;

public class ProjectileControl : MonoBehaviour, ICrosshairAction
{
    [SerializeField] Rigidbody rb;
    [SerializeField] UnityEvent onReachedEnd;
    [SerializeField] float hitAreaSize = 0.1f;

    [SerializeField] string hitTrigger = "IsHit";

    float zTarget;

    public void SetCrosshairTarget(Transform crosshair)
    {
        zTarget = crosshair.position.z;
    }

    private void FixedUpdate()
    {
        if(transform.position.z >= zTarget)
        {
            onReachedEnd.Invoke();
            HitArea();
        }
    }

    public void HitArea()
    {
        RaycastHit2D[] hitArray = Physics2D.BoxCastAll(transform.position, Vector2.one * hitAreaSize, 0, Vector2.zero);
        for(int i = 0; i < hitArray.Length; i++)
        {
            if(hitArray[i].rigidbody.TryGetComponent(out BaseModule module) == false) { continue; }
            if(module.GetOwner().TryGetModule(out AnimatorModule animModule) == false) { continue; }
            animModule.SetAnimatorTrigger(hitTrigger);
        }
    }
}

public interface ICrosshairAction
{
    public void SetCrosshairTarget(Transform crosshair);
}
