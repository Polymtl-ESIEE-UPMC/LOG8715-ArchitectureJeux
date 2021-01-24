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
                world.ColliderComponents[i].HasCollider = true;
            }
            world.CollisionWithEdges[i].HasCollision = false;
        }
    }
}
