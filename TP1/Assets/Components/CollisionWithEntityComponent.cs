
public class CollisionWithEntityComponent : IComponent
{

    public CollisionWithEntityComponent(bool hasCollision)
    {
        HasCollision = hasCollision;
    }
    public bool HasCollision { get; set; }
}

