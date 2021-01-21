using System.Collections.Generic;
using UnityEngine;

class PositionUpdateSystem : ISystem
{

    private World world;
    private ECSManager ecs;
    public PositionUpdateSystem()
    {
        world = World.Instance;
        ecs = ECSManager.Instance;
    }
    public string Name => "PositionUpdateSystem";

    public void UpdateSystem()
    {
        for (int i = 0; i < world.Positions.Count; i++)
        {
            if (!world.Static[i].IsStatic)
            {
                Vector2 newPosition = world.Positions[i].Position + world.Speeds[i].Speed;
                ecs.UpdateShapePosition((uint)i, newPosition);
                world.Positions[i].Position = newPosition;
            }
        }
    }
}