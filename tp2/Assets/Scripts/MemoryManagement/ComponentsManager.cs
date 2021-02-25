#define BAD_PERF // TODO CHANGEZ MOI. Mettre en commentaire pour utiliser votre propre structure

using System;
using UnityEngine;

#if BAD_PERF
using InnerType = System.Collections.Generic.List<IComponent>;
using AllComponents = System.Collections.Generic.Dictionary<uint, System.Collections.Generic.List<IComponent>>;
#else
using InnerType = ...; // TODO CHANGEZ MOI, UTILISEZ VOTRE PROPRE TYPE ICI
using AllComponents = ...; // TODO CHANGEZ MOI, UTILISEZ VOTRE PROPRE TYPE ICI
#endif

// Appeler GetHashCode sur un Type est couteux. Cette classe sert a precalculer le hashcode
public static class TypeRegistry<T> where T : IComponent
{
    public static uint typeID = (uint)Mathf.Abs(default(T).GetRandomNumber()) % ComponentsManager.maxEntities;
}

public class Singleton<V> where V : new()
{
    private static bool isInitiated = false;
    private static V _instance;
    public static V Instance
    {
        get
        {
            if (!isInitiated)
            {
                isInitiated = true;
                _instance = new V();
            }
            return _instance;
        }
    }
    protected Singleton() { }
}

internal class ComponentsManager : Singleton<ComponentsManager>
{
    private AllComponents _allComponents = new AllComponents();

    public const int maxEntities = 2000;

    public void DebugPrint()
    {
        string toPrint = "";
        var allComponents = Instance.DebugGetAllComponents();
        foreach (var type in allComponents)
        {
            toPrint += $"{type}: \n";
#if !BAD_PERF
            foreach (var component in type)
#else
            foreach (var component in type.Value)
#endif
            {
#if BAD_PERF
               // toPrint += $"\t{component.Key}: {component.Value}\n";
#else
                //toPrint += $"\t{component}: {component}\n";
#endif
            }
            toPrint += "\n";
        }
        Debug.Log(toPrint);
    }

    // CRUD
    public void SetComponent<T>(EntityComponent entityID, IComponent component) where T : IComponent
    {
        if (!_allComponents.ContainsKey(TypeRegistry<T>.typeID))
        {
            _allComponents[TypeRegistry<T>.typeID] = new InnerType();
        }
        if (_allComponents[TypeRegistry<T>.typeID].Count <= entityID.id)
            _allComponents[TypeRegistry<T>.typeID].Add(component);
        else
            _allComponents[TypeRegistry<T>.typeID][(int)entityID.id] = component;
    }
    public void RemoveComponent<T>(EntityComponent entityID) where T : IComponent
    {
        if (_allComponents[TypeRegistry<T>.typeID].Count > entityID.id)
            _allComponents[TypeRegistry<T>.typeID][(int)entityID.id] = null;
    }
    public T GetComponent<T>(EntityComponent entityID) where T : IComponent
    {
        return (T)_allComponents[TypeRegistry<T>.typeID][(int)entityID.id];
    }
    public bool TryGetComponent<T>(EntityComponent entityID, out T component) where T : IComponent
    {
        if (_allComponents.ContainsKey(TypeRegistry<T>.typeID))
        {
            if (_allComponents[TypeRegistry<T>.typeID].Count > entityID.id && _allComponents[TypeRegistry<T>.typeID][(int)entityID.id] != null)
            {
                component = (T)_allComponents[TypeRegistry<T>.typeID][(int)entityID.id];
                return true;
            }
        }
        component = default;
        return false;
    }

    public bool EntityContains<T>(EntityComponent entity) where T : IComponent
    {
        return _allComponents[TypeRegistry<T>.typeID][(int)entity.id] != null;
    }

    public void ClearComponents<T>() where T : IComponent
    {
        int size = _allComponents.ContainsKey(TypeRegistry<EntityComponent>.typeID) ? _allComponents[TypeRegistry<EntityComponent>.typeID].Count : maxEntities;
        if (!_allComponents.ContainsKey(TypeRegistry<T>.typeID))
        {
            _allComponents[TypeRegistry<T>.typeID] = new InnerType();
            for (int i = 0; i < size; i++)
            {
                _allComponents[TypeRegistry<T>.typeID].Add(null);
            }
        }
        else
        {
            for (int i = 0; i < size; i++)
            {
                _allComponents[TypeRegistry<T>.typeID][i] = null;
            }
        }
    }

