using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Seek Anthill")]
public class SeekAnthill : FlockBehaviour
{
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        Vector2 targetOffset = flock.transform.position - agent.transform.position;
        return targetOffset;
    }
}
