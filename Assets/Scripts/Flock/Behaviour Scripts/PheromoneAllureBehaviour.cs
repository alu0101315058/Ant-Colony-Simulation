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
        foreach (Transform item in filteredContext)
        {
            PheromoneNode pheromoneNode = item.GetComponent<PheromoneNode>();
            if (pheromoneNode != null)
            {
                for (int i = 0; i < pheromoneNode.feromoneTimes.Length && i < pheromoneAllure.Length; i++)
                {
                    float time = Time.time - pheromoneNode.feromoneTimes[i];
                    float duration = PheromoneField.instance.pheromoneDuration[i];
                    float potency = time / duration;
                    float strength = pheromoneAllure[i] * potency;
                    if (strength > 0)
                    {
                        move += (Vector2)item.position * strength;
                    }
                }
            }
        }
        move /= filteredContext.Count;

        move -= (Vector2)agent.transform.position;
        return move;
    }
}
