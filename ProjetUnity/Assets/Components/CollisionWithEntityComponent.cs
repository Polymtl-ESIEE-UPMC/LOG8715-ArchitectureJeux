
public class CollisionWithEntityComponent : EntityComponent
{
    public CollisionWithEntityComponent(uint id, bool hasCollision)
    {
        this.id = id;
        HasCollision = hasCollision;
    }
    public bool HasCollision { get; set; }
}

