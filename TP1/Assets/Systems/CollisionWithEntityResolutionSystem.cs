﻿using UnityEngine;

public class CollisionWithEntityResolutionSystem : ISystem
{
    public string Name => "CollisionWithEntityResolutionSystem";
    private readonly World world;
    private readonly ECSManager ecs;

    public CollisionWithEntityResolutionSystem()
    {
        world = World.Instance;
        ecs = ECSManager.Instance;
    }

    public void UpdateSystem()
    {
        for (int i = 0; i < world.CollisionsWithEntity.Count; i++)
        {
            if (hasCollision(i) && !isStatic(i) && isMoreThanMinSize(i))
            {
                world.Sizes[i].Size = world.Sizes[i].Size / 2;

            }
            if (!isMoreThanMinSize(i))
            {
                world.ColliderComponents[i].HasCollider = false;
            }
            world.CollisionsWithEntity[i].HasCollision = false;
        }
    }

    private bool isStatic(int id)
    {
        return world.Static[id].IsStatic;
    }

    private bool hasCollision(int id)
    {
        return world.CollisionsWithEntity[id].HasCollision;
    }

    private bool isMoreThanMinSize(int id)
    {
        return world.Sizes[id].Size > ecs.Config.minSize;
    }
}

