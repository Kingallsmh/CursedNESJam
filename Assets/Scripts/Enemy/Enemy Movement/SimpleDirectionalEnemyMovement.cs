using Sirenix.OdinInspector;
using UnityEngine;
using System;

public class SimpleDirectionalEnemyMovement : EnemyMovementStyle
{
    #region Variables
    [Title("Movement Style: ")]
    [SerializeField]
    MovementAxis m_direction;
    #endregion

    #region Methods
    [Button("Update Direction")]
    [FoldoutGroup("Testing")]
    public override void SetUp()
    {
        switch(m_direction)
        {
            case MovementAxis.HORIZONTAL_POS:
                CurrentDirection = Vector2.right;
                break;
            case MovementAxis.HORIZONTAL_NEG:
                CurrentDirection = Vector2.left;
                break;
            case MovementAxis.VERTICAL_POS:
                CurrentDirection = Vector2.up;
                break;
            case MovementAxis.VERTICAL_NEG:
                CurrentDirection = Vector2.down;
                break;
        }
    }
    #endregion
}

[Serializable]
enum MovementAxis
{
    HORIZONTAL_POS, 
    HORIZONTAL_NEG, 
    VERTICAL_POS, 
    VERTICAL_NEG
}
