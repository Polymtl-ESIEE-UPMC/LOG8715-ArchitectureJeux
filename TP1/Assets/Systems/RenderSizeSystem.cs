public class RenderSizeSystem : ISystem
{
    public string Name => "RenderSizeSystem";
    private readonly ECSManager ecs;
    private readonly World world;

    public RenderSizeSystem()
    {
        ecs = ECSManager.Instance;
        world = World.Instance;
    }

    public void UpdateSystem()
    {
       foreach (SizeComponent size in world.Sizes)
        {
            ecs.UpdateShapeSize(size.id, size.Size);
        }
    }
}