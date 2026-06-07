using Sirenix.OdinInspector;
using UnityEngine.UI;
using UnityEngine;

public class DisplayHealthModule : MonoBehaviour
{
    #region Variables
    [SerializeField]
    Slider m_healthBar;

    float m_max;
    float m_currentHealth;

    [Title("For connecting to Stats Module:")]
    [SerializeField]
    StatImpactModule m_statImpactor;
    [SerializeField]
    string m_statName;
    StatImpact m_stat;
    #endregion

    #region Methods
    void Start()
    {
        if (m_healthBar == null)
        {
            Debug.Log("Can't handle scroll display - missing reference!");
            return;
        }

        GetStatForImpacting();
        ApplyHealthChange();
    }

    [Button]
    [FoldoutGroup("Testing")]
    public void GetStatForImpacting()
    {
        if (m_statImpactor == null)
        {
            Debug.Log($"Can't get stat of name {m_statName}!");
            return;
        }

        m_stat = m_statImpactor.GetStat(m_statName);

        if (m_stat == null)
            return;

        m_healthBar.minValue = 0;
        m_healthBar.maxValue = m_stat.MaxValue;
        m_healthBar.value = m_stat.CurrentValue;

        m_stat.StatChangeEvent.RemoveListener(ApplyHealthChange);
        m_stat.StatChangeEvent.AddListener(ApplyHealthChange);
    }

    [Button]
    [FoldoutGroup("Testing")]
    public void ApplyHealthChange()
    {
        if (m_statImpactor == null || m_stat == null)
            return;

        m_healthBar.value = m_stat.GetStatChange();
    }
    #endregion
}
