using System;
using UnityEngine;

public class RewindSystem : ISystem
{
    public string Name => "RewindSystem";
    private readonly float COOLDOWN = 2.0f;
    private readonly World world;
    private float nextRewindTime;

    public RewindSystem()
    {
        world = World.Instance;
        nextRewindTime = 0;
    }

    public void UpdateSystem()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextRewindTime)
        {
            Rewind(CalculateFrameToGoBackTo());
            ClearPastFrames();

            nextRewindTime = Time.time + COOLDOWN;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Rewind in cooldown for another" + (nextRewindTime - Time.time) + "seconds");
        }
    }

    private void Rewind(int frameToRewindTo)
    {
        for (int i = 0; i < world.Positions.Count; i++)
        {
            world.Positions[i].Position = world.PastPositions[i].PastPositions[frameToRewindTo];
            world.Speeds[i].Speed = world.PastSpeeds[i].PastSpeeds[frameToRewindTo];
            world.Colors[i].Color = world.PastColors[i].PastColors[frameToRewindTo];
            world.Sizes[i].Size = world.PastSizes[i].PastSizes[frameToRewindTo];
            world.Colliders[i].HasCollider = world.PastColliders[i].PastColliders[frameToRewindTo];
        }
    }

    private void ClearPastFrames()
    {
        for (int i = 0; i < world.Positions.Count; i++)
        {
            world.PastPositions[i].PastPositions.Clear();
            world.PastSpeeds[i].PastSpeeds.Clear();
            world.PastColors[i].PastColors.Clear();
            world.PastSizes[i].PastSizes.Clear();
            world.PastColliders[i].PastColliders.Clear();
        }
        world.frameTimes.Clear();
    }

    private int CalculateFrameToGoBackTo()
    {
        float frameTime = Time.time - COOLDOWN < 0 ? 0 : Time.time - COOLDOWN;
        int frameToRewindTo = 0;
        for (int i = 0; i < world.frameTimes.Count; i++)
        {
            if (world.frameTimes[i].Time >= frameTime)
            {
                if (i != 0)
                {
                    float timeDiff1 = Math.Abs(world.frameTimes[i].Time - frameTime);
                    float timeDiff2 = Math.Abs(world.frameTimes[i - 1].Time - frameTime);
                    frameToRewindTo = timeDiff1 < timeDiff2 ? i : i - 1;
                }
                break;
            }
        }
        return frameToRewindTo;
    }
}
