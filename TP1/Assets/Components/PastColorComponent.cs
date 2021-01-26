using System.Collections.Generic;
using UnityEngine;

public class PastColorComponent : EntityComponent
{
    public PastColorComponent(uint id)
    {
        this.id = id;
        PastColors = new List<Color>();
    }

    public List<Color> PastColors;
}
