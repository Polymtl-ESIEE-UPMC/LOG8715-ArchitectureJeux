﻿using System.Collections.Generic;
using UnityEngine;

class PositionUpdateSystem : ISystem
{

    private World world;
    public PositionUpdateSystem()
    {
        world = World.Instance;
    }
    public string Name => "PositionUpdateSystem";

    public void UpdateSystem()
    {
        for (int i = 0; i < world.Positions.Count; i++)
        {
            if (!world.Static[i].IsStatic)
            {
                Vector2 newPosition = world.Positions[i].Position + world.Speeds[i].Speed*Time.deltaTime;
                world.Positions[i].Position = newPosition;
            }
        }
    }
}