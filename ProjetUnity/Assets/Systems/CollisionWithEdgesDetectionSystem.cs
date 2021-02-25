using UnityEngine;

public class CollisionWithEdgesDetectionSystem : ISystem
{
    public string Name => "CollisionWithEdgesDetectionSystem";
    private readonly World world;


    public CollisionWithEdgesDetectionSystem()
    {
        world = World.Instance;
    }

    public void UpdateSystem()
    {
        if (world.IsFirstRun)
            FullScreenDetection();
        else
            UpperHalfScreenDetection();

    }

    private void FullScreenDetection()
    {
        for (int i = 0; i < world.Positions.Count; i++)
        {
            CheckCollisionWithEntity(i);
        }
    }

    private void UpperHalfScreenDetection()
    {
        for (int i = 0; i < world.Positions.Count; i++)
        {
            if (world.EntityInUpperHalf(i))
                CheckCollisionWithEntity(i);
        }
    }

    private void CheckCollisionWithEntity(int id)
    {
        Vector2 position = world.Positions[id].Position;
        float size = world.Sizes[id].Size / 2;
        if (isCollidingWithEdges(position, size))
        {
            world.CollisionWithEdges[id].HasCollision = true;
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
        if ((position.y + size) >= world.RuCorner.y)
            return true;
        else
            return false;
    }

    private bool IsCollidingWithLeftEdge(Vector2 position, float size)
    {
        if (position.x - size <= world.LdCorner.x)
            return true;
        else
            return false;
    }

    private bool IsCollidingWithRightEdge(Vector2 position, float size)
    {
        if ((position.x + size) >= world.RuCorner.x)
            return true;
        else 
            return false;
    }

    private bool IsCollidingWithLowerEdge(Vector2 position, float size)
    {
        if ((position.y - size) <= world.LdCorner.y)
            return true;
        else 
            return false;
    }
}
