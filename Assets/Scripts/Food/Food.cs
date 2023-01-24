using UnityEngine;

public class Food : MonoBehaviour
{
    public int minFoodValue = 50;
    public int maxFoodValue = 150;

    public int foodValue = 4;
    private ParticleSystem particles;

    void Start()
    {
        foodValue = Random.Range(minFoodValue, maxFoodValue);
        particles = GetComponent<ParticleSystem>();
        var emission = particles.emission;
        emission.rateOverTime = foodValue / 50;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Ant ant = other.GetComponent<Ant>();
        if (ant != null && ant.state == 0)
        {
            ant.GetFood();
            TakeBite();
        }
        FinalAnt finalAnt = other.GetComponent<FinalAnt>();
        if (finalAnt != null && finalAnt.state == 0)
        {
            finalAnt.GetFood();
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
            emission.rateOverTime = foodValue / 50;
        }
    }

}
