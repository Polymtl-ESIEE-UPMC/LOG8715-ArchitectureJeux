using System.Collections.Generic;

public class RegisterSystems
{
    public static List<ISystem> GetListOfSystems()
    {
        // determine order of systems to add
        List<ISystem> toRegister = new List<ISystem>();

        toRegister.Add(new InitializationSystem());
        toRegister.Add(new TimeUpdateSystem());
        toRegister.Add(new SaveSystem());
        toRegister.Add(new RewindSystem());
        toRegister.Add(new CollisionWithEdgesDetectionSystem());
        toRegister.Add(new CollisionWithEntityDetectionSystem());
        toRegister.Add(new GeneralCollisionResolutionSystem());
        toRegister.Add(new CollisionWithEntityResolutionSystem());
        toRegister.Add(new CollisionWithEdgesResolutionSystem());
        toRegister.Add(new ColorUpdateSystem());
        toRegister.Add(new PositionUpdateSystem());
        toRegister.Add(new RenderSystem());

        return toRegister;
    }
}