public class SizeComponent : EntityComponent
{
    public SizeComponent(uint id, float size)
    {
        this.id = id;
        this.Size = size;
    }
   public float Size { get; set; }
}
