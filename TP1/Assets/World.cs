using System.Collections.Generic;
using UnityEngine;

public sealed class World
{
    private static readonly World instance = new World();

    public Dictionary<uint, Vector2> Positions { get; set; }

    public Dictionary<uint, Vector2> Speeds { get; set; }
    public Dictionary<uint, float> Sizes { get; set; }
    public Dictionary<uint, Color> Colors { get; set; }

    static World()
    {
    }

    private World()
    {
        Positions = new Dictionary<uint, Vector2>();
        Speeds = new Dictionary<uint, Vector2>();
        Sizes = new Dictionary<uint, float>();
        Colors = new Dictionary<uint, Color>();
    }

    public static World Instance
    {
        get
        {
            return instance;
        }
    }
}
