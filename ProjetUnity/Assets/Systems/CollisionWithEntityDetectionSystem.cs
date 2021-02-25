using System;
using UnityEngine;

public class CollisionWithEntityDetectionSystem : ISystem
{
    public string Name => "CollisionWithEntityDetectionSystem";
    private readonly World world;
    public CollisionWithEntityDetectionSystem()
    {
        world = World.Instance;
    }

    public void UpdateSystem()
    {
        if (world.IsFirstRun)
            FullScreenDetection();
        else
            UpperHalfScreenDetection();

    }

    private void FullScreenDetection()
    {
        foreach (PositionComponent entity in world.Positions)
        {
            CheckCollisionWithEdges(entity);
        }
    }

    private void UpperHalfScreenDetection()
    {
        foreach (PositionComponent entity in world.Positions)
        {
            if (world.EntityInUpperHalf((int) entity.id))
                CheckCollisionWithEdges(entity);
        }
    }

    private void CheckCollisionWithEdges(PositionComponent entity)
    {
        foreach (PositionComponent otherEntity in world.Positions)
        {
            if (entity == otherEntity || !world.Colliders[(int)entity.id].HasCollider || !world.Colliders[(int)otherEntity.id].HasCollider)
                continue;

            float dx = entity.Position.x - otherEntity.Position.x;
            float dy = entity.Position.y - otherEntity.Position.y;
            float distance = (float)Math.Sqrt(dx * dx + dy * dy);

            float radius = world.Sizes[(int)entity.id].Size / 2f;
            float otherRadius = world.Sizes[(int)otherEntity.id].Size / 2f;
            if (distance < radius + otherRadius)
            {
                world.CollisionsWithEntities[(int)entity.id].HasCollision = true;
                world.CollisionsWithEntities[(int)otherEntity.id].HasCollision = true;
            }
        }
    }
}

