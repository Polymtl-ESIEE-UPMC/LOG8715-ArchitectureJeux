public class LatencyCalculatorSystem : ISystem
{
    public string Name => "LatencyCalculatorSystem";

    public void UpdateSystem()
    {
        int max = 0;
        int time = Utils.SystemTime;
        ComponentsManager.Instance.ForEach<ReplicationMessage>((id, message) => {
            if (Utils.SystemTime - message.timeCreated > max)
                max = time - message.timeCreated;
        });

        if (max != 0)
            World.Instance.latency = max;

    }
}

