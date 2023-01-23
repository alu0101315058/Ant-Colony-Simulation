using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PheromoneNode : MonoBehaviour
{
    public float[] feromoneTimes;
    public float lastUpdate = 0;
    private Renderer rend;

    public void UpdateFeromone(int index)
    {
        feromoneTimes[index] = Time.time;
        lastUpdate = Time.time;
        if (rend == null) rend = GetComponent<Renderer>();
        rend.material.SetFloat("_Duration", PheromoneField.instance.pheromoneDuration[index]);
        rend.material.SetColor("_DominantColor", PheromoneField.instance.pheromoneColors[index]);
        rend.material.SetFloat("_LastUpdated", lastUpdate);
    }

    public void Smell()
    {
        for (int i = 0; i < feromoneTimes.Length; i++)
        {
            feromoneTimes[i]--;
        }
        lastUpdate--;
        rend.material.SetFloat("_LastUpdated", lastUpdate);
    }
}
