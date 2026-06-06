using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovementStyleModule : MonoBehaviour
{
    #region Variables
    [Title("Enemy Movement: ", Bold = true)]
    [SerializeReference]
    EnemyMovementStyle m_movementStyle;

    [Title("Additional Movement Impacts:")]
    [SerializeField]
    float m_startSpeed = 0f;
    [SerializeField]
    LayerMask m_collisionLayers;

    [Title("Required Reference(s):")]
    [SerializeField]
    Rigidbody2D m_rigidbodyRef;

    [SerializeField]
    StatImpactModule m_statImpactor;
    [SerializeField]
    string m_statName;
    StatImpact m_stat;
    #endregion

    #region Methods
    void Start()
    {
        if (m_rigidbodyRef == null)
            GetRigidybody2dReference();

        GetStatForImpacting();
        ApplySpeedStatChange();

        m_movementStyle.RigidbodyRef = m_rigidbodyRef;
        m_movementStyle.SetUp();
    }

    void Update()
    {
        if (m_rigidbodyRef != null)
            m_movementStyle.UpdateMovement();
    }

    void OnDestroy()
    {
        if (m_stat != null)
            m_stat.StatChangeEvent.RemoveListener(ApplySpeedStatChange);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if ((m_collisionLayers.value & (1 << collision.gameObject.layer)) > 0)
        {
            m_movementStyle.SetCollisionHitDetails(collision);
            m_movementStyle.CollidedHandling();
        }
    }

    void GetRigidybody2dReference()
    {
        m_rigidbodyRef = this.GetComponent<Rigidbody2D>();
    }

    [Button]
    [FoldoutGroup("Testing")]
    public void UpdateSpeed(float newSpeed) => m_movementStyle.SetNewSpeed(newSpeed);

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
        m_stat.StatChangeEvent.RemoveListener(ApplySpeedStatChange);
        m_stat.StatChangeEvent.AddListener(ApplySpeedStatChange);
    }

    [Button]
    [FoldoutGroup("Testing")]
    public void ApplySpeedStatChange()
    {
        if (m_statImpactor == null || m_stat == null)
            return;

        UpdateSpeed(m_stat.GetStatChange());
    }
    #endregion
}
