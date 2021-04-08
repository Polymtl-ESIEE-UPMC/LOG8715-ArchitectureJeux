using UnityEngine;
using System.Collections.Generic;
public class ExtrapolationSystem : ISystem
{
    public string Name => "ExtrapolationSystem";

    public void UpdateSystem()
    {
        if (ECSManager.Instance.NetworkManager.isClient && ECSManager.Instance.Config.enablDeadReckoning)
        {
            List<uint> ids = new List<uint>();
            ComponentsManager.Instance.ForEach<ReplicationMessage>((id, message) => {
                ids.Add(id);
            });

            if (ids.Count == 0)
                return;

            int nbFrameToExtrapolate = World.Instance.latency / 20;

            CircleCollisionDetectionSystem circleCollision = new CircleCollisionDetectionSystem();
            WallCollisionDetectionSystem wallCollision = new WallCollisionDetectionSystem();
            BounceBackSystem bounceBack = new BounceBackSystem();
            PositionUpdateSystem positionUpdate = new PositionUpdateSystem();
            for (int i = 0; i < nbFrameToExtrapolate; i++)
            {
                ComponentsManager.Instance.ClearComponents<CollisionEventComponent>();
                circleCollision.UpdateSystem();
                wallCollision.UpdateSystem();
                bounceBack.UpdateSystem();
                positionUpdate.UpdateSystemExtrapolation(ids);
            }
        }
    }
}

