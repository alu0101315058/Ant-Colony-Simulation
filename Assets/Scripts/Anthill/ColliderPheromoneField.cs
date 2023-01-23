using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderPheromoneField : MonoBehaviour
{
    static public ColliderPheromoneField instance { get; private set;}
    public LayerMask layerMask;
    public List<Color> colors = new List<Color>();
    private Dictionary<Vector3, ColliderPheromoneNode> nodes = new Dictionary<Vector3, ColliderPheromoneNode>();

    void Start()
    {
        if (instance == null) instance = this;
        else Debug.LogError("FeromoneField already exists");
    }

    public List<ColliderPheromoneNode> GetPheromoneContext(Ant ant, int filterType)
    {
        List<ColliderPheromoneNode> context = new List<ColliderPheromoneNode>();
        return context;
    }

    public ColliderPheromoneNode GetNode(Vector3 position)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, 0.1f, layerMask);
        if (colliders.Length > 0) {
            return nodes[colliders[0].offset];
        }
        if (nodes.ContainsKey(position)) return nodes[position];
        ColliderPheromoneNode node = new ColliderPheromoneNode(gameObject.AddComponent<CircleCollider2D>(), position);
        nodes.Add(position, node);
        return node;
    }
}
