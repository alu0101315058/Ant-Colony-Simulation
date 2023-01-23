using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalPheromoneNode : MonoBehaviour
{
    public int type = -1;
    public float lastUpdate = 0;
    public Renderer rend;

    public void UpdatePheromone(FinalAnt ant)
    {
        int index = ant.Home.states[ant.state].pheromoneDropped;
        transform.position = ant.transform.position;
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

    public float Potency() 
    {
        float time = Time.time - lastUpdate;
        float duration = PheromoneField.instance.pheromoneDuration[type];
        float potency = 1 - (time / duration);
        if (potency < 0) potency = 0;
        return potency;
    }
}
