using UnityEngine;

/// <summary>
/// Doesn't really need to do much but exist, at this point?
/// </summary>
public class ImpactsPointsModule : MonoBehaviour
{
    #region Variables
    public int Points { get => m_pointsWithImpact; }

    [SerializeField]
    int m_pointsWithImpact;
    #endregion
}
