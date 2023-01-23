using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntPheromoneNode
{
    public int type = -1;
    public float lastUpdate = 0;
    public Vector3 position;
    public AntPheromoneNode(Vector3 pos = default)
    {
        position = pos;
    }

    public void UpdatePheromone(int type)
    {
        this.type = type;
        lastUpdate = Time.time;
    }

    public void Smell()
    {
        lastUpdate--;
    }
}
