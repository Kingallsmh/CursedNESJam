using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HitboxModule : BaseModule
{
    [FoldoutGroup("Event")]
    [SerializeField] UnityEvent2 onHit;
    [FoldoutGroup("Event")]
    [SerializeField] HurtInfoEvent onHitWithInfo;

    [FoldoutGroup("Debug")]
    [SerializeField, ReadOnly] bool isHittable = true;
    public bool IsHittable { get => isHittable; set => isHittable = value; }

    [Button]
    public bool HitEntity(HurtInfo info)
    {
        if(isHittable == false) { return false; }
        onHit.Invoke();
        onHitWithInfo.Invoke(info);
        return true;
    }
}

public class HurtInfo
{
    public Entity owner;
    public List<StatValue> affectedStats = new List<StatValue>();
}
