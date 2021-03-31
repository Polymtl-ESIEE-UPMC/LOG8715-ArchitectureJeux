using UnityEngine;
using System;
public class ExtrapolationSystem : ISystem
{
    public string Name => "ExtrapolationSystem";

    public void UpdateSystem()
    {
        if (ECSManager.Instance.NetworkManager.isClient)
        {
            int max = 0;
            ComponentsManager.Instance.ForEach<ReplicationMessage>((id, message) => {
                if (Utils.SystemTime - message.timeCreated > max)
                    max = Utils.SystemTime - message.timeCreated;

            });

            if (max != 0)
                World.Instance.latency = max;
            int nbFrameToExtrapolate = World.Instance.latency / 20;

            Debug.Log("Latency=" + World.Instance.latency);

            CircleCollisionDetectionSystem circleCollision = new CircleCollisionDetectionSystem();
            WallCollisionDetectionSystem wallCollision = new WallCollisionDetectionSystem();
            BounceBackSystem bounceBack = new BounceBackSystem();
            PositionUpdateSystem positionUpdate = new PositionUpdateSystem();

            for (int i = 0; i < nbFrameToExtrapolate; i++)
            {
                circleCollision.UpdateSystem();
                wallCollision.UpdateSystem();
                bounceBack.UpdateSystem();
                positionUpdate.UpdateSystem();
            }
        }
    }
}

