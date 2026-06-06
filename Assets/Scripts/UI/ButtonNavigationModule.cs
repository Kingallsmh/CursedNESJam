using System.Collections.Generic;
using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using System;

/// <summary>
/// A button navigation system that works akin to a
/// FSM, though specifically for handling button
/// navigation on a north-east-south-west directional
/// system. Has some additional tools for setting up
/// for user-friendliness, though there's other ways
/// of doing this
/// </summary>
public class ButtonNavigationModule : MonoBehaviour
{
    #region Variables
    [Title("Button Navigation ''Map''")]
    [SerializeField]
    List<NavigationalButton> m_buttonMap = new List<NavigationalButton>();
    [SerializeField]
    int m_currentIndex = 0;
    [SerializeField]
    float m_cooldownTimeMovement = 0.5f;
    [SerializeField]
    float m_cooldownTimeButtonPress = 0.5f;

    [Title("Additional Effects")]
    [SerializeField]
    Color m_highlightColour = Color.darkRed;
    Color m_defaultColour;

    ColorBlock m_highlightBlock;
    ColorBlock m_defaultBlock;

    bool m_ignoreMovementInteraction = false;
    bool m_ignoreButtonPressInteraction = false;
    #endregion

    #region Methods
    private void Start()
    {
        CleanListOfNulls();
        SetUpColourBlocks();
        HighlightButton(true);
    }

    void CleanListOfNulls()
    {
        if (m_buttonMap.Count == 0)
            return;

        for (int index = m_buttonMap.Count - 1; index >= 0; --index)
            if (m_buttonMap[index] == null || m_buttonMap[index].actualButton == null)
                m_buttonMap.Remove(m_buttonMap[index]);
    }

    [Button("Update Colours")]
    public void SetUpColourBlocks()
    {
        if (m_buttonMap == null || m_buttonMap.Count == 0)
            return;

        m_defaultBlock = m_buttonMap[0].actualButton.colors;
        m_highlightBlock = m_defaultBlock;
        m_highlightBlock.normalColor = m_highlightColour;
    }

    void HighlightButton(bool highlight)
    {
        if (highlight)
        {
            m_buttonMap[m_currentIndex].actualButton.colors = m_highlightBlock;
            return;
        }

        m_buttonMap[m_currentIndex].actualButton.colors = m_defaultBlock;
    }

    IEnumerator ButtonPressedCooldown()
    {
        m_ignoreButtonPressInteraction = true;
        yield return new WaitForSeconds(m_cooldownTimeButtonPress);
        m_ignoreButtonPressInteraction = false;
    }

    IEnumerator NavigationMovementCooldown()
    {
        m_ignoreMovementInteraction = true;
        yield return new WaitForSeconds(m_cooldownTimeMovement);
        m_ignoreMovementInteraction = false;
    }

    [Button]
    [FoldoutGroup("Navigation Tools")]
    public void ClickCurrentButton(ButtonState state)
    {
        if (!state.IsFirstDown())
            return;

        if (m_ignoreButtonPressInteraction)
            return;

        Debug.Log($"{m_buttonMap[m_currentIndex].actualButton.name} button pressed!");
        m_buttonMap[m_currentIndex].actualButton.onClick.Invoke();
        StartCoroutine(ButtonPressedCooldown());
    }

    public void NavigateButtonsThroughMovementVector(Vector2 input)
    {
        if (m_ignoreMovementInteraction)
            return;

        if (input.y > 0) MoveUp();
        if (input.y < 0) MoveDown();
        if (input.x < 0) MoveLeft();
        if (input.x > 0) MoveRight();

        StartCoroutine(NavigationMovementCooldown());
    }

    [Button]
    [FoldoutGroup("Navigation Tools")]
    public void MoveUp()
    {
        if (!m_buttonMap[m_currentIndex].CanMoveUp)
            return;

        HighlightButton(false);
        m_currentIndex = m_buttonMap[m_currentIndex].UpButtonIndex;
        HighlightButton(true);
    }

    [Button]
    [FoldoutGroup("Navigation Tools")]
    public void MoveDown()
    {
        if (!m_buttonMap[m_currentIndex].CanMoveDown)
            return;

        HighlightButton(false);
        m_currentIndex = m_buttonMap[m_currentIndex].DownButtonIndex;
        HighlightButton(true);
    }

    [Button]
    [FoldoutGroup("Navigation Tools")]
    public void MoveLeft()
    {
        if (!m_buttonMap[m_currentIndex].CanMoveLeft)
            return;

        HighlightButton(false);
        m_currentIndex = m_buttonMap[m_currentIndex].LeftButtonIndex;
        HighlightButton(true);
    }

    [Button]
    [FoldoutGroup("Navigation Tools")]
    public void MoveRight()
    {
        if (!m_buttonMap[m_currentIndex].CanMoveRight)
            return;

        HighlightButton(false);
        m_currentIndex = m_buttonMap[m_currentIndex].RightButtonIndex;
        HighlightButton(true);
    }

