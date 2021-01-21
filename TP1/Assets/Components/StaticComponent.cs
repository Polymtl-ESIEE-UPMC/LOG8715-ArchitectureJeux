public class StaticComponent : EntityComponent
{
    public bool IsStatic { get; set; }
    public StaticComponent(bool isStatic)
    {
        this.IsStatic = isStatic;
    }
}
