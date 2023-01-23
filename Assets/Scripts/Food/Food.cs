using UnityEngine;

public class Food : MonoBehaviour
{
    public int minFoodValue = 50;
    public int maxFoodValue = 150;

    public int foodValue = 4;
    private float shrinkFactor;
    private ParticleSystem particles;

    void Start()
    {
        foodValue = Random.Range(minFoodValue, maxFoodValue);
        shrinkFactor = transform.localScale.magnitude / foodValue;
        particles = GetComponent<ParticleSystem>();
        var emission = particles.emission;
        emission.rateOverTime = foodValue;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        FlockAgent agent = other.GetComponent<FlockAgent>();
        if (agent != null)
        {
            agent.SetState(1, Color.red);
            TakeBite();
        }
        Ant ant = other.GetComponent<Ant>();
        if (ant != null && ant.state == 0)
        {
            ant.GetFood();
            TakeBite();
        }
    }


    [ContextMenu("TakeBite")]
    public void TakeBite()
    {
        foodValue -= 1;
        if (foodValue <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            var emission = particles.emission;
            emission.rateOverTime = foodValue;
        }
    }

}
