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
            
            ComponentsManager.Instance.SetComponent<ShapeComponent>(entityId, playerShape);
        });


            
    }
}
