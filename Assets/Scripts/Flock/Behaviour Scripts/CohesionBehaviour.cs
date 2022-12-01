using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Cohesion")]
public class CohesionBehaviour : FilteredFlockBehaviour
{
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        // if no neighbours, return no adjustment
        if (filteredContext.Count == 0)
            return Vector2.zero;

        // add all points together and average
        Vector2 cohesionMove = Vector2.zero;
        foreach (Transform item in filteredContext)
        {
            cohesionMove += (Vector2)item.position;
        }
        cohesionMove /= filteredContext.Count;

        // create offset from agent position
        cohesionMove -= (Vector2)agent.transform.position;
        return cohesionMove;
    }
}
