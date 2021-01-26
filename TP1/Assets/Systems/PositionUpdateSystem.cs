using System.Collections.Generic;
using UnityEngine;

class PositionUpdateSystem : ISystem
{

    private World world;
    public PositionUpdateSystem()
    {
        world = World.Instance;
    }
    public string Name => "PositionUpdateSystem";

    public void UpdateSystem()
    {
        if (world.IsFirstRun)
            FullScreenUpdate();
        else
            UpperHalfScreenUpdate();
    }

    private void FullScreenUpdate()
    {
        for (int i = 0; i < world.Positions.Count; i++)
        {
            UpdatePosition(i);
        }
    }

    private void UpperHalfScreenUpdate()
    {
        for (int i = 0; i < world.Positions.Count; i++)
        {
            if (world.EntityInUpperHalf(i))
                UpdatePosition(i);
        }
    }

    private void UpdatePosition(int id)
    {
        if (!world.Static[id].IsStatic)
        {
            Vector2 newPosition = world.Positions[id].Position + world.Speeds[id].Speed * Time.deltaTime;
            world.Positions[id].Position = newPosition;
        }
    }
}