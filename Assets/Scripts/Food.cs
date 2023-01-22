using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {
  void OnTriggerEnter2D(Collider2D other) {
    Debug.Log("Food Triggered: " + other.gameObject.name);
    FlockAgent agent = other.GetComponent<FlockAgent>();
    if (agent != null) {
      agent.state = 1;
      gameObject.SetActive(false);
    }
  }
}