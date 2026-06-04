using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine.Events;
using UnityEngine;
using System;

/// <summary>
/// Potentially redundent, but gives greater control over swapping
/// active input options with additional events, if anything should
/// be required there.
/// </summary>
public class SwitchInputModesModule : MonoBehaviour
{
    #region Variables
    [SerializeField]
    List<InputMode> m_inputModes = new List<InputMode>();

    [SerializeField]
    PlayerUnitInput m_playerInputHandler;
    [SerializeField]
    bool m_setToFirstOnStart = true;

    bool m_isValid = true;
    int m_currentInputOption = 0;
    #endregion

    #region Methods
    private void Start()
    {
        CleanListOfNulls();

        if (m_playerInputHandler == null || m_inputModes.Count == 0)
            m_isValid = false;

        if (m_setToFirstOnStart)
            SetInputOpion(0);
    }

    void CleanListOfNulls()
    {
        if (m_inputModes.Count == 0)
            return;

        for (int index = m_inputModes.Count - 1; index >= 0; --index)
            if (m_inputModes[index] == null || m_inputModes[index].InputOption == null)
                m_inputModes.Remove(m_inputModes[index]);
    }

    // TODO: Add "add with delay" options?

    [Button]
    public void SetInputOpion(int index)
    {
        if (!m_isValid) return;
        m_currentInputOption = index;
        m_playerInputHandler.SwitchUnitInput(m_inputModes[index].InputOption);
    }

    [Button]
    public void SetToNextInputOption()
    {
        ++m_currentInputOption;

        if (m_currentInputOption >= m_inputModes.Count)
            m_currentInputOption = 0;

        SetInputOpion(m_currentInputOption);
    }
    #endregion
}

[Serializable]
class InputMode
{
    #region Variables
    [Title("$Title")]
    public InputModule InputOption;
    [FoldoutGroup("Event")]
    public UnityEvent OptionSpecificEvent = new UnityEvent();
    #endregion

    #region Methods
    public string Title()
    {
        if (InputOption != null)
            return InputOption.name;

        return "New Input Option";
    }
    #endregion
}
