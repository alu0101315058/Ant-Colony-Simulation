using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AntBehaviour : ScriptableObject
{
    public abstract Vector2 CalculateMove(Ant agent, List<Transform> context, Anthill anthill);
}
