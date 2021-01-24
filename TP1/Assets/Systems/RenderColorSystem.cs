public class RenderColorSystem : ISystem
{
    public string Name => "RenderColorSystem";
    private readonly World world;
    private readonly ECSManager ecs;

    public RenderColorSystem()
    {
        world = World.Instance;
        ecs = ECSManager.Instance;
    }
    public void UpdateSystem()
    {
        foreach (ColorComponent color in world.Colors)
        {
            ecs.UpdateShapeColor(color.id, color.Color);
        }
    }
}
