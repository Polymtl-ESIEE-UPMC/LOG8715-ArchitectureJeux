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
        RenderPosition();
        RenderColor();
        RenderSize();
    }

    private void RenderSize()
    {
        foreach (SizeComponent size in world.Sizes)
        {
            ecs.UpdateShapeSize(size.id, size.Size);
        }
    }

    private void RenderColor()
    {
        foreach (ColorComponent color in world.Colors)
        {
            ecs.UpdateShapeColor(color.id, color.Color);
        }
    }

    private void RenderPosition()
    {
        foreach (PositionComponent position in world.Positions)
        {
            ecs.UpdateShapePosition(position.id, position.Position);
        }
    }

}
