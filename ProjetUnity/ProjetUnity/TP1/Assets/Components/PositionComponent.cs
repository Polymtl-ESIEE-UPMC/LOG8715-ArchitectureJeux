using UnityEngine;

public class PositionComponent : EntityComponent
{
    public PositionComponent(uint id, Vector2 position)
    {
        this.id = id;
        this.Position = position;
    }
    public Vector2 Position { get; set; }
}

