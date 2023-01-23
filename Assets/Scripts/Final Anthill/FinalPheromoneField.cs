using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalPheromoneField : MonoBehaviour
{
    static public FinalPheromoneField instance { get; private set;}
    public FinalPheromoneNode nodePrefab;
    public float[] pheromoneDuration;
    public List<Color> colors = new List<Color>();
    public float proximityUpdate = 0.1f;
    public LayerMask layerMask;
    [SerializeField] private int nodeCount = 0;
    private List<FinalPheromoneNode> nodes = new List<FinalPheromoneNode>();

    void Start()
    {
        if (instance == null) instance = this;
        else Debug.LogError("FeromoneField already exists");
    }

    public List<FinalPheromoneNode> GetPheromoneContext(FinalAnt ant, int filterType)
    {
        List<FinalPheromoneNode> context = new List<FinalPheromoneNode>();
        Collider2D[] collidersUp = Physics2D.OverlapCircleAll(ant.transform.position + ant.transform.up, 1f, layerMask);
        foreach (Collider2D collider in collidersUp) {
            FinalPheromoneNode node = collider.GetComponent<FinalPheromoneNode>();
            if (node != null && node.type == filterType) context.Add(node);
        }
        return context;
    }

    public FinalPheromoneNode GetNode(Vector3 position)
    {
        Collider2D collider = Physics2D.OverlapCircle(position, .6f, layerMask);
        if (collider != null) {
        }
        FinalPheromoneNode node = Instantiate(nodePrefab, position, Quaternion.identity, transform);
        return node;
    }
}
