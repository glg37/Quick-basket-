using UnityEngine;

public class Coin : MonoBehaviour
{
    [Header("Som de Coleta")]
    public AudioClip somColeta;              
    [Range(0f, 1f)] public float volumeSom = 0.3f; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
       
            CoinManager.instance.AdicionarMoeda(1);

            
            if (somColeta != null)
            {
                AudioSource.PlayClipAtPoint(somColeta, transform.position, volumeSom);
            }

            
            Destroy(gameObject);
        }
    }
}
