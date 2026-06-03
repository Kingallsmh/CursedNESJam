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
    #endregion

    #region Methods
    void Start()
    {
        if (m_rigidbodyRef == null)
            GetRigidybody2dReference();

        m_movementStyle.Speed = m_startSpeed;
        m_movementStyle.RigidbodyRef = m_rigidbodyRef;

        m_movementStyle.SetUp();
    }

    void Update()
    {
        if (m_rigidbodyRef != null)
            m_movementStyle.UpdateMovement();
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
    #endregion
}
