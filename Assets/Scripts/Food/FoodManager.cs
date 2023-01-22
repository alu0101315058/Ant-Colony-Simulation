using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public List<GameObject> instantiatedFood;
    public GameObject foodPrefab;

    static public FoodManager instance;

    void Start()
    {
        if (instance == null)
        {
            instantiatedFood = new List<GameObject>();
            instance = this;
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                instantiatedFood.Add(Instantiate(foodPrefab, spawnPoints[i].position, spawnPoints[i].rotation, this.transform));
            }
        }

    }
}
