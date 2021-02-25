public class SizeComponent : EntityComponent
{
    public SizeComponent(uint id, float size)
    {
        this.id = id;
        Size = size;
    }
    public float Size;
}