using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseModule : MonoBehaviour
{
    Entity owner;

    public void SetOwner(Entity owner)
    {
        this.owner = owner;
    }

    public Entity GetOwner()
    {
        return owner;
    }
}
