using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntMovement : MonoBehaviour
{
    public float speed = 2f;
    public float turnSpeed = 90f;
    public float turnInterest = 0f;
    
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
        transform.Rotate(Vector3.forward * turnInterest * turnSpeed * Time.deltaTime);
    }
}
