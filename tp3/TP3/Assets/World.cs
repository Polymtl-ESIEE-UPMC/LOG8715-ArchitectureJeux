using System.Collections.Generic;
using UnityEngine;

public sealed class World
{
    private static readonly World instance = new World();

    public int latency;

    public List<KeyCode> inputHistory;
    public Dictionary<uint, List<Vector2>> positionHistory;

    static World()
    {
    }

    private World()
    {
        inputHistory = new List<KeyCode>();
        positionHistory = new Dictionary<uint, List<Vector2>>();
        latency = 0;
    }

    public static World Instance
    {
        get
        {
            return instance;
        }
    }
}