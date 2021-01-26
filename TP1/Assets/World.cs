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
    public List<CollisionWithEntityComponent> CollisionsWithEntities;
    public List<ColliderComponent> Colliders;

    public List<PastPositionComponent> PastPositions;
    public List<PastSpeedComponent> PastSpeeds;
    public List<PastColorComponent> PastColors;
    public List<PastSizeComponent> PastSizes;
    public List<PastColliderComponent> PastColliders;
    public List<FrameTimeComponent> frameTimes;



    static World()
    {
    }

    public World(World world)
    {
        Positions = new List<PositionComponent>(world.Positions);
    }

    private World()
    {
        Positions = new List<PositionComponent>();
        Speeds = new List<SpeedComponent>();
        Sizes = new List<SizeComponent>();
        Colors = new List<ColorComponent>();
        Static = new List<StaticComponent>();
        CollisionWithEdges = new List<CollisionWithEdgesComponent>();
        CollisionsWithEntities = new List<CollisionWithEntityComponent>();
        Colliders = new List<ColliderComponent>();
        PastPositions = new List<PastPositionComponent>();
        frameTimes = new List<FrameTimeComponent>();
        PastSpeeds = new List<PastSpeedComponent>();
        PastColors = new List<PastColorComponent>();
        PastSizes = new List<PastSizeComponent>();
        PastColliders = new List<PastColliderComponent>();
    }

    public static World Instance
    {
        get
        {
            return instance;
        }
    }
}
