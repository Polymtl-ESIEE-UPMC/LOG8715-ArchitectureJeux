using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionUpdateSystem : ISystem {
    public string Name
    {
        get
        {
            return this.GetType().Name;
        }
    }

    public void UpdateSystem()
    {
        UpdateSystem(Time.deltaTime);
    }

    public void UpdateSystemExtrapolation(List<uint> ids)
    {
        UpdateSystemExtrapolation(ids, Time.deltaTime);
    }

    public void UpdateSystemExtrapolation(List<uint> ids, float deltaTime)
    {
        for (int i = 0; i < ids.Count; i++)
        {
            if (ComponentsManager.Instance.TryGetComponent(new EntityComponent(ids[i]), out ShapeComponent shape))
            {
                UpdateShape(ids[i], shape, deltaTime);
            }
        }
    }

    public void UpdateSystem(float deltaTime)
    {
        ComponentsManager.Instance.ForEach<ShapeComponent>((entityID, shapeComponent) => {
            UpdateShape(entityID, shapeComponent, deltaTime);
        });
    }

    private void UpdateShape(uint entityId, ShapeComponent shape, float deltaTime)
    {
        shape.pos = GetNewPosition(shape.pos, shape.speed, deltaTime);
        ComponentsManager.Instance.SetComponent<ShapeComponent>(entityId, shape);
    }

    public static Vector2 GetNewPosition(Vector2 position, Vector2 speed)
    {
        return GetNewPosition(position, speed, Time.deltaTime);
    }

    public static Vector2 GetNewPosition(Vector2 position, Vector2 speed, float deltaTime)
    {
        var newPosition = position + speed * deltaTime;
        return newPosition;
    }
}

