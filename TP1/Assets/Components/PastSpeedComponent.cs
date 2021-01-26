using System.Collections.Generic;
using UnityEngine;

public class PastSpeedComponent : EntityComponent
{
   public PastSpeedComponent(uint id)
    {
        this.id = id;
        PastSpeeds = new List<Vector2>();
    }
    public List<Vector2> PastSpeeds;
}

