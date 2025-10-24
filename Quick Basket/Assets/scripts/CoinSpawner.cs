using UnityEngine;
using System.Collections;

public class CoinSpawner : MonoBehaviour
{
    [Header("Configura��o da Moeda")]
    public GameObject moedaPrefab;
    [Tooltip("Tempo entre um spawn e outro")]
    public float intervaloSpawn = 10f;
    [Tooltip("Velocidade da moeda se movendo horizontalmente")]
    public float velocidade = 2f;
    [Tooltip("Altura m�nima da moeda")]
    public float alturaMin = 1f;
    [Tooltip("Altura m�xima da moeda")]
    public float alturaMax = 4f;

    private bool podeSpawnar = true;

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            if (podeSpawnar && moedaPrefab != null)
            {
                // Gera altura aleat�ria
                float alturaAleatoria = Random.Range(alturaMin, alturaMax);

                // Calcula bordas da tela
                Camera cam = Camera.main;
                float zMoeda = 0f;
                float xMin = cam.ViewportToWorldPoint(new Vector3(0, 0, zMoeda - cam.transform.position.z)).x - 1f;
                float xMax = cam.ViewportToWorldPoint(new Vector3(1, 0, zMoeda - cam.transform.position.z)).x + 1f;

                // Define dire��o inicial aleat�ria
                bool indoDireita = Random.value > 0.5f;
                Vector3 startPos = new Vector3(indoDireita ? xMin : xMax, alturaAleatoria, zMoeda);
                Vector3 endPos = new Vector3(indoDireita ? xMax : xMin, alturaAleatoria, zMoeda);

                // Instancia a moeda
                GameObject moedaAtual = Instantiate(moedaPrefab, startPos, Quaternion.identity);
                moedaAtual.SetActive(true);

                // Move a moeda
                yield return StartCoroutine(MovimentoMoeda(moedaAtual, startPos, endPos));

                // Espera o intervalo antes do pr�ximo spawn
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
            t += Time.deltaTime * velocidade / distancia; // Normaliza velocidade
            moeda.transform.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }

        if (moeda != null)
            Destroy(moeda); // Destroi a moeda ao chegar no final
    }

    public void AtivarSpawner(bool ativo)
    {
        podeSpawnar = ativo;
    }
}
