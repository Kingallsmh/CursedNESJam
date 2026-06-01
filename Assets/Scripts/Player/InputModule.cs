using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputModule : BaseModule
{
    [SerializeField]
    [FoldoutGroup("Events")]
    private List<ButtonStateEvent> buttonEvent;
    [SerializeField]
    [FoldoutGroup("Events")]
    private List<Vector2Event> vector2InputEvent;

    bool canUseInput = true;

    public bool CanUseInput { get => canUseInput; set => canUseInput = value; }
    public List<Vector2Event> Vector2InputEvent { get => vector2InputEvent; }
    public List<ButtonStateEvent> ButtonEvent { get => buttonEvent; }

    public void SetVector2Value(Vector2 value, int layer)
    {
        if(canUseInput == false || layer >= Vector2InputEvent.Count) { return; }
        vector2InputEvent[layer].Invoke(value);
    }

    public void SetButtonStateValue(ButtonState state, int layer)
    {
        if (canUseInput == false || layer >= buttonEvent.Count) { return; }
        buttonEvent[layer].Invoke(state);
    }

    public void ResetControlValues()
    {
        for(int i = 0; i < vector2InputEvent.Count; i++)
        {
            vector2InputEvent[i].Invoke(Vector2.zero);
        }

        for (int i = 0; i < buttonEvent.Count; i++)
        {
            buttonEvent[i].Invoke(new ButtonState());
        }
    }
}
