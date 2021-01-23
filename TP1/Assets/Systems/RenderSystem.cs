public class RenderSystem : ISystem
{
    private readonly ECSManager ecs;
    private readonly World world;
    public string Name => "RenderSystem";

    public RenderSystem()
    {
        ecs = ECSManager.Instance;
        world = World.Instance;
    }

    public void UpdateSystem()
    {
        foreach (PositionComponent position in this.world.Positions)
        {
            ecs.UpdateShapePosition(position.id, position.Position);
        }
    }

}
