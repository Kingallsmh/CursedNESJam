using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] string directionalXName = "Horizontal";
    [SerializeField] string directionalYName = "Vertical";
    [SerializeField] string button1Name = "Btn1";
    [SerializeField] string button2Name = "Btn2";

    [SerializeField] UnityEvent<Vector2> onDirectionalInput = new UnityEvent<Vector2>();
    [FoldoutGroup("Button 1")]
    [SerializeField] UnityEvent<bool> onButton1Input = new UnityEvent<bool>();
    [FoldoutGroup("Button 1")]
    [SerializeField] UnityEvent OnButton1DownInput = new UnityEvent();
    [FoldoutGroup("Button 1")]
    [SerializeField] UnityEvent OnButton1UpInput = new UnityEvent();

    [FoldoutGroup("Button 2")]
    [SerializeField] UnityEvent<bool> OnButton2Input = new UnityEvent<bool>();
    [FoldoutGroup("Button 2")]
    [SerializeField] UnityEvent OnButton2DownInput = new UnityEvent();
    [FoldoutGroup("Button 2")]
    [SerializeField] UnityEvent OnButton2UpInput = new UnityEvent();

    public UnityEvent<Vector2> OnDirectionalInput { get => onDirectionalInput; set => onDirectionalInput = value; }

    private void Update()
    {
        onDirectionalInput.Invoke(new Vector2(Input.GetAxisRaw(directionalXName), Input.GetAxisRaw(directionalYName)));
        onButton1Input.Invoke(Input.GetButton(button1Name));
        if (Input.GetButtonDown(button1Name)) { OnButton1DownInput.Invoke(); }
        if (Input.GetButtonUp(button1Name)) { OnButton1UpInput.Invoke(); }
        OnButton2Input.Invoke(Input.GetButton(button2Name));
        if (Input.GetButtonDown(button2Name)) { OnButton2DownInput.Invoke(); }
        if (Input.GetButtonUp(button2Name)) { OnButton2UpInput.Invoke(); }
    }
}
