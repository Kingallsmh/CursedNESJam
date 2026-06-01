using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimatorModule : BaseModule
{
    [SerializeField] Animator animator;

    [FoldoutGroup("Events")]
    [SerializeField] UnityEvent<string, bool> onAnimatorBoolSet;
    [FoldoutGroup("Events")]
    [SerializeField] UnityEvent<string, int> onAnimatorIntSet;
    [FoldoutGroup("Events")]
    [SerializeField] UnityEvent<string, float> onAnimatorFloatSet;
    [FoldoutGroup("Events")]
    [SerializeField] UnityEvent<string> onAnimatorTriggerSet;
    [FoldoutGroup("Events")]
    [SerializeField] UnityEvent<RuntimeAnimatorController> onAnimatorControllerSet;

    public UnityEvent<string, bool> OnAnimatorBoolSet { get => onAnimatorBoolSet; }
    public UnityEvent<string, int> OnAnimatorIntSet { get => onAnimatorIntSet; }
    public UnityEvent<string, float> OnAnimatorFloatSet { get => onAnimatorFloatSet; }
    public UnityEvent<string> OnAnimatorTriggerSet { get => onAnimatorTriggerSet; }
    public UnityEvent<RuntimeAnimatorController> OnAnimatorControllerSet { get => onAnimatorControllerSet; }

    public void SetAnimatorBool(bool value, string name)
    {
        animator.SetBool(name, value);
        onAnimatorBoolSet.Invoke(name, value);
    }

    public void SetAnimatorInt(int value, string name)
    {
        animator.SetInteger(name, value);
        onAnimatorIntSet.Invoke(name, value);
    }

    public void SetAnimatorFloat(float value, string name)
    {
        animator.SetFloat(name, value);
        onAnimatorFloatSet.Invoke(name, value);
    }

    public void SetAnimatorTrigger(string name)
    {
        animator.SetTrigger(name);
        onAnimatorTriggerSet.Invoke(name);
    }

    public void SetAnimatorController(RuntimeAnimatorController controller)
    {
        animator.runtimeAnimatorController = controller;
        onAnimatorControllerSet.Invoke(controller);
    }
}
