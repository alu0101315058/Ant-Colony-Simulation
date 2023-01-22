using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltPheromoneField : MonoBehaviour
{
    static public AltPheromoneField instance { get; private set;}
    public float[] pheromoneDuration;
    public LayerMask layerMask;
    private Dictionary<Vector2Int, AltPheromoneNode> nodes = new Dictionary<Vector2Int, AltPheromoneNode>();

    void Start()
    {
        if (instance == null) instance = this;
        else Debug.LogError("FeromoneField already exists");
    }

    public List<AltPheromoneNode> GetPheromoneContext(Vector3 position)
    {
        Vector2Int pos = new Vector2Int(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.z));
        List<AltPheromoneNode> context = new List<AltPheromoneNode>();
        for (int x = -1; x <= 1; x++)
        {
            for (int z = -1; z <= 1; z++)
            {
                Vector2Int nodePos = new Vector2Int(pos.x + x, pos.y + z);
                if (nodes.ContainsKey(nodePos)) context.Add(nodes[nodePos]);
            }
        }
        return context;
    }

    public AltPheromoneNode GetNode(Vector3 position)
    {
        Vector2Int pos = new Vector2Int(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.z));
        if (nodes.ContainsKey(pos)) return nodes[pos];
        AltPheromoneNode node = new AltPheromoneNode(pheromoneDuration.Length, pos);
        nodes.Add(pos, node);
        return node;
    }
}
