using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Pheromone")]
public class PheromoneAllureBehaviour : FilteredFlockBehaviour
{
    public float[] pheromoneAllure;
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        if (filteredContext.Count == 0)
            return Vector2.zero;
        Vector2 move = Vector2.zero;
        PheromoneNode smelled = null;
        foreach (Transform item in filteredContext)
        {
            PheromoneNode pheromoneNode = item.GetComponent<PheromoneNode>();
            if (pheromoneNode != null)
            {
                Vector2 maxMove = Vector2.zero;
                for (int i = 0; i < pheromoneNode.feromoneTimes.Length && i < pheromoneAllure.Length; i++)
                {
                    if (pheromoneNode.feromoneTimes[i] < 0) continue;
                    float time = Time.time - pheromoneNode.feromoneTimes[i];
                    float duration = PheromoneField.instance.pheromoneDuration[i];
                    float potency = 1 - (time / duration);
                    float strength = pheromoneAllure[i] * potency;
                    if (potency > 0)
                    {
                        maxMove += (Vector2)(agent.transform.position - item.position) * strength;
                    }
                }
                if (maxMove.magnitude > move.magnitude) {
                    move = maxMove;
                    smelled = pheromoneNode;
                }
            }
        }
        smelled?.Smell();
        move /= filteredContext.Count;

        return move;
    }
}
