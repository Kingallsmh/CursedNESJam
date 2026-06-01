using System.Collections.Generic;
using UnityEngine;

public class MaterialFlickerConstant : MonoBehaviour
{
    [SerializeField] SpriteMaterialHandler matHandler;
    [SerializeField] List<Material> materialList;
    [SerializeField] float timePerMaterial;

    Timer timer = new Timer();
    int currentIndex;

    private void Update()
    {
        if (timer.Done())
        {
            matHandler.SetMaterial(GetNextMaterial());
            timer.SetTime(timePerMaterial);
        }
        else
        {
            timer.Update();
        }
    }

    Material GetNextMaterial()
    {
        currentIndex += 1;
        if(currentIndex >= materialList.Count) { currentIndex = 0; }
        return materialList[currentIndex];
    }
}
