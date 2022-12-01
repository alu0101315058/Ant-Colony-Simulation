using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Composite")]
public class CompositeBehaviour : FlockBehaviour
{
    [System.Serializable]
    public class BehaviourWeight
    {
        public FlockBehaviour behaviour;
        public float weight = 1f;
    }
    public BehaviourWeight[] behaviours;

    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {

        // set up move
        Vector2 move = Vector2.zero;

        // iterate through behaviours
        for (int i = 0; i < behaviours.Length; i++)
        {
            Vector2 partialMove = behaviours[i].behaviour.CalculateMove(agent, context, flock) * behaviours[i].weight;

            if (partialMove != Vector2.zero)
            {
                if (partialMove.sqrMagnitude > behaviours[i].weight * behaviours[i].weight)
                {
                    partialMove.Normalize();
                    partialMove *= behaviours[i].weight;
                }
                move += partialMove;
            }
        }

        return move;
    }
}
