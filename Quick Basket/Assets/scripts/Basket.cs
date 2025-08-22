using UnityEngine;

public class Basket : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            // avisa que acertou uma cesta
            FindFirstObjectByType<ArenaManager>().AcertouCesta();

            // spawna a pr�xima cesta
            FindFirstObjectByType<BasketSpawner>().SpawnNovaCesta();

            // opcional: destruir a cesta atual (pra n�o ficar acumulando)
            Destroy(gameObject);
        }
    }
}
