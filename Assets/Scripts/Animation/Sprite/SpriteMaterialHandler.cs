using NUnit.Framework;
using UnityEngine;

public class SpriteMaterialHandler : MonoBehaviour
{
    [SerializeField] SpriteRenderer rend;

    public void SetMaterial(Material mat)
    {
        rend.material = mat;
    }
}
