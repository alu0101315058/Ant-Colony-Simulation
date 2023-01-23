using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Ant : MonoBehaviour
{
    public int state = 0;
    private Anthill home;
    public Anthill Home { get { return home; } }
    private Collider2D agentCollider;
    public Collider2D AgentCollider { get { return agentCollider; } }



    // Start is called before the first frame update
    void Start()
    {
        agentCollider = GetComponent<Collider2D>();
    }

    public void Initialize(Anthill anthill)
    {
        home = anthill;
    }

    public void Move(Vector2 velocity)
    {
        transform.up = velocity;
        transform.position += (Vector3)velocity * Time.deltaTime;
    }
}
