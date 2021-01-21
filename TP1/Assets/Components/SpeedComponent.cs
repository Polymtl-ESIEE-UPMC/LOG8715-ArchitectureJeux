using UnityEngine;

public class SpeedComponent : EntityComponent
{

    public SpeedComponent(uint id, Vector2 speed)
    {
        this.id = id;
        this.Speed = speed;
    }

    public Vector2 Speed { get; set; }
}