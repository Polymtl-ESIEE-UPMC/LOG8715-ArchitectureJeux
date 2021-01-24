using System;
public class CollisionWithEntityDetectionSystem : ISystem
{
    public string Name => "CollisionWithEntityDetectionSystem";
    private readonly World world;
    public CollisionWithEntityDetectionSystem()
    {
        world = World.Instance;
    }

    public void UpdateSystem()
    {
        foreach (PositionComponent entity in world.Positions)
        {
            foreach (PositionComponent otherEntity in world.Positions)
            {
                if (entity == otherEntity || !world.ColliderComponents[(int)entity.id].HasCollider || !world.ColliderComponents[(int)otherEntity.id].HasCollider)
                    continue;

                float dx = entity.Position.x - otherEntity.Position.x;
                float dy = entity.Position.y - otherEntity.Position.y;
                float distance = (float) Math.Sqrt(dx * dx + dy * dy);

                float radius = world.Sizes[(int)entity.id].Size/2f;
                float otherRadius = world.Sizes[(int)otherEntity.id].Size/2f;
                if (distance < radius + otherRadius)
                {
                    world.CollisionsWithEntity[(int)entity.id].HasCollision = true;
                    world.CollisionsWithEntity[(int)otherEntity.id].HasCollision = true;
                }
            }
        }
    }
}
