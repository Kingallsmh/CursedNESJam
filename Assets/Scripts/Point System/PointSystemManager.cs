using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

/// <summary>
/// Has reference to all the object that gives points in the scene,
/// will active add more / will add to point total on destroy.
/// 
/// TODO: We gotta add ImpactPointModules to this list for calculation
/// when enemies can spawn + leave. For later since those systems aren't 
/// in place yet!
/// TODO: AKA update so this works once we have enemies spawning and handling
/// </summary>
public class PointSystemManager : MonoBehaviour
{
    #region Variables
    [SerializeField]
    List<ImpactsPointsModule> m_impactPointsList = new List<ImpactsPointsModule>();

    float m_currentPointCount = 0f;
    #endregion

    #region Methods
    void Start()
    {
        CleanListOfNulls();
    }

    void CleanListOfNulls()
    {
        if (m_impactPointsList.Count == 0)
            return;

        for (int index = m_impactPointsList.Count - 1; index >= 0; --index)
            if (m_impactPointsList[index] == null)
                m_impactPointsList.Remove(m_impactPointsList[index]);
    }

    [Button]
    public float GetPointTotalInList()
    {
        float total = 0;
        foreach (ImpactsPointsModule impact in m_impactPointsList)
            total += impact.Points;

        return total;
    }

    [Button]
    public void UpdateCurrentPointCountIndidivualIndex(int index)
    {
        if (index >= m_impactPointsList.Count || index < 0)
            return;

        m_currentPointCount += m_impactPointsList[index].Points;
    }

    [Button]
    public void UpdateCurrentPointCountIndidivualRef(ImpactsPointsModule impact)
    {
        if (impact == null)
            return;

        m_currentPointCount += impact.Points;
    }

    [Button]
    public void UpdateCurrentPointCountFromList()
    {
        m_currentPointCount += GetPointTotalInList();
    }

    [Button]
    public void ResetCurrentPointCount()
    {
        m_currentPointCount = 0f;
    }
    #endregion
}
