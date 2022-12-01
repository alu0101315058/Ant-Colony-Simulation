using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Wandering")]
public class WanderingBehaviour : FlockBehaviour
{
    private Vector2 currentVelocity;
    public float wanderingRadius = 1;
    public float agentSmoothTime = 0.5f;
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        Vector2 randomDirection = Random.insideUnitCircle * wanderingRadius;
        Vector2 wanderingMove = Vector2.SmoothDamp(agent.transform.up, randomDirection, ref currentVelocity, agentSmoothTime);
        return wanderingMove;
    }
}
