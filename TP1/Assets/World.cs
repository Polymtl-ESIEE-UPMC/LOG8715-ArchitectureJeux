using System.Collections.Generic;

public sealed class World
{
    private static readonly World instance = new World();

    public List<PositionComponent> Positions;
    public List<SpeedComponent> Speeds;
    public List<SizeComponent> Sizes;
    public List<ColorComponent> Colors;
    public List<StaticComponent> Static;
    public List<CollisionWithEdgesComponent> CollisionWithEdges;
    public List<CollisionWithEntityComponent> CollisionsWithEntity;

    static World()
    {
    }

    private World()
    {
        Positions = new List<PositionComponent>();
        Speeds = new List<SpeedComponent>();
        Sizes = new List<SizeComponent>();
        Colors = new List<ColorComponent>();
        Static = new List<StaticComponent>();
        CollisionWithEdges = new List<CollisionWithEdgesComponent>();
        CollisionsWithEntity = new List<CollisionWithEntityComponent>();
    }

    public static World Instance
    {
        get
        {
            return instance;
        }
    }
}