    public void ForEach<T1>(Action<EntityComponent, T1> lambda) where T1 : IComponent
    {
        var allEntities = _allComponents[TypeRegistry<EntityComponent>.typeID];
        var t1List = _allComponents[TypeRegistry<T1>.typeID];
        var t1Size = t1List.Count;
        IComponent t1Entity;
        foreach (EntityComponent entity in allEntities)
        {
            int index = (int)entity.id;
            if (t1Size > index)
            {
                t1Entity = t1List[index];
                if (t1Entity != null)
                {
                    lambda(entity, (T1)t1Entity);
                }
            }
            else
            {
                return;
            }
        }
    }

    public void ForEach<T1, T2>(Action<EntityComponent, T1, T2> lambda) where T1 : IComponent where T2 : IComponent
    {
        var allEntities = _allComponents[TypeRegistry<EntityComponent>.typeID];
        var t1List = _allComponents[TypeRegistry<T1>.typeID];
        var t2List = _allComponents[TypeRegistry<T2>.typeID];
        var t1Size = t1List.Count;
        var t2Size = t2List.Count;
        IComponent t1Entity, t2Entity;
        foreach (EntityComponent entity in allEntities)
        {
            int index = (int)entity.id;
            if (t1Size > index && t2Size > index)
            {
                t1Entity = t1List[index];
                t2Entity = t2List[index];
                if (t1Entity != null && t2List[index] != null)
                {
                    lambda(entity, (T1)t1Entity, (T2)t2Entity);
                }
            }
            else
            {
                return;
            }
        }
    }

    public void ForEach<T1, T2, T3>(Action<EntityComponent, T1, T2, T3> lambda) where T1 : IComponent where T2 : IComponent where T3 : IComponent
    {
        var allEntities = _allComponents[TypeRegistry<EntityComponent>.typeID];
        var t1List = _allComponents[TypeRegistry<T1>.typeID];
        var t2List = _allComponents[TypeRegistry<T2>.typeID];
        var t3List = _allComponents[TypeRegistry<T3>.typeID];
        var t1Size = t1List.Count;
        var t2Size = t2List.Count;
        var t3Size = t3List.Count;
        IComponent t1Entity, t2Entity, t3Entity;
        foreach (EntityComponent entity in allEntities)
        {
            int index = (int)entity.id;

            if (t1Size > index && t2Size > index && t3Size > index)
            {
                t1Entity = t1List[index];
                t2Entity = t2List[index];
                t3Entity = t3List[index];
                if (t1Entity != null && t2Entity != null && t3Entity != null)
                {
                    lambda(entity, (T1)t1Entity, (T2)t2Entity, (T3)t3Entity);
                }
            }
            else
            {
                return;
            }
        }
    }

    public void ForEach<T1, T2, T3, T4>(Action<EntityComponent, T1, T2, T3, T4> lambda) where T1 : IComponent where T2 : IComponent where T3 : IComponent where T4 : IComponent
    {
        var allEntities = _allComponents[TypeRegistry<EntityComponent>.typeID];
        var t1List = _allComponents[TypeRegistry<T1>.typeID];
        var t2List = _allComponents[TypeRegistry<T2>.typeID];
        var t3List = _allComponents[TypeRegistry<T3>.typeID];
        var t4List = _allComponents[TypeRegistry<T4>.typeID];
        var t1Size = t1List.Count;
        var t2Size = t2List.Count;
        var t3Size = t3List.Count;
        var t4Size = t4List.Count;
        IComponent t1Entity, t2Entity, t3Entity, t4Entity;
        foreach (EntityComponent entity in allEntities)
        {
            int index = (int)entity.id;
            if (t1Size > index && t2Size > index && t3Size > index && t4Size > index)
            {
                t1Entity = t1List[index];
                t2Entity = t2List[index];
                t3Entity = t3List[index];
                t4Entity = t4List[index];
                if (t1Entity != null && t2Entity != null && t3Entity != null && t4Entity != null)
                {
                    lambda(entity, (T1)t1Entity, (T2)t2Entity, (T3)t3Entity, (T4)t4Entity);
                }
            } 
            else
            {
                return;
            }
        }
    }

    public AllComponents DebugGetAllComponents()
    {
        return _allComponents;
    }
}
