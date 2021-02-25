using UnityEngine;

class GeneralCollisionResolutionSystem : ISystem
{
    public string Name => "GeneralCollisionResolutionSystem";
    private readonly World world;

    public GeneralCollisionResolutionSystem()
    {
        world = World.Instance;
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
        for (int i = 0; i < world.Speeds.Count; i++)
        {
            CollisionResolution(i);
        }
    }

    private void UpperHalfScreenResolution()
    {
        for (int i = 0; i < world.Speeds.Count; i++)
        {  
            if (world.EntityInUpperHalf(i))
                CollisionResolution(i);
        }
    }

    private void CollisionResolution(int id)
    {
        if (world.CollisionsWithEntities[id].HasCollision || world.CollisionWithEdges[id].HasCollision)
        {
            world.Speeds[id].Speed = new Vector2(-world.Speeds[id].Speed.x, -world.Speeds[id].Speed.y);
        }
    }
}

