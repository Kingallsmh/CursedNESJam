using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine.Events;
using UnityEngine;
using System;

public class StatImpactModule : BaseModule
{
    [SerializeField]
    List<StatImpact> m_statsToEffect = new List<StatImpact>();

    [Button]
    public void CallStatEffect(string statName, float Value)
    {
        foreach (StatImpact stat in m_statsToEffect)
        {
            if (stat != null && stat.StatName.ToLower() == statName.ToLower())
            {
                stat.ApplyStatChange(Value);
                return;
            }
        }

        Debug.Log($"Stat {statName} was not found!");
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

        Debug.Log($"Stat {statName} was not found!");
        return null;
    }
}

[Serializable]
public class StatImpact
{
    #region Variables
    [Title("$StatName")]
    public string StatName = "Unassigned";
    public float MinValue = 0f;
    public float MaxValue = 0f;
    public float CurrentValue = 0f;
    [FoldoutGroup("Event")]
    public UnityEvent StatChangeEvent = new UnityEvent();
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
