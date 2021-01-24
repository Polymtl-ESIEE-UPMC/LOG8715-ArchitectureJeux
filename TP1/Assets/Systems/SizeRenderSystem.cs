public class SizeRenderSystem : ISystem
{
    public string Name => "SizeRenderSystem";
    private readonly ECSManager ecs;
    private readonly World world;

    public SizeRenderSystem()
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