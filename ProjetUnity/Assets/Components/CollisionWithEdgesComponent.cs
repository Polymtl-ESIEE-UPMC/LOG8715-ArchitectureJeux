public class CollisionWithEdgesComponent : EntityComponent
{
    public CollisionWithEdgesComponent(uint id, bool hasCollision)
    {
        this.id = id;
        HasCollision = hasCollision;
    }
   public bool HasCollision { get; set; }
}

