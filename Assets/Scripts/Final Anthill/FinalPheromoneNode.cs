using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalPheromoneNode : MonoBehaviour
{
    public int type = -1;
    public float lastUpdate = 0;
    public Vector3 position;
    public Renderer rend;

    public void UpdateFeromone(int index)
    {
        type = index;
        lastUpdate = Time.time;
        if (rend == null) rend = GetComponent<Renderer>();
        rend.material.SetFloat("_Duration", PheromoneField.instance.pheromoneDuration[index]);
        rend.material.SetColor("_DominantColor", PheromoneField.instance.pheromoneColors[index]);
        rend.material.SetFloat("_LastUpdated", lastUpdate);
    }

    public void Smell()
    {
        lastUpdate--;
        rend.material.SetFloat("_LastUpdated", lastUpdate);
    }
}
