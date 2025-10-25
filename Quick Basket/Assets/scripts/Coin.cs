using UnityEngine;

public class Coin : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
           
            CoinManager.instance.AdicionarMoeda(1);

            
            Destroy(gameObject);
        }
    }
}