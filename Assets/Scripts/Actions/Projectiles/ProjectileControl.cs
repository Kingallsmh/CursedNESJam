using UnityEngine;
using UnityEngine.Events;

public class ProjectileControl : MonoBehaviour, ICrosshairAction
{
    [SerializeField] Rigidbody rb;
    [SerializeField] UnityEvent onReachedEnd;
    [SerializeField] string hitTrigger = "IsHit";
    [SerializeField] float hitAreaSize = 0.1f;
    [SerializeField] float cooldownTime = 0.5f;
    [SerializeField] StatValue damagedStat = new StatValue("Health", 1);

    [SerializeField] float defaultSpeed = 15;

    float zTarget;

    public void UseCrosshairAction(AttackModule owner, Transform crosshair)
    {
        zTarget = crosshair.position.z;
        transform.position = owner.transform.position;
        Vector3 direction = (crosshair.position - owner.transform.position).normalized;
        rb.linearVelocity = direction * defaultSpeed;
    }

    private void FixedUpdate()
    {
        if(transform.position.z >= zTarget)
        {
            HitArea();
            onReachedEnd.Invoke();
        }
    }

    public void HitArea()
    {
        RaycastHit2D[] hitArray = Physics2D.BoxCastAll(transform.position, Vector2.one * hitAreaSize, 0, Vector2.zero);
        bool didHit = false;
        for(int i = 0; i < hitArray.Length; i++)
        {
            BaseModule module = null;
            //if (hitArray[i].rigidbody)
            //{
            //    module = hitArray[i].rigidbody.GetComponent<BaseModule>();
            //}
            if (hitArray[i].collider)
            {
                module = hitArray[i].collider.GetComponent<BaseModule>();
            }
            if(module == null) { continue; }
            if(module.GetOwner().TryGetModule(out StatImpactModule statModule) == false) { continue; }
            //animModule.SetAnimatorTrigger(hitTrigger);
            didHit = true;
        }
        if (didHit) { DestroyThis(); }
    }

    public void DestroyThis()
    {
        Destroy(gameObject);
    }

    public float GetCooldownTime()
    {
        return cooldownTime;
    }
}


