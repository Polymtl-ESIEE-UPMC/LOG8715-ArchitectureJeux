using UnityEngine;

public class InitializationSystem : ISystem
{
    public string Name => "InitializationSystem";
    private ECSManager ecs;
    private World world;
    public InitializationSystem()
    {
        ecs  = ECSManager.Instance;
        world = World.Instance;
    }

    public void UpdateSystem()
    {
        if (world.Positions.Count > 0)
        {
            return;
        }

        for (uint i = 0; i < ecs.Config.allShapesToSpawn.Count; i++)
        {
            Config.ShapeConfig shape = ecs.Config.allShapesToSpawn[(int)i];

            ecs.CreateShape(i, ecs.Config.allShapesToSpawn[(int)i]);
            ecs.UpdateShapePosition(i, shape.initialPos);
            world.Positions.Add(new PositionComponent(i, shape.initialPos));
            world.Sizes.Add(new SizeComponent(i, shape.size));
            world.Speeds.Add(new SpeedComponent(i, shape.initialSpeed));
            world.Static.Add(isModulo4(i+1) ? new StaticComponent(i, true) : new StaticComponent(i, false));
            world.CollisionWithEdges.Add(new CollisionWithEdgesComponent(i, false));
            world.CollisionsWithEntity.Add(new CollisionWithEntityComponent(i, false));
            world.ColliderComponents.Add(new ColliderComponent(i, true));
            world.Colors.Add(new ColorComponent(i, Color.white));
        }
    }
    
    private bool isModulo4(uint i)
    {
        return i % 4 == 0;
    }
}

