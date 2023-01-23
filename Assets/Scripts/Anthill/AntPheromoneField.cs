using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntPheromoneField : MonoBehaviour
{
    static public AntPheromoneField instance { get; private set;}
    public LayerMask layerMask;
    public float resolution = 16;
    private Dictionary<Vector2Int, AntPheromoneNode> nodes = new Dictionary<Vector2Int, AntPheromoneNode>();

    void Start()
    {
        if (instance == null) instance = this;
        else Debug.LogError("FeromoneField already exists");
    }

    public List<Vector2Int> GetSensorPositions(Ant ant)
    {
        float facingAngle = Vector2.SignedAngle(Vector2.up, ant.transform.up);
        int sign = facingAngle > 0 ? 1 : -1;
        int angle = Mathf.FloorToInt((facingAngle + 22.5f * sign) / 45 * sign)*sign;
        List<Vector2Int> positions = new List<Vector2Int>();
        switch (angle) {
            case 0:
                positions.Add(new Vector2Int(1, 1));
                positions.Add(new Vector2Int(0, 1));
                positions.Add(new Vector2Int(-1, 1));
                break;
            case 1:
            case -1:
                positions.Add(new Vector2Int(-sign, 0));
                positions.Add(new Vector2Int(-sign, 1));
                positions.Add(new Vector2Int(0, 1));
                break;
            case 2:
            case -2:
                positions.Add(new Vector2Int(-sign, -1));
                positions.Add(new Vector2Int(-sign, 0));
                positions.Add(new Vector2Int(-sign, 1));
                break;
            case 3:
            case -3:
                positions.Add(new Vector2Int(0, -1));
                positions.Add(new Vector2Int(-sign, -1));
                positions.Add(new Vector2Int(-sign, 0));
                break;
            case 4:
            case -4:
                positions.Add(new Vector2Int(1, -1));
                positions.Add(new Vector2Int(0, -1));
                positions.Add(new Vector2Int(-1, -1));
                break;
        }
        return positions;
    }

    public List<AntPheromoneNode> GetPheromoneContext(Ant ant, int filterType)
    {
        List<AntPheromoneNode> context = new List<AntPheromoneNode>();
        List<Vector2Int> positions = GetSensorPositions(ant);
        foreach (Vector2Int pos in positions)
        {
            Vector2Int position = new Vector2Int(Mathf.RoundToInt(ant.transform.position.x / resolution) + pos.x, Mathf.RoundToInt(ant.transform.position.z / resolution) + pos.y);
            if (nodes.ContainsKey(position))
            {
                AntPheromoneNode node = nodes[position];
                if (node.type == filterType) context.Add(node);
            }
        }
        return context;
    }

    public AntPheromoneNode GetNode(Vector3 position)
    {
        Vector2Int pos = new Vector2Int(Mathf.RoundToInt(position.x / resolution), Mathf.RoundToInt(position.z / resolution));
        if (nodes.ContainsKey(pos)) return nodes[pos];
        AntPheromoneNode node = new AntPheromoneNode(pos);
        nodes.Add(pos, node);
        return node;
    }
}
