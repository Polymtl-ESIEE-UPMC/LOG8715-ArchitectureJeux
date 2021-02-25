using UnityEngine;

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
        if (world.IsFirstRun)
            FullScreenResolution();
        else
            UpperHalfScreenResolution();
    }


    private void FullScreenResolution()
    {
        for (int i = 0; i < world.CollisionsWithEntities.Count; i++)
        {
            CollisionResolution(i);
        }
    }

    private void UpperHalfScreenResolution()
    {
        for (int i = 0; i < world.CollisionsWithEntities.Count; i++)
        {
            if (world.EntityInUpperHalf(i))
                CollisionResolution(i);
        }
    }

    private void CollisionResolution(int id)
    {
        if (hasCollision(id) && !isStatic(id) && isMoreThanMinSize(id))
        {
            world.Sizes[id].Size = world.Sizes[id].Size / 2;
        }
        if (!isMoreThanMinSize(id) && !world.Static[id].IsStatic)
        {
            world.Colliders[id].HasCollider = false;
        }
        world.CollisionsWithEntities[id].HasCollision = false;
    }

    private bool isStatic(int id)
    {
        return world.Static[id].IsStatic;
    }

    private bool hasCollision(int id)
    {
        return world.CollisionsWithEntities[id].HasCollision;
    }

    private bool isMoreThanMinSize(int id)
    {
        return world.Sizes[id].Size > ecs.Config.minSize;
    }
}

