using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Seek Target")]
public class SeekTarget : FlockBehaviour
{
    public Transform target;

    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        Vector2 targetOffset = target.position - agent.transform.position;
        return targetOffset;
    }
}
