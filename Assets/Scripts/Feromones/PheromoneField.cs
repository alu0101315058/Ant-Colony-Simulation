using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PheromoneField : MonoBehaviour
{
    static public PheromoneField instance { get; private set;}
    public GameObject nodePrefab;
    public float[] pheromoneDuration;
    public Color[] pheromoneColors;
    public float proximityUpdate = 0.1f;
    public LayerMask layerMask;
    [SerializeField] private int nodeCount = 0;
    private float maxFeromoneTime;
    private List<PheromoneNode> nodes = new List<PheromoneNode>();

    void Start()
    {
        maxFeromoneTime = 1;
        for (int i = 0; i < pheromoneDuration.Length; i++)
        {
            if (pheromoneDuration[i] > maxFeromoneTime) maxFeromoneTime = pheromoneDuration[i];
        }
        if (instance == null) instance = this;
        else Debug.LogError("FeromoneField already exists");
    }

    public PheromoneNode GetNode(Vector3 position)
    {
        Collider[] colliders = Physics.OverlapSphere(position, proximityUpdate, layerMask);
        PheromoneNode node = null;
        for (int i = 0; node == null && i < colliders.Length; i++)
        {
            node = colliders[i].GetComponent<PheromoneNode>();
        }
        for (int i = 0; node == null && i < nodes.Count; i++)
        {
            if (Time.time - nodes[i].lastUpdate > maxFeromoneTime) node = nodes[i];
        }
        if (node == null) node = CreateNode();
        node.transform.position = position;
        return node;
    }

    public PheromoneNode CreateNode()
    {
        GameObject nodeObject = Instantiate(nodePrefab, transform);
        PheromoneNode node = nodeObject.GetComponent<PheromoneNode>();
        node.feromoneTimes = new float[pheromoneDuration.Length];
        nodes.Add(node);
        nodeCount++;
        return node;
    }
}
