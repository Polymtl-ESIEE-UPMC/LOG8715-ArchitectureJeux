using UnityEngine;
using System.Collections.Generic;
public class ExtrapolationSystem : ISystem
{
    public string Name => "ExtrapolationSystem";

    public void UpdateSystem()
    {
        if (ECSManager.Instance.NetworkManager.isClient)
        {
            int max = 0;
            int time = Utils.SystemTime;
            List<uint> ids = new List<uint>();
            ComponentsManager.Instance.ForEach<ReplicationMessage>((id, message) => {
                if (Utils.SystemTime - message.timeCreated > max)
                    max = time - message.timeCreated;
                ids.Add(id);
            });

            World.Instance.latency = max;

            int nbFrameToExtrapolate = World.Instance.latency / 20;

            Debug.Log("Latency=" + World.Instance.latency);

            CircleCollisionDetectionSystem circleCollision = new CircleCollisionDetectionSystem();
            WallCollisionDetectionSystem wallCollision = new WallCollisionDetectionSystem();
            BounceBackSystem bounceBack = new BounceBackSystem();
            PositionUpdateSystem positionUpdate = new PositionUpdateSystem();
            ClearEndOfFrameComponentsSystem clear = new ClearEndOfFrameComponentsSystem();
            for (int i = 0; i < nbFrameToExtrapolate; i++)
            {
                clear.UpdateSystem();
                circleCollision.UpdateSystem();
                wallCollision.UpdateSystem();
                bounceBack.UpdateSystem();
                positionUpdate.UpdateSystemExtrapolation(ids);
            }
        }
    }
}

