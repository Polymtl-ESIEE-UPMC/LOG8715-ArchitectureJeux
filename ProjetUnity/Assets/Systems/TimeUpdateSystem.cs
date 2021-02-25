using UnityEngine;

public class TimeUpdateSystem : ISystem
{
    public string Name => "TimeUpdateSystem";
    private readonly World world;

    public TimeUpdateSystem()
    {
        world = World.Instance;
    }
    public void UpdateSystem()
    {
        world.frameTimes.Add(new FrameTimeComponent(Time.time));
    }
}
