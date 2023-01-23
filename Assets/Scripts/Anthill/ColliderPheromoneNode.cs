using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderPheromoneNode
{
    public int type = -1;
    public float lastUpdate = 0;
    public Vector3 position;
    public CircleCollider2D collider;
    public ColliderPheromoneNode(CircleCollider2D col, Vector3 pos = default)
    {
        position = pos;
        collider = col;
        collider.isTrigger = true;
        collider.radius = 0.3f;
        collider.offset = pos;
    }

    public ColliderPheromoneNode Transfer(Vector3 position)
    {
        ColliderPheromoneNode node = new ColliderPheromoneNode(collider, position);
        collider = null;
        node.type = type;
        node.lastUpdate = lastUpdate;
        return node;
    }

    public void UpdatePosition(Vector3 pos)
    {
        position = pos;
        collider.offset = pos;
    }

    public void UpdatePheromone(int type)
    {
        this.type = type;
        lastUpdate = Time.time;
    }

    public float Potency() 
    {
        float time = Time.time - lastUpdate;
        float duration = ColliderPheromoneField.instance.durations[type];
        float potency = 1 - (time / duration);
        if (potency < 0) potency = 0;
        return potency;
    }
}
