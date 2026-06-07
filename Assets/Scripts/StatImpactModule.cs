using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine.Events;
using UnityEngine;
using System;

public class StatImpactModule : BaseModule
{
    public List<StatImpact> StatsList { get => m_statsToEffect; }

    [SerializeField]
    List<StatImpact> m_statsToEffect = new List<StatImpact>();

    [Button]
    public void CallStatEffect(string statName, float Value)
    {
        if (string.IsNullOrWhiteSpace(statName) || statName.Length == 0) //Don't let this fail silently. Without some kind of log or it crashing, you'll never know
            return;

        if (TryGetStat(statName, out StatImpact stat))
        {
            stat.ApplyStatChange(Value);            
        }
    }

    public void CallStatEffect(StatValue stat)
    {
        CallStatEffect(stat.statName, stat.currentValue);
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

    public StatValue Stat => stat;
    public string StatName { get => stat.statName; }
    public float CurrentValue { get => stat.currentValue; set => stat.currentValue = value; }
    #endregion

    #region Methods
    public void ApplyStatChange(float value)
    {
        CurrentValue = Mathf.Clamp(value, MinValue, MaxValue);
        StatChangeEvent.Invoke();
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
