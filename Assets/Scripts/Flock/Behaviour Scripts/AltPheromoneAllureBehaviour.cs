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
        foreach (AltPheromoneNode item in pheromoneContext)
        {
            for (int i = 0; i < item.feromoneTimes.Length && i < pheromoneAllure.Length; i++)
            {
                float time = Time.time - item.feromoneTimes[i];
                float duration = PheromoneField.instance.pheromoneDuration[i];
                float potency = time / duration;
                float strength = pheromoneAllure[i] * potency;
                if (strength > 0)
                {
                    move += (Vector2)item.position * strength;
                }
            }
        }
        move /= pheromoneContext.Count;

        move -= (Vector2)agent.transform.position;
        return move;
    }
}
