using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class RegisterSystems
{
    public static List<ISystem> GetListOfSystems()
    {
        // determine order of systems to add
        List<ISystem> toRegister = new List<ISystem>();
        // Add your systems

        toRegister.Add(new InputPredictionSystem());
        toRegister.Add(new SpawnSystem());
        toRegister.Add(new LatencyCalculatorSystem());
        toRegister.Add(new WallCollisionDetectionSystem());
        toRegister.Add(new CircleCollisionDetectionSystem());
        toRegister.Add(new BounceBackSystem());
        toRegister.Add(new PositionUpdateSystem());


        toRegister.Add(new GetUserInputSystem());
        toRegister.Add(new InputManagerSystem());

        toRegister.Add(new ReplicationSystem());
        toRegister.Add(new MemorySystem());
        toRegister.Add(new ExtrapolationSystem());
        toRegister.Add(new NetworkMessageSystem());
        toRegister.Add(new ClearEndOfFrameComponentsSystem());
        toRegister.Add(new DisplayShapePositionSystem());

        return toRegister;

    }
}