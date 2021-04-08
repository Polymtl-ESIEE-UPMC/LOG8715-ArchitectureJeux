using UnityEngine;
using System.Collections.Generic;
public class InputPredictionSystem : ISystem
{
    public string Name => "InputPredictionSystem";

    public void UpdateSystem()
    {
        if (ECSManager.Instance.NetworkManager.isClient && ECSManager.Instance.Config.enableInputPrediction)
        {
            int framesToPredict = ((World.Instance.latency / 20)) * 2;

            if (framesToPredict == 0 || World.Instance.inputHistory.Count < framesToPredict)
                return;

            uint playerId = (uint)CustomNetworkManager.Singleton.LocalClientId;
            Vector2 historicPosition = World.Instance.positionHistory[playerId][World.Instance.positionHistory[playerId].Count - framesToPredict];

            if (ComponentsManager.Instance.TryGetComponent(new EntityComponent(playerId), out ReplicationMessage replication))
            {
                if (Mathf.Abs(replication.pos.magnitude - historicPosition.magnitude) > 0.10)
                {
                    List<uint> ids = new List<uint>();
                    ComponentsManager.Instance.ForEach((EntityComponent entity, ReplicationMessage msg) =>
                    {
                        ShapeComponent shape = ComponentsManager.Instance.GetComponent<ShapeComponent>(entity);
                        shape.pos = msg.pos;
                        ComponentsManager.Instance.SetComponent<ShapeComponent>(entity, shape);
                        ids.Add(entity.id);
                    });

                    InputManagerSystem inputManagerSystem = new InputManagerSystem();
                    CircleCollisionDetectionSystem circleCollision = new CircleCollisionDetectionSystem();
                    WallCollisionDetectionSystem wallCollision = new WallCollisionDetectionSystem();
                    BounceBackSystem bounceBack = new BounceBackSystem();
                    PositionUpdateSystem positionUpdate = new PositionUpdateSystem();
  
                    for (int i = World.Instance.inputHistory.Count - framesToPredict; i < World.Instance.inputHistory.Count; i++)
                    {
                        ComponentsManager.Instance.ClearComponents<CollisionEventComponent>();
                        circleCollision.UpdateSystem();
                        wallCollision.UpdateSystem();
                        bounceBack.UpdateSystem();
                        positionUpdate.UpdateSystemExtrapolation(ids);
                        inputManagerSystem.UpdateSystemPrediction(World.Instance.inputHistory[i], playerId);

                        //update historic for everyone
                        ComponentsManager.Instance.ForEach((EntityComponent entity, ReplicationMessage msg) =>
                        {
                            World.Instance.positionHistory[entity][i] = ComponentsManager.Instance.GetComponent<ShapeComponent>(entity).pos;
                        });
                    }
                }
                ComponentsManager.Instance.RemoveComponent<ReplicationMessage>(new EntityComponent(playerId));
            }

        }
    }
}
