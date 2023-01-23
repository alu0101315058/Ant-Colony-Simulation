using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anthill : MonoBehaviour
{
    [System.Serializable]
    public struct AgentState
    {
        // Behaviour
        public int pheromoneDropped;
    }
    public Transform target;

    public List<AgentState> states;
    public Ant agentPrefab;
    private List<Ant> ants = new List<Ant>();

    [Range(1, 500)]
    public int startingCount = 250;

    [Range(0f, 1f)]
    public float turnSpeed = 1f;
    [Range(0f, 100f)]
    public float maxSpeed = 5f;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < startingCount; i++)
        {
            Ant newAnt = Instantiate(
                agentPrefab,
                (Vector2)transform.position + (Random.insideUnitCircle),
                Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)),
                transform
            );
            newAnt.name = "Ant " + i;
            newAnt.Initialize(this);
            ants.Add(newAnt);
        }
        ants[0].state = 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (Ant ant in ants)
        {
            // Perception
            // Movement
            Vector2 move = Smell(ant);
            //move = HandleMapCollision(ant, move);
            ant.Move(move);
        }
    }

    private Vector2 HandleMapCollision(Ant ant, Vector2 move)
    {
        RaycastHit2D hit = Physics2D.Raycast(ant.transform.position, move, 0.5f, (1 << 7));
        if (hit.collider != null)
        {
            move = Vector2.Reflect(move, hit.normal);
        }
        return move;
    }

    Vector2 Visuals(Ant ant)
    {
        // use three rays to check for obstacles
        // if obstacle, turn away from it
        RaycastHit2D hitRigth = Physics2D.Raycast(ant.transform.position, 3*ant.transform.up + ant.transform.right, 1f, LayerMask.GetMask("Obstacle"));
        Debug.DrawRay(ant.transform.position, 3 * ant.transform.up + ant.transform.right, Color.red);
        RaycastHit2D hitLeft = Physics2D.Raycast(ant.transform.position, 3*ant.transform.up - ant.transform.right, 1f, LayerMask.GetMask("Obstacle"));
        Debug.DrawRay(ant.transform.position, 3 * ant.transform.up - ant.transform.right, Color.red);
        Vector2 avoidanceMove = Vector2.zero;
        if (hitRigth.collider != null)
        {
            avoidanceMove += ((Vector2)ant.transform.position - (Vector2)hitRigth.point) / (hitRigth.distance == 0 ? .1f : hitRigth.distance);
        }
        if (hitLeft.collider != null)
        {
            avoidanceMove += ((Vector2)ant.transform.position - (Vector2)hitLeft.point) / (hitLeft.distance == 0 ? .1f : hitLeft.distance);
        }
        return avoidanceMove;
    }

    Vector2 Smell(Ant ant)
    {
        // Get Pheromones in three directions
        // ant.transform.up = 
        return target.position - ant.transform.position;
    }

    private void DropPheromone(Ant ant)
    {
        AntPheromoneField.instance.GetNode(ant.transform.position).UpdatePheromone(ant.Home.states[ant.state].pheromoneDropped);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Change state from carrying to searching
    }
}
