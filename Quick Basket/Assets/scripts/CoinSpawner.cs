using UnityEngine;
using System.Collections;

public class CoinSpawner : MonoBehaviour
{
    [Header("Configuração da Moeda")]
    public GameObject moedaPrefab;
    public float tempoInicial = 0.5f;
    public float intervaloSpawn = 5f;
    public float velocidade = 2f;
    public float alturaMin = 1f;
    public float alturaMax = 4f;

    private GameObject moedaAtual;
    private Coroutine spawnCoroutine;
    private bool ativo = false;

    // Ativa ou desativa o spawner
    public void AtivarSpawner(bool ativo)
    {
        this.ativo = ativo;

        // Para spawn antigo e destrói moeda atual
        if (!ativo)
        {
            if (spawnCoroutine != null)
            {
                StopCoroutine(spawnCoroutine);
                spawnCoroutine = null;
            }

            if (moedaAtual != null)
            {
                Destroy(moedaAtual);
                moedaAtual = null;
            }
        }
        else
        {
            if (spawnCoroutine == null)
                spawnCoroutine = StartCoroutine(SpawnLoop());
        }
    }

    private IEnumerator SpawnLoop()
    {
        yield return new WaitForSeconds(tempoInicial);

        while (ativo)
        {
            if (moedaAtual == null && moedaPrefab != null)
            {
                SpawnMoeda();
                yield return new WaitForSeconds(intervaloSpawn);
            }
            else
            {
                yield return null;
            }
        }
    }

    private void SpawnMoeda()
    {
        float alturaAleatoria = Random.Range(alturaMin, alturaMax);

        Camera cam = Camera.main;
        float zMoeda = 0f;
        float xMin = cam.ViewportToWorldPoint(new Vector3(0, 0, zMoeda - cam.transform.position.z)).x - 1f;
        float xMax = cam.ViewportToWorldPoint(new Vector3(1, 0, zMoeda - cam.transform.position.z)).x + 1f;

        bool indoDireita = Random.value > 0.5f;
        Vector3 startPos = new Vector3(indoDireita ? xMin : xMax, alturaAleatoria, zMoeda);
        Vector3 endPos = new Vector3(indoDireita ? xMax : xMin, alturaAleatoria, zMoeda);

        moedaAtual = Instantiate(moedaPrefab, startPos, Quaternion.identity);
        StartCoroutine(MovimentoMoeda(moedaAtual, startPos, endPos));
    }

    private IEnumerator MovimentoMoeda(GameObject moeda, Vector3 startPos, Vector3 endPos)
    {
        float t = 0f;
        float distancia = Vector3.Distance(startPos, endPos);

        while (moeda != null && t < 1f)
        {
            t += Time.deltaTime * velocidade / distancia;
            moeda.transform.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }

        if (moeda != null)
            Destroy(moeda);
    }
}
