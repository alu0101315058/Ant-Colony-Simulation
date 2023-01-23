using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalPheromoneField : MonoBehaviour
{
    static public FinalPheromoneField instance { get; private set;}
    public LayerMask layerMask;
    public List<Color> colors = new List<Color>();
    private Dictionary<Vector3, FinalPheromoneNode> nodes = new Dictionary<Vector3, FinalPheromoneNode>();

    void Start()
    {
        if (instance == null) instance = this;
        else Debug.LogError("FeromoneField already exists");
    }

    public List<FinalPheromoneNode> GetPheromoneContext(FinalAnt ant, int filterType)
    {
        List<FinalPheromoneNode> context = new List<FinalPheromoneNode>();
        Collider2D[] collidersRight = Physics2D.OverlapCircleAll(ant.transform.position + (ant.transform.up + ant.transform.right).normalized, 0.37f, layerMask);
        Collider2D[] collidersLeft = Physics2D.OverlapCircleAll(ant.transform.position + (ant.transform.up - ant.transform.right).normalized, 0.37f, layerMask);
        Collider2D[] collidersUp = Physics2D.OverlapCircleAll(ant.transform.position + ant.transform.up.normalized, 0.37f, layerMask);
        HashSet<Vector3> positions = new HashSet<Vector3>();
        foreach (Collider2D collider in collidersRight) positions.Add(collider.offset);
        foreach (Collider2D collider in collidersLeft) positions.Add(collider.offset);
        foreach (Collider2D collider in collidersUp) positions.Add(collider.offset);
        foreach (Vector3 position in positions) {
            if (nodes.ContainsKey(position) && nodes[position].type == filterType) context.Add(nodes[position]);
        }
        return context;
    }

    public FinalPheromoneNode GetNode(Vector3 position)
    {
        Collider2D collider = Physics2D.OverlapCircle(position, .6f, layerMask);
        if (collider != null) {
            FinalPheromoneNode transferee = nodes[collider.offset].Transfer(position);
            nodes.Remove(collider.offset);
            nodes.Add(position, transferee);
            return nodes[position];
        }
        FinalPheromoneNode node = new FinalPheromoneNode(gameObject.AddComponent<CircleCollider2D>(), position);
        nodes.Add(position, node);
        return node;
    }
}
