using UnityEngine;

public class NewAnt : MonoBehaviour
{
    public float maxSpeed = 2f;
    public float steerStrength = 2f;
    public float wanderStrength = 1f;
    public float viewDistance = 5f;
    public float fov = 90f;

    public Transform target;
    private Vector2 desiredDirection;
    private Vector2 velocity;

    void Update() {
        desiredDirection = (desiredDirection + Random.insideUnitCircle * wanderStrength).normalized;
        HandleFood();

        Vector2 desiredVelocity = desiredDirection * maxSpeed;
        Vector2 desiredSteeringForce = (desiredVelocity - velocity)* steerStrength;
        Vector2 acceleration = Vector2.ClampMagnitude(desiredSteeringForce, steerStrength) / 1;

        velocity = Vector2.ClampMagnitude(velocity + acceleration*Time.deltaTime, maxSpeed);
        transform.position += (Vector3)velocity * Time.deltaTime;

        float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void HandleFood() {
        if (target == null) {
            Collider2D[] allTargets = Physics2D.OverlapCircleAll(transform.position, viewDistance, LayerMask.GetMask("Food"));
            Debug.Log(allTargets.Length);
            if (allTargets.Length > 0) {
                Transform newTarget = allTargets[Random.Range(0, allTargets.Length)].transform;
                Vector2 direction = (newTarget.position - transform.position).normalized;

                if (Vector2.Angle(velocity, direction) < fov/2) {
                    newTarget.gameObject.layer = LayerMask.NameToLayer("Taken");
                    target = newTarget;
                }
            }
        } else {
            desiredDirection = (target.position - transform.position).normalized;

            const float distanceThreshold = 0.1f;
            if (Vector2.Distance(transform.position, target.position) < distanceThreshold) {
                Destroy(target.gameObject);
                target = null;
            }
        }
    }
}