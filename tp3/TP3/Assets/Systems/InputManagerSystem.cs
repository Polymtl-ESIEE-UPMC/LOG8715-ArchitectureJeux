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
        if (ECSManager.Instance.NetworkManager.isClient)
        {
            UpdateClient(deltaTime);
        }
        else
        {
            UpdateServer(deltaTime);
        }
    }


    private void UpdateClient(float deltaTime)
    {
        uint playerId = (uint)CustomNetworkManager.Singleton.LocalClientId;
        if (ComponentsManager.Instance.TryGetComponent(new EntityComponent(playerId), out InputMessage input))
        {
            if (ComponentsManager.Instance.TryGetComponent(new EntityComponent(playerId), out ShapeComponent playerShape))
            {
                KeyCode keycode = (KeyCode) input.keycode[input.keycode.Count - 1];
                playerShape = updatePosition(keycode, playerShape, deltaTime);
                if (!provoqueCollision(playerId, playerShape))
                {
                    ComponentsManager.Instance.SetComponent<ShapeComponent>(playerId, playerShape);
                }
            }
        }
    }

    private void UpdateServer(float deltaTime)
    {
        ComponentsManager.Instance.ForEach<InputMessage>((entityId, input) => {
            if (!ComponentsManager.Instance.TryGetComponent(new EntityComponent(entityId), out InputMessageIdTracker msgTracker))
            {
                msgTracker = new InputMessageIdTracker(input.messageID);
            }
            int numberOfInputsToSimulate = input.messageID - msgTracker.currentMessageId + 1;

            if (!ECSManager.Instance.Config.enableInputPrediction)
                numberOfInputsToSimulate = 1;

            for (int i = numberOfInputsToSimulate; i > 0; i--)
            {
                KeyCode keyCode = (KeyCode)input.keycode[input.keycode.Count - i];
                ShapeComponent playerShape = ComponentsManager.Instance.GetComponent<ShapeComponent>(entityId);
                playerShape = updatePosition(keyCode, playerShape, deltaTime);

                if (!provoqueCollision(entityId, playerShape))
                {
                    ComponentsManager.Instance.SetComponent<ShapeComponent>(entityId, playerShape);
                }
            }
            ComponentsManager.Instance.SetComponent<InputMessageIdTracker>(entityId, new InputMessageIdTracker(input.messageID+1));

        });
    }

    public void UpdateSystemPrediction(KeyCode input, uint id)
    {
        UpdateSystemPrediction(input, id, Time.deltaTime);
    }

    public void UpdateSystemPrediction(KeyCode input, uint id, float deltaTime)
    {
        if (ComponentsManager.Instance.TryGetComponent(new EntityComponent(id), out ShapeComponent playerShape))
        {
            playerShape = updatePosition(input, playerShape, deltaTime);
            if (!provoqueCollision(id, playerShape))
            {
                ComponentsManager.Instance.SetComponent<ShapeComponent>(id, playerShape);
            }
        }
    }

    private ShapeComponent updatePosition(KeyCode input, ShapeComponent playerShape, float deltaTime)
    {
        if (input == KeyCode.A)
        {
            playerShape.pos = playerShape.pos + speed * deltaTime * Vector2.left;
        }
        else if (input == KeyCode.W)
        {
            playerShape.pos = playerShape.pos + speed * deltaTime * Vector2.up;
        }
        else if (input == KeyCode.D)
        {
            playerShape.pos = playerShape.pos + speed * deltaTime * Vector2.right;
        }
        else if (input == KeyCode.S)
        {
            playerShape.pos = playerShape.pos + speed * deltaTime * Vector2.down;
        }

        return playerShape;
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
