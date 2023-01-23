using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderPheromoneNode
{
    public int type = -1;
    public float lastUpdate = 0;
    public Vector3 position;
    public Collider2D collider;
    public ColliderPheromoneNode(Collider2D col, Vector3 pos = default)
    {
        position = pos;
        collider = col;
        collider.offset = pos;
    }

    public void UpdatePheromone(int type)
    {
        this.type = type;
        lastUpdate = Time.time;
    }
}
