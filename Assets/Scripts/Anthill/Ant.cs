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

    public void GetFood()
    {
        state = 1;
        transform.up = -transform.up;
    }

    public void DropFood()
    {
        state = 0;
        transform.up = -transform.up;
    }

    // Start is called before the first frame update
    void Start()
    {
        agentCollider = GetComponent<Collider2D>();
    }

    public void Initialize(Anthill anthill)
    {
        home = anthill;
    }

    public void Move(Vector2 target)
    {
        float angle = Vector2.Angle(transform.up, target);
        // transform.up = Vector2.Lerp(transform.up, target, home.turnSpeed < angle ? home.turnSpeed : angle);
        float speed = home.maxSpeed * Mathf.Cos(Mathf.Deg2Rad*angle);
        transform.position += transform.up * Time.deltaTime * (speed > 0 ? speed : 0);
    }
}
