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

    public List<AgentState> states;
    public Ant agentPrefab;
    private List<Ant> ants = new List<Ant>();

    [Range(10, 500)]
    public int startingCount = 250;

    [Range(1f, 100f)]
    public float driveFactor = 10f;
    [Range(1f, 100f)]
    public float inertia = 5f;
    [Range(1f, 100f)]
    public float maxSpeed = 5f;

    public bool dropPheromones = true;
    public float pheromoneDropRate = .5f;
    public int gatheredFood = 0;

    float lastDropped = 0;
    float squareMaxSpeed;

    // Start is called before the first frame update
    void Start()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;

        for (int i = 0; i < startingCount; i++)
        {
            Ant newAnt = Instantiate(
                agentPrefab,
                (Vector2)transform.position + (Random.insideUnitCircle),
                //Random.insideUnitCircle * AgentDensity * startingCount
                Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)),
                transform
            );
            newAnt.name = "Agent " + i;
            newAnt.Initialize(this);
            ants.Add(newAnt);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bool pheromoneUpdate = dropPheromones && Time.time - lastDropped > pheromoneDropRate;
        if (pheromoneUpdate) lastDropped = Time.time;
        foreach (Ant ant in ants)
        {
            // Perception
            // Movement
            Vector2 move = 100 * Visuals(ant) + Smell(ant) * driveFactor + ((Vector2)ant.transform.up * inertia);
            if (move.sqrMagnitude > squareMaxSpeed)
            {
                move = move.normalized * maxSpeed;
            }
            ant.Move(move);
            if (pheromoneUpdate) DropPheromone(ant);
        }
        
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
        return ant.transform.up + ant.transform.right * Random.Range(-1f,1f);
    }

    private void DropPheromone(Ant ant)
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Change state from carrying to searching
    }
}
