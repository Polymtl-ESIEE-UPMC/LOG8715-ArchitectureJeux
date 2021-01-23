public class CollisionWithEdgesComponent : IComponent
{
    public CollisionWithEdgesComponent(bool hasCollision)
    {
        HasCollision = hasCollision;
    }
   public bool HasCollision { get; set; }
}

