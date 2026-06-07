using Sirenix.OdinInspector;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class StatDisplayModule : MonoBehaviour
{
    #region Variables
    public string StatName { get => m_statName; set => m_statName = value; }
    public StatImpactModule StatImpactor { get => StatImpactor; set => m_statImpactor = value; }

    [Title("For Display: ")]
    [SerializeField]
    TextMeshProUGUI m_displayedStatName;
    [SerializeField]
    TextMeshProUGUI m_displayedStatValues;
    [SerializeField]
    Slider m_displayBar;
    [SerializeField]
    bool m_setupOnStart = false;

    [Title("For connecting to Stats Module:")]
    [SerializeField]
    StatImpactModule m_statImpactor;
    [SerializeField]
    string m_statName;
    StatImpact m_stat;

    float m_maxValue;
    float m_currentValue;
    #endregion

    #region Methods
    void Start()
    {
        if (m_setupOnStart)
            SetUp();
    }

    public void SetUp()
    {
        if (m_displayBar == null)
        {
            Debug.Log("Can't handle scroll display - missing reference!");
            return;
        }

        GetStatForImpacting();
        ApplyChangeToBarDisplay();
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

        m_displayBar.minValue = 0;
        m_displayBar.maxValue = m_stat.MaxValue;
        m_displayBar.value = m_stat.CurrentValue;

        if (m_displayedStatName != null)
            m_displayedStatName.text = m_stat.StatName;

        if (m_displayedStatValues != null)
            m_displayedStatValues.text = m_displayBar.value + "/" + m_displayBar.maxValue;

        m_stat.StatChangeEvent.RemoveListener(ApplyChangeToBarDisplay);
        m_stat.StatChangeEvent.AddListener(ApplyChangeToBarDisplay);
    }

    [Button]
    [FoldoutGroup("Testing")]
    public void ApplyChangeToBarDisplay()
    {
        if (m_statImpactor == null || m_stat == null)
            return;

        m_displayBar.value = m_stat.GetStatChange();

        if (m_displayedStatValues != null)
            m_displayedStatValues.text = m_displayBar.value + "/" + m_displayBar.maxValue;
    }
    #endregion
}
