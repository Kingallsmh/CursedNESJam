using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProjectileControl : MonoBehaviour, ICrosshairAction
{
    [FoldoutGroup("References")]
    [SerializeField] Rigidbody rb;
    [FoldoutGroup("References")]
    [SerializeField] Collider2D hurtTrigger;
    [FoldoutGroup("Events")]
    [SerializeField] UnityEvent onProjectileStart;
    [FoldoutGroup("Events")]
    [SerializeField] UnityEvent onReachedEnd;
    [FoldoutGroup("Events")]
    [SerializeField] UnityEvent<Collider2D> onHitTarget;
    [FoldoutGroup("Settings")]
    [SerializeField] float cooldownTime = 0.5f;
    [FoldoutGroup("Settings")]
    [SerializeField] StatValue cooldownEffectedByOwnerStat = new StatValue("ATK CD", 1f);
    [FoldoutGroup("Settings")]
    [SerializeField] StatValue damagedStat = new StatValue("HP", 1);
    [FoldoutGroup("Settings")]
    [SerializeField] StatValue damageIncreaseByOwnerStat = new StatValue("ATK", 0.5f);
    [FoldoutGroup("Settings")]
    [SerializeField] float defaultSpeed = 15;
    [FoldoutGroup("Settings")]
    [SerializeField] StatValue speedIncreaseByOwnerStat = new StatValue("ATK SPD", 1f);

    float zTarget;
    AttackModule owner;

    public void UseCrosshairAction(AttackModule owner, Transform crosshair)
    {
        this.owner = owner;
        zTarget = crosshair.position.z;
        transform.position = owner.transform.position;
        Vector3 direction = (crosshair.position - owner.transform.position).normalized;

        float additionalSpeed = 0;
        if (owner.GetOwner().TryGetModule(out StatImpactModule statModule))
        {
            if (statModule.TryGetStat(cooldownEffectedByOwnerStat.statName, out StatImpact stat))
            {
                additionalSpeed = stat.CurrentValue * speedIncreaseByOwnerStat.currentValue;
            }
        }

        rb.linearVelocity = direction * (defaultSpeed + additionalSpeed);
        onProjectileStart.Invoke();
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
        List<Collider2D> collidersHit = new List<Collider2D>();        
        int totalHit = Physics2D.OverlapCollider(hurtTrigger, collidersHit);
        bool didHit = false;
        for(int i = 0; i < totalHit; i++)
        {
            if(collidersHit[i].TryGetComponent(out HitboxModule hitbox))
            {
                HurtInfo sendInfo = GetHurtInfo();
                if(sendInfo != null) 
                { 
                    didHit = hitbox.HitEntity(sendInfo); //Should we not remove after first hit?
                    onHitTarget.Invoke(collidersHit[i]);
                    break;
                }
            }
        }
        if (didHit) { DestroyThis(); }
    }

    public void DestroyThis()
    {
        Destroy(gameObject);
    }

    public float GetCooldownTime()
    {
        float statEffect = 0;
        if (owner.GetOwner().TryGetModule(out StatImpactModule statModule))
        {
            if (statModule.TryGetStat(cooldownEffectedByOwnerStat.statName, out StatImpact stat))
            {
                statEffect = (1 + (stat.CurrentValue* cooldownEffectedByOwnerStat.currentValue) / 10);
            }
        }
            return cooldownTime / statEffect;
    }

    public HurtInfo GetHurtInfo()
    {
        HurtInfo info = new HurtInfo();
        info.owner = owner.GetOwner();
        float additionalDmg = 0;
        if(info.owner.TryGetModule(out StatImpactModule statModule))
        {
            if(statModule.TryGetStat(damageIncreaseByOwnerStat.statName, out StatImpact stat))
            {
                additionalDmg = (stat.CurrentValue * damageIncreaseByOwnerStat.currentValue);                
            }
            else
            {
                Debug.LogWarning("Did not find stat from owner: " + damageIncreaseByOwnerStat.statName);
                return null;
            }
        }
        else
        {
            Debug.LogError("Did not find stat Module from owner");
            return null;
        }

        StatValue dmgStat = new StatValue();
        dmgStat.statName = damagedStat.statName;
        dmgStat.currentValue = -(damagedStat.currentValue + additionalDmg);
        info.affectedStats.Add(dmgStat);
        return info;
    }
}


