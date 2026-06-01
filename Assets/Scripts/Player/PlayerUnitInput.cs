using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerUnitInput : MonoBehaviour
{
    [SerializeField] InputModule unit;
    [SerializeField] UnityEvent<Entity> onUnitChanged;
    [SerializeField] float directionalInputDeadzone = 0.125f;

    [ReadOnly, SerializeField] Vector2 mainDirectional;
    [ReadOnly, SerializeField] ButtonState buttonA;
    [ReadOnly, SerializeField] ButtonState buttonB;
    [ReadOnly, SerializeField] ButtonState buttonStart;
    [ReadOnly, SerializeField] ButtonState buttonSelect;

    private void Start()
    {
        mainDirectional = new Vector2();
        buttonA = new ButtonState();
        buttonB = new ButtonState();
        buttonStart = new ButtonState();
        buttonSelect = new ButtonState();
    }

    private void Update()
    {
        if(unit == null) { return; }
        unit.SetVector2Value(mainDirectional, 0);
        unit.SetButtonStateValue(buttonA, 0);
        buttonA.UpdatePressed();
        unit.SetButtonStateValue(buttonB, 1);
        buttonB.UpdatePressed();
        unit.SetButtonStateValue(buttonStart, 2);
        buttonStart.UpdatePressed();
        unit.SetButtonStateValue(buttonSelect, 3);
        buttonSelect.UpdatePressed();
    }

    public void UseMainDirectional(CallbackContext action)
    {
        if (unit == null) { return; }
        mainDirectional = action.ReadValue<Vector2>();
        mainDirectional.x = Mathf.Abs(mainDirectional.x) > directionalInputDeadzone ? Mathf.Sign(mainDirectional.x) : 0;
        mainDirectional.y = Mathf.Abs(mainDirectional.y) > directionalInputDeadzone ? Mathf.Sign(mainDirectional.y) : 0;
    }

    public void UseButtonA(CallbackContext action)
    {
        if (unit == null) { return; }
        buttonA.IsPressed = action.performed;
    }

    public void UseButtonB(CallbackContext action)
    {
        if (unit == null) { return; }
        buttonB.IsPressed = action.performed;
    }

    public void UseButtonStart(CallbackContext action)
    {
        if (unit == null) { return; }
        buttonStart.IsPressed = action.performed;
    }

    public void UseButtonSelect(CallbackContext action)
    {
        if (unit == null) { return; }
        buttonSelect.IsPressed = action.performed;
    }

    public void ResetControlValues()
    {
        mainDirectional = Vector2.zero;
        buttonA.Reset();
        buttonB.Reset();
        buttonStart.Reset();
        buttonSelect.Reset();
    }

    public void SwitchUnitInput(InputModule input)
    {
        unit = input;
        onUnitChanged.Invoke(input.GetOwner());
    }
}
