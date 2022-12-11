using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Ant : MonoBehaviour
{
    private int state;
    
    private Anthill anthill;
    public Anthill Anthill { get { return anthill; } }
    private Collider2D antCollider;
    public Collider2D AntCollider { get { return antCollider; } }

    // Start is called before the first frame update
    void Start()
    {
        antCollider = GetComponent<Collider2D>();
    }

    public void Initialize(Anthill anthill)
    {
        this.anthill = anthill;
    }

    public void Move(Vector2 velocity)
    {
        transform.up = velocity;
        transform.position += (Vector3)velocity * Time.deltaTime;
    }
}
