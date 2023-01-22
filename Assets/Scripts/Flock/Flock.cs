using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    [System.Serializable]
    public struct AgentState
    {
        public FlockBehaviour behaviour;
        public int pheromoneDropped;
    }

    public List<AgentState> states;
    public FlockAgent agentPrefab;
    private List<FlockAgent> agents = new List<FlockAgent>();

    public Transform spawnPoint;

    [Range(10, 500)]
    public int startingCount = 250;
    const float AgentDensity = 0.08f;

    [Range(1f, 100f)]
    public float driveFactor = 10f;
    [Range(1f, 100f)]
    public float maxSpeed = 5f;
    [Range(1f, 10f)]
    public float neighbourRadius = 1.5f;
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    public bool dropPheromones = true;
    public float pheromoneDropRate = .5f;
    public int gatheredFood = 0;

    float lastDropped = 0;
    float squareMaxSpeed;
    float squareNeighbourRadius;
    float squareAvoidanceRadius;
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }

    // Start is called before the first frame update
    void Start()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighbourRadius = neighbourRadius * neighbourRadius;
        squareAvoidanceRadius = squareNeighbourRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        for (int i = 0; i < startingCount; i++)
        {
            FlockAgent newAgent = Instantiate(
                agentPrefab,
                (Vector2)spawnPoint.position + (Random.insideUnitCircle),
                //Random.insideUnitCircle * AgentDensity * startingCount
                Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)),
                transform
            );
            newAgent.name = "Agent " + i;
            newAgent.Initialize(this);
            agents.Add(newAgent);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bool pheromoneUpdate = dropPheromones && Time.time - lastDropped > pheromoneDropRate;
        if (pheromoneUpdate) lastDropped = Time.time;
        foreach (FlockAgent agent in agents)
        {
            List<Transform> context = GetNearbyObjects(agent);
            // agent.GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, context.Count / 6f);
            if (agent.state >= states.Count) continue;
            Vector2 move = states[agent.state].behaviour.CalculateMove(agent, context, this);
            move = HandleMapCollision(agent, move);
            move *= driveFactor;
            if (move.sqrMagnitude > squareMaxSpeed)
            {
                move = move.normalized * maxSpeed;
            }
            agent.Move(move);
            if (pheromoneUpdate) DropPheromone(agent);
        }
    }
        
    private Vector2 HandleMapCollision(FlockAgent agent, Vector2 move)
    {
        RaycastHit2D hit = Physics2D.Raycast(agent.transform.position, move, 0.5f, (1 << 7));
        if (hit.collider != null)
        {
            move = Vector2.Reflect(move, hit.normal);
        }
        return move;
    }

    List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, neighbourRadius);
        foreach (Collider2D c in contextColliders)
        {

            if (c != agent.AgentCollider)
            {
                context.Add(c.transform);
            }
        }
        return context;
    }
    private void DropPheromone(FlockAgent agent)
    {
        PheromoneField.instance.GetNode(agent.transform.position).UpdateFeromone(states[agent.state].pheromoneDropped);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Anthill Triggered: " + other.gameObject.name);
        FlockAgent agent = other.GetComponent<FlockAgent>();
        if (agent != null && agent.AgentFlock == this && agent.state == 1)
        {
            agent.state = 0;
            gatheredFood++;
        }
    }
}
