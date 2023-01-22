using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltPheromoneNode
{
    public float[] feromoneTimes;
    public float lastUpdate = 0;
    public Vector2Int position;
    public AltPheromoneNode(int numFeromones = 0, Vector2Int pos = default)
    {
        position = pos;
        feromoneTimes = new float[numFeromones];
        for (int i = 0; i < numFeromones; i++)
        {
            feromoneTimes[i] = 0;
        }
    }

    public void UpdateFeromone(int index)
    {
        feromoneTimes[index] = Time.time;
        lastUpdate = Time.time;
    }

    public void Smell()
    {
        for (int i = 0; i < feromoneTimes.Length; i++)
        {
            feromoneTimes[i]--;
        }
        lastUpdate--;
    }
}
