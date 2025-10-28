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

    public void AtivarSpawner(bool ativo)
    {
        this.ativo = ativo;

        // Para qualquer coroutine antiga
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }

        // Destrói qualquer moeda atual
        if (moedaAtual != null)
        {
            Destroy(moedaAtual);
            moedaAtual = null;
        }

        // Se ativar o spawner, começa a rotina de spawn
        if (ativo)
            spawnCoroutine = StartCoroutine(SpawnLoop());
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
        // Define altura aleatória
        float alturaAleatoria = Random.Range(alturaMin, alturaMax);

        Camera cam = Camera.main;
        float zMoeda = 0f;

        // Calcula bordas da tela em coordenadas do mundo
        float xMin = cam.ViewportToWorldPoint(new Vector3(0, 0, zMoeda - cam.transform.position.z)).x - 2f;
        float xMax = cam.ViewportToWorldPoint(new Vector3(1, 0, zMoeda - cam.transform.position.z)).x + 2f;

        // Escolhe direção aleatória (esquerda -> direita ou direita -> esquerda)
        bool indoDireita = Random.value > 0.5f;

        // Define posição inicial fora da tela
        Vector3 spawnPos = new Vector3(
            indoDireita ? xMin : xMax,
            transform.position.y + alturaAleatoria,
            0f
        );

        // Cria moeda
        moedaAtual = Instantiate(moedaPrefab, spawnPos, Quaternion.identity);

        // Move ela totalmente de fora a fora
        StartCoroutine(MovimentoMoeda(moedaAtual, indoDireita ? xMax : xMin));
    }

    private IEnumerator MovimentoMoeda(GameObject moeda, float destinoX)
    {
        Vector3 startPos = moeda.transform.position;
        Vector3 endPos = new Vector3(destinoX, startPos.y, startPos.z);

        float distancia = Vector3.Distance(startPos, endPos);
        float t = 0f;

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
