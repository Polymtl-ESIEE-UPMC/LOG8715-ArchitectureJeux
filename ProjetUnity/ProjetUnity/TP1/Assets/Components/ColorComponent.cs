using UnityEngine;

public class ColorComponent : EntityComponent
{
    public ColorComponent(uint id, Color color)
    {
        this.id = id;
        this.Color = color;
    }
    public Color Color { get; set; }
}