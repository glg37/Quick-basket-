using UnityEngine;
using System.Collections;

public class CoinSpawner : MonoBehaviour
{
    [Header("Configuração da Moeda")]
    public GameObject moedaPrefab;
    [Tooltip("Tempo entre uma moeda sumir e a próxima aparecer")]
    public float intervaloSpawn = 5f;
    [Tooltip("Velocidade da moeda se movendo horizontalmente")]
    public float velocidade = 2f;
    [Tooltip("Altura mínima da moeda")]
    public float alturaMin = 1f;
    [Tooltip("Altura máxima da moeda")]
    public float alturaMax = 4f;

    private GameObject moedaAtual;
    private bool podeSpawnar = true;

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            if (podeSpawnar && moedaAtual == null && moedaPrefab != null)
            {
                // Altura aleatória
                float alturaAleatoria = Random.Range(alturaMin, alturaMax);

                // Bordas da tela
                Camera cam = Camera.main;
                float zMoeda = 0f;
                float xMin = cam.ViewportToWorldPoint(new Vector3(0, 0, zMoeda - cam.transform.position.z)).x - 1f;
                float xMax = cam.ViewportToWorldPoint(new Vector3(1, 0, zMoeda - cam.transform.position.z)).x + 1f;

                // Direção aleatória
                bool indoDireita = Random.value > 0.5f;
                Vector3 startPos = new Vector3(indoDireita ? xMin : xMax, alturaAleatoria, zMoeda);
                Vector3 endPos = new Vector3(indoDireita ? xMax : xMin, alturaAleatoria, zMoeda);

                // Cria a moeda
                moedaAtual = Instantiate(moedaPrefab, startPos, Quaternion.identity);
                moedaAtual.SetActive(true);

                // Move a moeda de uma ponta à outra
                yield return StartCoroutine(MovimentoMoeda(moedaAtual, startPos, endPos));

                // Depois que a moeda some, espera intervalo antes de spawnar de novo
                moedaAtual = null;
                yield return new WaitForSeconds(intervaloSpawn);
            }
            else
            {
                yield return null;
            }
        }
    }

    IEnumerator MovimentoMoeda(GameObject moeda, Vector3 startPos, Vector3 endPos)
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
            Destroy(moeda); // Destrói a moeda ao chegar no final
    }

    public void AtivarSpawner(bool ativo)
    {
        podeSpawnar = ativo;

        if (!ativo && moedaAtual != null)
        {
            Destroy(moedaAtual);
            moedaAtual = null;
        }
    }
}
