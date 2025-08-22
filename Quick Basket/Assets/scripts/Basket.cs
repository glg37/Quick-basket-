using UnityEngine;

public class Basket : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
           
            FindFirstObjectByType<BasketSpawner>().SpawnNovaCesta();
        }
    }
}
