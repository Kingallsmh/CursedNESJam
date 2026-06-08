using UnityEngine;
using UnityEngine.Events;

public class OnStatChanged : MonoBehaviour
{
    [SerializeField] StatImpactModule statModule;
    [SerializeField] string statName;
    [SerializeField] UnityEvent2 OnStatDropped;
    [SerializeField] UnityEvent2 OnStatIncreased;

    StatImpact stat;
    float previousRecordedValue;

    private void OnEnable()
    {
        stat = statModule.GetStat(statName);
        stat.StatChangeEvent.AddListener(StatChanged);
        previousRecordedValue = stat.CurrentValue;
    }

    private void OnDisable()
    {
        if(stat == null) { return; }
        stat.StatChangeEvent.RemoveListener(StatChanged);
    }

    public void StatChanged()
    {
        if (previousRecordedValue > stat.CurrentValue) //Took damage or had stat lowered
        {
            OnStatDropped.Invoke();
        }
        else if (previousRecordedValue < stat.CurrentValue) //Healed or had stat raised
        {
            OnStatIncreased.Invoke();
        }
    }
}
