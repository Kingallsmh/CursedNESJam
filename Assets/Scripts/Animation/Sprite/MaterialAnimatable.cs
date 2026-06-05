using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

public class MaterialAnimatable : MonoBehaviour
{
    [SerializeField] SpriteMaterialHandler matHandler;
    [SerializeField] List<Material> materialList;

    [ReadOnly] public int wantedMat;
    int currentMat;

    private void Update()
    {
        if(currentMat == wantedMat) { return; }
        matHandler.SetMaterial(materialList[wantedMat]);
        currentMat = wantedMat;
    }
}
