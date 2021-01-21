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
        for (uint i = 0; i < world.Positions.Count; i++)
        {
            Vector2 newPosition = world.Positions[i] + world.Speeds[i];
            ecs.UpdateShapePosition(i, newPosition);
            world.Positions[i] = newPosition;
        }
    }
}