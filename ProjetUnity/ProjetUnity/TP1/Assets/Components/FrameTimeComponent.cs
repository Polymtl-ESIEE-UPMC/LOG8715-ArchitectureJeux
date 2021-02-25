public class FrameTimeComponent : IComponent
{
    public FrameTimeComponent(float time)
    {
        Time = time;
    }

   public float Time { get; set; }
}
