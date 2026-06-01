using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] List<BaseModule> moduleCollection;

    [Button]
    public void CollectCurrentRefs()
    {
        moduleCollection.Clear();
        BaseModule[] modules = transform.GetComponentsInChildren<BaseModule>();
        Entity[] entities = transform.GetComponentsInChildren<Entity>();
        
        for(int i = 0; i < modules.Length; i++)
        {
            bool isOwned = false;
            for(int x = 0; x < entities.Length; x++)
            {
                if (entities[x].moduleCollection.Contains(modules[i])){
                    isOwned = true;
                    break;
                }
            }
            if(isOwned == false)
            {
                moduleCollection.Add(modules[i]);
            }
        }
    }

    private void OnEnable()
    {
        SetupEntity();
    }

    public void SetupEntity()
    {
        for (int i = 0; i < moduleCollection.Count; i++)
        {
            moduleCollection[i].SetOwner(this);
        }
    }

    public T GetModule<T>() where T : BaseModule
    {
        for (int i = 0; i < moduleCollection.Count; i++)
        {
            if (moduleCollection[i].GetType() == typeof(T))
            {
                return (T)moduleCollection[i];
            }
        }
        return null;
    }

    public List<T> GetModules<T>() where T : BaseModule
    {
        List<T> behaviourList = new List<T>();
        for (int i = 0; i < moduleCollection.Count; i++)
        {
            if (moduleCollection[i].GetType() == typeof(T))
            {
                behaviourList.Add((T)moduleCollection[i]);
            }
        }
        return behaviourList;
    }

    public bool TryGetModule<T>(out T behaviour) where T : BaseModule
    {
        behaviour = GetModule<T>();
        return behaviour != null;
    }

    public bool TryGetModules<T>(out List<T> behaviours) where T : BaseModule
    {
        behaviours = GetModules<T>();
        return behaviours != null && behaviours.Count > 0;
    }
}
