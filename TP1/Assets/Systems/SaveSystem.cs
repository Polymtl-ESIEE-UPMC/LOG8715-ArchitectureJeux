using UnityEngine;

public class SaveSystem : ISystem
{
    public string Name => "SaveSystem";
    private readonly World world;

    public SaveSystem()
    {
        world = World.Instance;
    }

    public void UpdateSystem()
    {
        for (int i = 0; i < world.Positions.Count; i++)
        {
            world.PastPositions[i].PastPositions.Add(world.Positions[i].Position);
            world.PastSpeeds[i].PastSpeeds.Add(world.Speeds[i].Speed);
            world.PastColors[i].PastColors.Add(world.Colors[i].Color);
            world.PastSizes[i].PastSizes.Add(world.Sizes[i].Size);
            world.PastColliders[i].PastColliders.Add(world.Colliders[i].HasCollider);
        }
    }
}

