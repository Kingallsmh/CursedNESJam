using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using UnityEngine;

public class StatDisplayListModule : MonoBehaviour
{
    #region Variables
    [Title("Required References: ")]
    [SerializeField]
    VerticalLayoutGroup m_layoutGroup;
    [SerializeField]
    GameObject m_listEntryPrefab;
    [SerializeField]
    StatImpactModule m_statImpactor;

    [Title("Customization: ")]
    [SerializeField]
    int m_entriesToDisplayOnScreen = 4;
    int m_currentIndex = 0;
    List<GameObject> m_entries = new List<GameObject>();
    #endregion

    #region Methods
    void Start()
    {
        if (m_layoutGroup == null || m_listEntryPrefab == null || m_statImpactor == null)
        {
            Debug.Log("The StatDisplayListModule can't display the stats due to missing references!");
            return;
        }

        GetListSetUp();
    }

    void GetListSetUp()
    {
        if (m_statImpactor == null || m_statImpactor.StatsList.Count == 0)
            return;

        if (m_listEntryPrefab.GetComponent<StatDisplayModule>() == null)
        {
            Debug.Log("The list entry is invalid or is missing a reference, cannot set up!");
            return;
        }

        GameObject entry;
        StatDisplayModule tempStatModule;
        List<StatImpact> stats = m_statImpactor.StatsList;

        for (int index = 0; index < stats.Count; ++index)
        {
            entry = Instantiate(m_listEntryPrefab, m_layoutGroup.gameObject.transform);

            tempStatModule = entry.GetComponent<StatDisplayModule>();
            tempStatModule.StatImpactor = m_statImpactor;
            tempStatModule.StatName = stats[index].StatName;
            tempStatModule.SetUp();

            m_entries.Add(entry);
        }
    }

    [Button]
    [FoldoutGroup("Testing")]
    public void ScrollDisplayedEntries(int amount)
    {
        // This might be a bit of an odd way of doing it, but it keeps the "blocky" look and feel to it!

        if (amount == 0 || m_entries == null || m_entries.Count == 0 || m_entries.Count <= m_entriesToDisplayOnScreen)
            return;

        // This ensures it'll always display the amount of entries as expected to display at once
        m_currentIndex = Mathf.Clamp(m_currentIndex + amount, 0, m_entries.Count - m_entriesToDisplayOnScreen);

        for(int index = 0; index < m_entries.Count; ++index)
        {
            m_entries[index].SetActive(false);

            if (index >= m_currentIndex && index <= m_currentIndex + m_entriesToDisplayOnScreen)
                m_entries[index].SetActive(true);
        }    
    }
    #endregion
}
