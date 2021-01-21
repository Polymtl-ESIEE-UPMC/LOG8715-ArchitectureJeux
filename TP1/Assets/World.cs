using System.Collections.Generic;
using UnityEngine;

public sealed class World
{
    private static readonly World instance = new World();

    public List<PositionComponent> Positions;
    public List<SpeedComponent> Speeds;
    public List<SizeComponent> Sizes;
    public List<ColorComponent> Colors;
    public List<StaticComponent> Static;

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
    }

    public static World Instance
    {
        get
        {
            return instance;
        }
    }
}
