public class ColliderComponent : EntityComponent
{
    public ColliderComponent(uint id, bool hasCollider)
    {
        this.id = id;
        HasCollider = hasCollider;
    }
    public bool HasCollider { get; set; }
}
