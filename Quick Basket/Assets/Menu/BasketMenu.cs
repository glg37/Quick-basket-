using UnityEngine;
using TMPro;
using System.Collections;

public class BasketMenu : MonoBehaviour
{
    [HideInInspector]
    public TMP_Text mensagemCesta;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            BasketSpawnerMenu spawner = FindFirstObjectByType<BasketSpawnerMenu>();
            if (spawner != null) spawner.SpawnNovaCesta();



            Destroy(gameObject);
        }
    }

}
