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
        uint nbStatic = 0;
        for (uint i = 0; i < ecs.Config.allShapesToSpawn.Count; i++)
        {
            Config.ShapeConfig shape = ecs.Config.allShapesToSpawn[(int)i];

            //if (isStatic(shape) && isModulo4(++nbStatic))
            {
                ecs.CreateShape(i, ecs.Config.allShapesToSpawn[(int)i]);
                ecs.UpdateShapePosition(i, shape.initialPos);
                world.Positions[i] = shape.initialPos;
                world.Sizes[i] = shape.size;
                world.Speeds[i] = shape.initialSpeed;
            }
        }
        ecs.Config.systemsEnabled[Name] = false;
    }
    
    private bool isStatic(Config.ShapeConfig shape)
    {
        return (shape.initialSpeed.x == 0 && shape.initialSpeed.y == 0);
    }

    private bool isModulo4(uint i)
    {
        return i % 4 == 0;
    }
}

