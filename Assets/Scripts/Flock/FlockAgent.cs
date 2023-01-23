using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FlockAgent : MonoBehaviour
{
    public int state = 0;
    
    private Flock agentFlock;
    public Flock AgentFlock { get { return agentFlock; } }
    private Collider2D agentCollider;
    public Collider2D AgentCollider { get { return agentCollider; } }
    public ParticleSystem particles;

    public static float particleLifespan = 5f;
    public static int emmisionOverTime = 3;

    // Start is called before the first frame update
    void Start()
    {
        agentCollider = GetComponent<Collider2D>();
        particles.Play();
    }

    public void SetState(int newState, Color newColor)
    {
        state = newState;
        var main = particles.main;
        main.startColor = newColor;
    }

    public void Initialize(Flock flock)
    {
        agentFlock = flock;
    }

    public void Move(Vector2 velocity)
    {
        transform.up = velocity;
        transform.position += (Vector3)velocity * Time.deltaTime;
    }
}
