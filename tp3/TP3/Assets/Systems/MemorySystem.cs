
using System.Collections.Generic;
using UnityEngine;
public class MemorySystem : ISystem
{
    public string Name => "MemorySystem";

    public void UpdateSystem()
    {
        ComponentsManager.Instance.ForEach((EntityComponent entity, ShapeComponent shape) =>
       {
           if (World.Instance.positionHistory.ContainsKey(entity.id))
           {
               World.Instance.positionHistory[entity.id].Add(shape.pos);
           }
           else
           {
               List<Vector2> positions = new List<Vector2>();
               positions.Add(shape.pos);
               World.Instance.positionHistory.Add(entity.id, positions);
           }
       });

        if (ECSManager.Instance.NetworkManager.isClient)
        {
            uint playerId = (uint)CustomNetworkManager.Singleton.LocalClientId;
            if (World.Instance.positionHistory.ContainsKey(playerId) && World.Instance.positionHistory[playerId].Count > 0)
            {

                if (ComponentsManager.Instance.TryGetComponent(new EntityComponent(playerId), out InputMessage input))
                {
                    World.Instance.inputHistory.Add((KeyCode)input.keycode[input.keycode.Count - 1]);
                }
                else
                {
                    World.Instance.inputHistory.Add(KeyCode.None);
                }
            }
        }

    }
}
