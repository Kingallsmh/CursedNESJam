using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using UnityEngine;
using System;
using TMPro;

/// <summary>
/// Holds a list of potential player characters the user
/// can use, including the name, description, visual,
/// and prefab the player selects for gameplay
/// 
/// TODO: Get the prefab to become the one used during gameplay.
/// </summary>
public class PlayerSelectionModule : BaseModule
{
    #region Variables
    [SerializeField]
    List<PlayerOption> m_listOfPlayableCharacters = new List<PlayerOption>();

    [SerializeField]
    [PropertyTooltip("Continuous means you can keep going to the next option in the list, where the last will move to the first. " +
        "If it's false, then going to the last index will stop.")]
    bool m_continuousCyclingThrough = false;

    [SerializeField]
    bool m_applyOnStart = true;
    int m_currentIndex;

    [SerializeField]
    TextMeshProUGUI m_nameText;
    [SerializeField]
    TextMeshProUGUI m_descriptionText;
    [SerializeField]
    Image m_visualDisplay;
    #endregion

    #region Methods
    private void Start()
    {
        m_currentIndex = 0;
        CleanListOfNulls();

        if (m_applyOnStart)
            SetCurrentMenuVisuals();
    }

    void CleanListOfNulls()
    {
        if (m_listOfPlayableCharacters == null || m_listOfPlayableCharacters.Count == 0)
            return;
        
        for (int index = m_listOfPlayableCharacters.Count - 1; index >= 0; --index)
            if (m_listOfPlayableCharacters[index] == null || !m_listOfPlayableCharacters[index].IsValid())
                m_listOfPlayableCharacters.Remove(m_listOfPlayableCharacters[index]);
    }

    [Button]
    [FoldoutGroup("Navigation Tools")]
    public void CycleThroughListPrevious()
    {
        if (m_currentIndex == 0 && m_continuousCyclingThrough)
        {
            m_currentIndex = m_listOfPlayableCharacters.Count - 1;
            return;
        }

        if (m_currentIndex == 0)
            return;

        --m_currentIndex;

        SetCurrentMenuVisuals();
    }

    [Button]
    [FoldoutGroup("Navigation Tools")]
    public void CycleThroughListNext()
    {
        if (m_currentIndex == m_listOfPlayableCharacters.Count - 1 && m_continuousCyclingThrough)
        {
            m_currentIndex = 0;
            return;
        }

        if (m_currentIndex == m_listOfPlayableCharacters.Count - 1)
            return;

        ++m_currentIndex;

        SetCurrentMenuVisuals();
    }

    [Button]
    public void SetCurrentMenuVisuals()
    {
        if (m_nameText != null)
            m_nameText.text = m_listOfPlayableCharacters[m_currentIndex].Name;

        if (m_descriptionText != null)
            m_descriptionText.text = m_listOfPlayableCharacters[m_currentIndex].Description;

        if (m_visualDisplay == null)
            return;

        if (m_listOfPlayableCharacters[m_currentIndex].UseTextureInsteadOfSprite && m_listOfPlayableCharacters[m_currentIndex].TextureVisual != null)
        {
            Texture2D texture = m_listOfPlayableCharacters[m_currentIndex].TextureVisual;
            Sprite wanted = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);

            m_visualDisplay.sprite = wanted;
            return;
        }

        if (m_listOfPlayableCharacters[m_currentIndex].SpriteVisual != null)
            m_visualDisplay.sprite = m_listOfPlayableCharacters[m_currentIndex].SpriteVisual;
    }
    #endregion
}

[Serializable]
class PlayerOption
{
    #region Variables
    [Title("$Title")]
    public string Name;
    [TextArea]
    public string Description;

    [Space(15)]
    public bool UseTextureInsteadOfSprite = false;

    [PreviewField]
    [Title("Pick the texture to display: ", "For character select visual!")]
    [HorizontalGroup("Visuals and Prefab: ", width:250)]
    [HideLabel]
    [HideIf(nameof(UseTextureInsteadOfSprite))]
    public Sprite SpriteVisual;

    [PreviewField]
    [Title("Pick the texture to display: ", "For character select visual!")]
    [HorizontalGroup("Visuals and Prefab: ", width: 250)]
    [HideLabel]
    [ShowIf(nameof(UseTextureInsteadOfSprite))]
    public Texture2D TextureVisual;

    [PreviewField]
    [Title("Pick the prefab used in gameplay: ", "For gameplay!")]
    [HorizontalGroup("Visuals and Prefab: ", width:250)]
    [HideLabel]
    public GameObject Prefab;
    #endregion

    #region Method
    public string Title()
    {
        if (string.IsNullOrWhiteSpace(Name))
            return "No Name";

        return Name;
    }

    public bool IsValid() => Prefab != null;
    #endregion
}
