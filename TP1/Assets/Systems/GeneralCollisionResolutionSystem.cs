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
        for (int i = 0; i < world.Speeds.Count; i++)
        {
            if (world.CollisionsWithEntity[i].HasCollision || world.CollisionWithEdges[i].HasCollision)
            {
                world.Speeds[i].Speed = new Vector2(-world.Speeds[i].Speed.x, -world.Speeds[i].Speed.y);
            }
        }
    }
}

