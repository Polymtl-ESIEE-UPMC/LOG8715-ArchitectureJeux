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
        for (int i = 0; i < world.CollisionWithEdges.Count; i++)
        {
            if (world.CollisionWithEdges[i].HasCollision)
            {
                world.Sizes[i].Size = ecs.Config.allShapesToSpawn[i].size;
                world.Colliders[i].HasCollider = true;

                //reajust the entity position as to not be stuck in the wall after the resizing.
                float xSpeedSign = 0;
                if (world.Speeds[i].Speed.x != 0)
                {
                    xSpeedSign = world.Speeds[i].Speed.x / Math.Abs(world.Speeds[i].Speed.x);
                }
                float ySpeedSign = 0;
                if (world.Speeds[i].Speed.y != 0) 
                { 
                    ySpeedSign = world.Speeds[i].Speed.y / Math.Abs(world.Speeds[i].Speed.y);
                }
                Vector2 newPosition = new Vector2(world.Positions[i].Position.x + world.Sizes[i].Size * xSpeedSign/2, world.Positions[i].Position.y + world.Sizes[i].Size * ySpeedSign/2);
                world.Positions[i].Position = newPosition;
            }
            world.CollisionWithEdges[i].HasCollision = false;
        }
    }
}
