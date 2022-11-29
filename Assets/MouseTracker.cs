using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTracker : MonoBehaviour
{
    private AntMovement antMovement;
    private Vector3 mousePosition;
    // Start is called before the first frame update
    void Start()
    {
        antMovement = GetComponent<AntMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        float angle = Vector3.Angle(transform.up, mousePosition - transform.position);
        int direction = Vector3.Cross(transform.up, mousePosition - transform.position).z < 0? -1:1;
        antMovement.turnInterest = direction * Mathf.Pow(angle / 180f, 0.5f);
    }
}
