using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldAnt : MonoBehaviour
{
    [System.Serializable]
    public class Interest {
        
        public string tag;
        public int strength;
    }
    public float speed = 2f;
    public float turnSpeed = 90f;
    public float fov = 90f;
    public float viewDistance = 5f;
    [SerializeField] public List<Interest> interests = new List<Interest>();
    private PolygonCollider2D perceptionCollider;
    [Range(-1,1)] public float turnInterest = 0f;
    private Dictionary<string, int> interestMap = new Dictionary<string, int>();

    void Start() {
        perceptionCollider = GetComponent<PolygonCollider2D>();
        UpdateFOV();
        for (int i = 0; i < interests.Count; i++) {
            interestMap.Add(interests[i].tag, interests[i].strength);
        }
    }

    void UpdateFOV() {
        float angle1 = Mathf.Deg2Rad*fov/2;
        float angle2 = Mathf.Deg2Rad*fov/4;
        Vector2 proportions1 = viewDistance * new Vector2(Mathf.Sin(angle1), Mathf.Cos(angle1));
        Vector2 proportions2 = viewDistance *new Vector2(Mathf.Sin(angle2), Mathf.Cos(angle2));
        perceptionCollider.SetPath(0, new Vector2[]{
            new Vector2(0,0),
            proportions1,
            proportions2,
            new Vector2(0,viewDistance),
            new Vector2(-proportions2.x, proportions2.y),
            new Vector2(-proportions1.x, proportions1.y),
        });
    }

    void Update()
    {
        turnInterest = 0f;
        ContactPoint2D[] contacts = new ContactPoint2D[2];
        int totalInterest = 0;
        if (perceptionCollider.GetContacts(contacts) > 0) {
            HashSet<GameObject> objects = new HashSet<GameObject>();
            for (int i = 0; i < contacts.Length; i++) {
                if (contacts[i].collider != null && !objects.Contains(contacts[i].collider.gameObject)) {
                    objects.Add(contacts[i].collider.gameObject);
                    string otherTag = contacts[i].collider.tag;
                    if (interestMap.ContainsKey(otherTag)) {
                        float computed = ComputeInterest(contacts[i].point);
                        Debug.Log("Interest: " + computed);
                        turnInterest += interestMap[otherTag] * computed;
                        totalInterest += Mathf.Abs(interestMap[otherTag]);
                    }
                    
                    Debug.DrawLine(transform.position, contacts[i].point, Color.red);
                }
            }
        }
        if (totalInterest > 0) {
            turnInterest /= totalInterest;
        }
        if (turnInterest != 0f) {
            Debug.Log(turnInterest);
        }
        MouseTracker();
        transform.Translate(Vector3.up * speed * Time.deltaTime);
        transform.Rotate(Vector3.forward * turnInterest * turnSpeed * Time.deltaTime);
    }

    float ComputeInterest(Vector2 contact) {
        float angle = Vector3.Angle(transform.up, contact - (Vector2)transform.position);
        Debug.Log("Angle: " +angle);
        int direction = Vector3.Cross(transform.up, contact - (Vector2)transform.position).z < 0? -1:1;
        return direction * Mathf.Pow(angle / 180f, 0.5f);
    }

    void MouseTracker() {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        float angle = Vector3.Angle(transform.up, mousePosition - transform.position);
        int direction = Vector3.Cross(transform.up, mousePosition - transform.position).z < 0? -1:1;
        turnInterest = direction * Mathf.Pow(angle / 180f, 0.5f);
    }
}