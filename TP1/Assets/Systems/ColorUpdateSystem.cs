using UnityEngine;

public class ColorUpdateSystem : ISystem
{
    public string Name => "ColorUpdateSystem";
    private readonly World world;

    public ColorUpdateSystem()
    {
        world = World.Instance;
    }

    public void UpdateSystem()
    {
        if (world.IsFirstRun)
            FullScreenUpdate();
        else
            UpperHalfScreenUpdate();
    }

    private void FullScreenUpdate()
    {
        for (int i = 0; i < world.Colors.Count; i++)
        {
            UpdateColor(i);
        }
    }

    private void UpperHalfScreenUpdate()
    {
        for (int i = 0; i < world.Colors.Count; i++)
        {   
            if (world.EntityInUpperHalf(i))
                UpdateColor(i);
        }
    }

    private void UpdateColor(int id)
    {
        if (world.Static[id].IsStatic)
        {
            world.Colors[id].Color = Color.red;
        }
        else if (world.Colliders[id].HasCollider)
        {
            world.Colors[id].Color = Color.blue;
        }
        else
        {
            world.Colors[id].Color = Color.green;
        }
    }
}
