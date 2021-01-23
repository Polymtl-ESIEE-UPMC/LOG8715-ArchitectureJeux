class CollisionWithEntityDetectionSystem : ISystem
{
    public string Name => "CollisionWithEntityDetectionSystem";
    private readonly World world;
    public CollisionWithEntityDetectionSystem()
    {
        world = World.Instance;
    }

    public void UpdateSystem()
    {
        throw new System.NotImplementedException();
    }
}
