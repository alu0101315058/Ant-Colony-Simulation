using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PheromoneNode : MonoBehaviour
{
    public float[] feromoneTimes;
    public float lastUpdate = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UpdateFeromone(int index)
    {
        feromoneTimes[index] = Time.time;
        lastUpdate = Time.time;
    }

    public void UpdateColor()
    {
        
    }
}
