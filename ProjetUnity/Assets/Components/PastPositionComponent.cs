using System.Collections.Generic;
using UnityEngine;

public class PastPositionComponent : EntityComponent
{
    public PastPositionComponent(uint id)
    {
        this.id = id;
        PastPositions = new List<Vector2>();
    }
    public List<Vector2> PastPositions;
}

