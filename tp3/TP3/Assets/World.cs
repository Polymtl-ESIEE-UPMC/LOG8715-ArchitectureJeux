using System.Collections.Generic;
using UnityEngine;

public sealed class World
{
    private static readonly World instance = new World();

    public int latency;

    public List<ExtrapolationComponent> extrapolations;

    static World()
    {
    }

    private World()
    {
        extrapolations = new List<ExtrapolationComponent>();
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