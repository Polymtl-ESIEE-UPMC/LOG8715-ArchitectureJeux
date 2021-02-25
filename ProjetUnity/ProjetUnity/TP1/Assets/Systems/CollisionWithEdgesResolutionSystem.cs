using UnityEngine;
using System;

public class CollisionWithEdgesResolutionSystem : ISystem
{
    public string Name => "CollisionWithEdgesResolutionSystem";
    private readonly World world;
    private readonly ECSManager ecs;

    public CollisionWithEdgesResolutionSystem()
    {
        world = World.Instance;
        ecs = ECSManager.Instance;
    }

    public void UpdateSystem()
    {
        if (world.IsFirstRun)
            FullScreenResolution();
        else
            UpperHalfScreenResolution();
    }

    private void FullScreenResolution()
    {
        for (int i = 0; i < world.CollisionWithEdges.Count; i++)
        {
            CollisionResolution(i);
        }
    }

    private void UpperHalfScreenResolution()
    {
        for (int i = 0; i < world.CollisionWithEdges.Count; i++)
        {
            if (world.EntityInUpperHalf(i))
                CollisionResolution(i);
        }
    }
    private void CollisionResolution(int id)
    {
        if (world.CollisionWithEdges[id].HasCollision)
        {
            world.Sizes[id].Size = ecs.Config.allShapesToSpawn[id].size;
            world.Colliders[id].HasCollider = true;

            //reajust the entity position as to not be stuck in the wall after the resizing.
            float xSpeedSign = 0;
            if (world.Speeds[id].Speed.x != 0)
            {
                xSpeedSign = world.Speeds[id].Speed.x / Math.Abs(world.Speeds[id].Speed.x);
            }
            float ySpeedSign = 0;
            if (world.Speeds[id].Speed.y != 0)
            {
                ySpeedSign = world.Speeds[id].Speed.y / Math.Abs(world.Speeds[id].Speed.y);
            }
            Vector2 newPosition = new Vector2(world.Positions[id].Position.x + world.Sizes[id].Size * xSpeedSign / 2, world.Positions[id].Position.y + world.Sizes[id].Size * ySpeedSign / 2);
            world.Positions[id].Position = newPosition;
        }
        world.CollisionWithEdges[id].HasCollision = false;
    }
}
