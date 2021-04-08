/// <summary>
/// Clear components that shouldn't exist by the end of the frame
/// </summary>
using System.Collections.Generic;
using UnityEngine;
public class ClearEndOfFrameComponentsSystem : ISystem
{
    public string Name
    {
        get 
        {
            return GetType().Name;
        }
    }

    public void UpdateSystem()
    {
        ComponentsManager.Instance.ClearComponents<ReplicationMessage>();
        ComponentsManager.Instance.ClearComponents<CollisionEventComponent>();
        ComponentsManager.Instance.ClearComponents<InputMessage>();

        //only keep up to 2000 frames of informations.
        if (World.Instance.inputHistory.Count > 2000)
        {
            World.Instance.inputHistory.RemoveRange(0, World.Instance.inputHistory.Count / 2);
        }
        foreach (var entry in World.Instance.positionHistory)
        {
            if (entry.Value.Count > 2000)
            {
                entry.Value.RemoveRange(0, entry.Value.Count / 2);
            }
        }
    }
}

