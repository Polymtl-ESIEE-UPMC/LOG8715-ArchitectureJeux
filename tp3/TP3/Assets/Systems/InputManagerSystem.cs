using UnityEngine;
public class InputManagerSystem : ISystem
{
    public string Name => "InputManagerSystem";

    private const float speed = 2f;

    public void UpdateSystem()
    {
        UpdateSystem(Time.deltaTime);
    }
    public void UpdateSystem(float deltaTime)
    {
        ComponentsManager.Instance.ForEach<InputMessage>((entityId, input) => {
            ShapeComponent playerShape = ComponentsManager.Instance.GetComponent<ShapeComponent>(entityId);
            if (input.keycode == KeyCode.A)
            {
                playerShape.pos = playerShape.pos + speed * deltaTime * Vector2.left;
            }
            else if (input.keycode == KeyCode.W)
            {
                playerShape.pos = playerShape.pos + speed * deltaTime * Vector2.up;
            }
            else if (input.keycode == KeyCode.D)
            {
                playerShape.pos = playerShape.pos + speed * deltaTime * Vector2.right;
            }
            else if (input.keycode == KeyCode.S)
            {
                playerShape.pos = playerShape.pos + speed * deltaTime * Vector2.down;
            }

            if (!provoqueCollision(entityId, playerShape))
                ComponentsManager.Instance.SetComponent<ShapeComponent>(entityId, playerShape);

        });
    }

    private bool provoqueCollision(uint playerId, ShapeComponent playerShape)
    {   
        bool provoqueColl = false;
        ComponentsManager.Instance.ForEach<ShapeComponent>((entityId, shape) => {
            var pos1 = shape.pos;
            var radius1 = shape.size / 2;
            var pos2 = playerShape.pos;
            var radius2 = playerShape.size / 2;

            if (entityId == playerId)
            {
                //early return, no need to check self
                return;
            }

            if (Vector3.Distance(pos1, pos2) <= radius1 + radius2)
            {
                provoqueColl = true;
            }
        });
        return provoqueColl;
    }
}
