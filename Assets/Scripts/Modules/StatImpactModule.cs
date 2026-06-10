using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.Rendering.DebugUI;

public class StatImpactModule : BaseModule
{
    public List<StatImpact> StatsList { get => m_statsToEffect; }

    [SerializeField]
    List<StatImpact> m_statsToEffect = new List<StatImpact>();

    [Button]
    public void SetStatEffect(string statName, float Value)
    {
        if (string.IsNullOrWhiteSpace(statName) || statName.Length == 0) //Don't let this fail silently. Without some kind of log or it crashing, you'll never know
            return;

        if (TryGetStat(statName, out StatImpact stat))
        {
            stat.SetStat(Value);            
        }
    }    

    public void SetStatEffect(StatValue stat)
    {
        SetStatEffect(stat.statName, stat.currentValue);
    }

    public void ApplyStatEffect(string statName, float value)
    {
        if (TryGetStat(statName, out StatImpact stat))
        {
            stat.AddToStat(value);
        }
    }

    public void ApplyStatEffect(StatValue stat)
    {
        ApplyStatEffect(stat.statName, stat.currentValue);
    }

    public void ApplyHurtInfo(HurtInfo info)
    {
        for (int i = 0; i < info.affectedStats.Count; i++)
        {
            ApplyStatEffect(info.affectedStats[i]);
        }
    }

    [Button]
    public void UpdateStatEffectsMaxValue(string statName, float max)
    {
        if (string.IsNullOrWhiteSpace(statName) || statName.Length == 0) //Don't let this fail silently. Without some kind of log or it crashing, you'll never know
            return;

        if(TryGetStat(statName, out StatImpact stat))
        {
            stat.MaxValue = max;
        }
    }

    public StatImpact GetStat(string statName)
    {
        foreach (StatImpact stat in m_statsToEffect)
        {
            if (stat != null && stat.StatName.ToLower() == statName.ToLower())
            {
                return stat;
            }
        }

        Debug.LogError($"Stat {statName} was not found!");
        return null;
    }

    public bool TryGetStat(string statName, out StatImpact stat)
    {
        stat = GetStat(statName);
        if(stat == null) { return false; }
        return true;
    }
}

[Serializable]
public class StatImpact
{
    #region Variables
    [Title("$StatName")]
    [SerializeField] private StatValue stat = new StatValue("unnassigned", 0f);
    public float MinValue = 0f;
    public float MaxValue = 0f;
    [FoldoutGroup("Event")]
    public UnityEvent StatChangeEvent = new UnityEvent();
    [FoldoutGroup("Event")]
    public FloatEvent StatValueUpdateEvent = new FloatEvent();

    public StatValue Stat => stat;
    public string StatName { get => stat.statName; }
    public float CurrentValue { get => stat.currentValue; set => stat.currentValue = value; }
    #endregion

    #region Methods
    public void SetStat(float value)
    {
        CurrentValue = MinValue == 0 && MaxValue == 0f ? value : Mathf.Clamp(value, MinValue, MaxValue);
        StatChangeEvent.Invoke();
        StatValueUpdateEvent.Invoke(CurrentValue);
    }

    public void AddToStat(float value)
    {
        float newValue = CurrentValue + value;
        SetStat(newValue);
    }

    public float GetStatChange() => CurrentValue;
    #endregion
}

[Serializable]
public struct StatValue
{
    public string statName;
    public float currentValue;

    public StatValue(string name, float value)
    {
        statName = name;
        currentValue = value;
    }
}
