class UpdateFirstRunSystem : ISystem
{
    public string Name => "UpdateFirstRunSystem";
    private readonly World world;

    public UpdateFirstRunSystem()
    {
        world = World.Instance;
    }

    public void UpdateSystem()
    {
        world.IsFirstRun = !world.IsFirstRun;
    }
}

