using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/AltPheromone")]
public class AltPheromoneAllureBehaviour : FilteredFlockBehaviour
{
    public float[] pheromoneAllure;
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        List<AltPheromoneNode> pheromoneContext = AltPheromoneField.instance.GetPheromoneContext(agent.transform.position);
        if (pheromoneContext.Count == 0)
            return Vector2.zero;
        Vector2 move = Vector2.zero;
        AltPheromoneNode smelled = null;
        foreach (AltPheromoneNode item in pheromoneContext)
        {
            Vector2 maxMove = Vector2.zero;
            for (int i = 0; i < item.feromoneTimes.Length && i < pheromoneAllure.Length; i++)
            {
                if (item.feromoneTimes[i] == 0) continue;
                float time = Time.time - item.feromoneTimes[i];
                float duration = PheromoneField.instance.pheromoneDuration[i];
                float potency = 1 - (time / duration);
                float strength = pheromoneAllure[i] * potency;
                if (potency > 0)
                {
                    maxMove += ((Vector2)agent.transform.position - item.position) * strength;
                }
            }
            if (maxMove.magnitude > move.magnitude) {
                move = maxMove;
                smelled = item;
            }
        }
        smelled?.Smell();
        move /= pheromoneContext.Count;

        return move;
    }
}
