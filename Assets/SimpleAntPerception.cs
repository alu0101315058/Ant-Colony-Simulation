using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAntPerception : MonoBehaviour
{
    private AntMovement antMovement;
    private Collider2D perceptionCollider;

    void Start() {
        antMovement = GetComponent<AntMovement>();
        perceptionCollider = GetComponent<Collider2D>();
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        Debug.DrawLine(transform.position, collision.contacts[0].point, Color.red, 1f);
        Vector2 direction = collision.contacts[0].point - (Vector2)transform.position;
    }
}
