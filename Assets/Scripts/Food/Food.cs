using UnityEngine;

public class Food : MonoBehaviour
{
    public int minFoodValue = 50;
    public int maxFoodValue = 150;

    private int foodValue;
    private float shrinkFactor;

    void Start()
    {
        foodValue = Random.Range(minFoodValue, maxFoodValue);
        shrinkFactor = transform.localScale.magnitude / foodValue;
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
            transform.localScale = transform.localScale - shrinkFactor * Vector3.one;
        }
    }

}
