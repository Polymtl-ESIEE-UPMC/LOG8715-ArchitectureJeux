public class StaticComponent : EntityComponent
{
    public StaticComponent(uint id, bool isStatic)
    {
        this.id = id;
        IsStatic = isStatic;
    }
    public bool IsStatic { get; set; }
}