    [Title("Help With Setting Up: ")]
    [Button(ButtonSizes.Large)]
    public void SetVisualIndexForAid()
    {
        if (m_buttonMap == null || m_buttonMap.Count == 0)
            return;

        for (int index = m_buttonMap.Count - 1; index >= 0; -- index)
            if (m_buttonMap[index] != null)
                m_buttonMap[index].SetIndexInList(index);
    }

    [Button]
    [FoldoutGroup("Auto Set Up Tools")]
    public void SetButtonsToAutoGoToPreviousNextHorizontal(bool rightToLeft)
    {
        if (m_buttonMap == null || m_buttonMap.Count == 0)
            return;
        int first = 0;
        int second = 0;

        for (int index = m_buttonMap.Count - 1; index >= 0; --index)
        {
            if (m_buttonMap[index] != null)
            {
                if (rightToLeft)
                {
                    first = index + 1;
                    second = index - 1;
                }
                else
                {
                    first = index - 1;
                    second = index + 1;
                }

                m_buttonMap[index].LeftButtonIndex = Mathf.Clamp(first, 0, m_buttonMap.Count - 1);
                m_buttonMap[index].RightButtonIndex = Mathf.Clamp(second, 0, m_buttonMap.Count - 1);

                m_buttonMap[index].CanMoveLeft = (index != 0 && !rightToLeft || index != m_buttonMap.Count - 1 && rightToLeft);
                m_buttonMap[index].CanMoveRight = (index != 0 && rightToLeft || index != m_buttonMap.Count - 1 && !rightToLeft);
            }
        }
    }

    [Button]
    [FoldoutGroup("Auto Set Up Tools")]
    public void SetButtonsToAutoGoToPreviousNextVertical(bool bottomToTop)
    {
        if (m_buttonMap == null || m_buttonMap.Count == 0)
            return;

        int first = 0;
        int second = 0;

        for (int index = m_buttonMap.Count - 1; index >= 0; --index)
        {
            if (m_buttonMap[index] != null)
            {
                if (bottomToTop)
                {
                    first = index - 1;
                    second = index + 1;
                }
                else
                {
                    first = index + 1;
                    second = index - 1;
                }

                m_buttonMap[index].DownButtonIndex = Mathf.Clamp(first, 0, m_buttonMap.Count - 1);
                m_buttonMap[index].UpButtonIndex = Mathf.Clamp(second, 0, m_buttonMap.Count - 1);

                m_buttonMap[index].CanMoveDown = (index != 0 && bottomToTop || index != m_buttonMap.Count - 1 && !bottomToTop);
                m_buttonMap[index].CanMoveUp = (index != 0 && !bottomToTop || index != m_buttonMap.Count - 1 && bottomToTop);
            }
        }
    }

    [Button]
    [FoldoutGroup("Auto Set Up Tools")]
    public void ClearAllButtonNavigationDirections()
    {
        if (m_buttonMap == null || m_buttonMap.Count == 0)
            return;

        for (int index = m_buttonMap.Count - 1; index >= 0; --index)
        {
            if (m_buttonMap[index] != null)
            {
                m_buttonMap[index].UpButtonIndex = 0;
                m_buttonMap[index].DownButtonIndex = 0;
                m_buttonMap[index].LeftButtonIndex = 0;
                m_buttonMap[index].RightButtonIndex = 0;

                m_buttonMap[index].CanMoveUp = false;
                m_buttonMap[index].CanMoveDown = false;
                m_buttonMap[index].CanMoveLeft = false;
                m_buttonMap[index].CanMoveRight = false;
            }
        }
    }
    #endregion
}

[Serializable]
class NavigationalButton
{
    #region Variables
    [Title("$Title")]
    public Button actualButton;

    [FoldoutGroup("For Navigation Up")]
    public bool CanMoveUp = false;
    [FoldoutGroup("For Navigation Up")]
    [ShowIf(nameof(CanMoveUp))]
    public int UpButtonIndex;

    [FoldoutGroup("For Navigation Down")]
    public bool CanMoveDown = false;
    [FoldoutGroup("For Navigation Down")]
    [ShowIf(nameof(CanMoveDown))]
    public int DownButtonIndex;

    [FoldoutGroup("For Navigation Left")]
    public bool CanMoveLeft = false;
    [FoldoutGroup("For Navigation Left")]
    [ShowIf(nameof(CanMoveLeft))]
    public int LeftButtonIndex;

    [FoldoutGroup("For Navigation Right")]
    public bool CanMoveRight = false;
    [FoldoutGroup("For Navigation Right")]
    [ShowIf(nameof(CanMoveRight))]
    public int RightButtonIndex;

    int m_indexInList;

    public string Title()
    {
        if (actualButton == null)
            return $"Button Navigator : {m_indexInList}";

        return actualButton.name + $": {m_indexInList}";
    }

    public void SetIndexInList(int index) => m_indexInList = index;
    #endregion
}
