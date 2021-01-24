using System.Collections.Generic;

public class RegisterSystems
{
    public static List<ISystem> GetListOfSystems()
    {
        // determine order of systems to add
        List<ISystem> toRegister = new List<ISystem>();

        toRegister.Add(new InitializationSystem());
        toRegister.Add(new CollisionWithEdgesDetectionSystem());
        toRegister.Add(new CollisionWithEntityDetectionSystem());
        toRegister.Add(new PositionUpdateSystem());
        toRegister.Add(new RenderSystem());

        return toRegister;
    }
}