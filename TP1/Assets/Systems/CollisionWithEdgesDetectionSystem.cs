using UnityEngine;

public class CollisionWithEdgesDetectionSystem : ISystem
{
    public string Name => "CollisionWithEdgesDetectionSystem";
    private readonly Camera camera;
    private readonly Vector2 LdCorner;
 
    private readonly Vector2 RuCorner;
    private readonly World world;


    public CollisionWithEdgesDetectionSystem()
    {
        world = World.Instance;
        camera = Camera.main;
        LdCorner = camera.ViewportToWorldPoint(new Vector3(0f, 0f, camera.nearClipPlane));
        RuCorner = camera.ViewportToWorldPoint(new Vector3(1f, 1f, camera.nearClipPlane));
    }

    public void UpdateSystem()
    {
        for (int i = 0; i < world.Positions.Count; i++)
        {

            Vector2 position = world.Positions[i].Position;
            float size = world.Sizes[i].Size/2;
            if (isCollidingWithEdges(position, size))
            {
                world.CollisionWithEdges[i].HasCollision = true;
            }
        }
    }

    private bool isCollidingWithEdges(Vector2 position, float size)
    {
        return IsCollidingWithLeftEdge(position, size)
            || IsCollidingWithRightEdge(position, size)
            || IscollidingWithUpperEdge(position, size)
            || IsCollidingWithLowerEdge(position, size);
          
    }

    private bool IscollidingWithUpperEdge(Vector2 position, float size)
    {
        if ((position.y + size) >= RuCorner.y)
            return true;
        else
            return false;
    }

    private bool IsCollidingWithLeftEdge(Vector2 position, float size)
    {
        if (position.x - size <= LdCorner.x)
            return true;
        else
            return false;
    }

    private bool IsCollidingWithRightEdge(Vector2 position, float size)
    {
        if ((position.x + size) >= RuCorner.x)
            return true;
        else 
            return false;
    }

    private bool IsCollidingWithLowerEdge(Vector2 position, float size)
    {
        if ((position.y - size) <= LdCorner.y)
            return true;
        else 
            return false;
    }
}
