using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    [SerializeField] Physics2DModule physics2DModule;
    [SerializeField] float gravityForce;
    [SerializeField] float terminalGravityVelocity;

    [SerializeField] bool isEnabled;

    private void FixedUpdate()
    {
        if (isEnabled == false) { return; }
        float gravityToApply = (-gravityForce * Time.deltaTime);//-1
        float currentVelocity = gravityToApply + physics2DModule.CurrentVelocity.y; //-1 + -5 = -6
        if(currentVelocity < -terminalGravityVelocity)
        {
            float dif = currentVelocity + terminalGravityVelocity; //-6 + 5 = -1
            gravityToApply = Mathf.Clamp(gravityToApply - dif, gravityToApply - dif, 0);//-1.5
        }
        physics2DModule.AddVelocity(new Vector3(0, gravityToApply, 0));
    }

    [Button]
    public void ToggleGravity(bool isEnabled)
    {
        this.isEnabled = isEnabled;
    }
}
