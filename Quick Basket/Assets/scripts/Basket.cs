using UnityEngine;

public class Basket : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            // avisa que acertou uma cesta
            FindFirstObjectByType<ArenaManager>().AcertouCesta();

            // spawna a próxima cesta
            FindFirstObjectByType<BasketSpawner>().SpawnNovaCesta();

            // opcional: destruir a cesta atual (pra não ficar acumulando)
            Destroy(gameObject);
        }
    }
}
