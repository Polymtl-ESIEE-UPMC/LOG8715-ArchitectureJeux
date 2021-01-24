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
        for (int i = 0; i < world.Colors.Count; i++)
        {
            if (world.Static[i].IsStatic)
            {
                world.Colors[i].Color = Color.red;
            } else if (world.ColliderComponents[i].HasCollider)
            {
                world.Colors[i].Color = Color.blue;
            } else
            {
                world.Colors[i].Color = Color.green;
            }
        }
    }
}
