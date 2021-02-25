using System.Collections.Generic;

public class PastSizeComponent : EntityComponent
{
    public PastSizeComponent(uint id)
    {
        this.id = id;
        PastSizes = new List<float>();
    }

    public List<float> PastSizes;
}
