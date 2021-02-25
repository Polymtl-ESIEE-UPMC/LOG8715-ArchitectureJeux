using System.Collections.Generic;

public class PastColliderComponent : EntityComponent
{
    public PastColliderComponent(uint id)
    {
        this.id = id;
        PastColliders = new List<bool>();
    }
    public List<bool> PastColliders;
}
