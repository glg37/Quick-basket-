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

           
            if (MessageManager.Instance != null)
                MessageManager.Instance.MostrarMensagem();

            Destroy(gameObject);
        }
    }

    private IEnumerator MostrarMensagem()
    {
        mensagemCesta.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        mensagemCesta.gameObject.SetActive(false);
    }
}
