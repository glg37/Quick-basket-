using UnityEngine;
using System.Collections;

public class BasketSpawner : MonoBehaviour
{
    [Header("Configura��es da Cesta")]
    public GameObject cestaPrefab;
    public Transform ball;

    [Header("Limites de Posi��o dentro da Arena")]
    public float alturaMinimaRelativa = 0.5f;
    public float alturaMaximaRelativa = 2f;
    public float xEsquerda = -3f;
    public float xDireita = 3f;

    [Header("Arena Manager")]
    public ArenaManager arenaManager;

    [Header("Configura��o do Movimento da Cesta")]
    public int arenaComMovimento = 0;
    public float velocidadeMovimento = 2f;

    [Header("Obst�culo M�vel")]
    public GameObject obstaculoPrefab;
    public int arenaComObstaculo = 2;
    public float alturaDoObstaculo = 0.3f;
    public float faixaMovimentoObstaculo = 1.5f; // largura do movimento do obst�culo em torno da cesta

    private GameObject cestaAtual;
    private bool ultimaCestaEsquerda = false;

    void Start()
    {
        SpawnNovaCesta();
    }

    public void SpawnNovaCesta()
    {
        StartCoroutine(SpawnComDelay(0.4f));
    }

    private IEnumerator SpawnComDelay(float delay)
    {
        if (cestaAtual != null)
            Destroy(cestaAtual);

        yield return new WaitForSeconds(delay);

        // Alterna lado da cesta
        float xPos = ultimaCestaEsquerda ? xDireita : xEsquerda;
        ultimaCestaEsquerda = !ultimaCestaEsquerda;

        Transform arenaAtualTransform = arenaManager.GetArenaAtualTransform();
        float yBase = arenaAtualTransform.position.y;
        float yPos = yBase + Random.Range(alturaMinimaRelativa, alturaMaximaRelativa);

        // Evita spawn muito pr�ximo da bola
        float minDistanciaY = 1f;
        if (Mathf.Abs(yPos - ball.position.y) < minDistanciaY)
            yPos = ball.position.y + minDistanciaY;

        // Cria cesta
        cestaAtual = Instantiate(cestaPrefab, new Vector2(xPos, yPos), Quaternion.identity);
        cestaAtual.tag = "Basket";

        int arenaIndex = arenaManager.GetArenaAtualIndex();

        // Adiciona movimento � cesta, se necess�rio
        if (arenaIndex == arenaComMovimento)
        {
            BasketMover mover = cestaAtual.AddComponent<BasketMover>();
            mover.limiteEsquerda = xEsquerda;
            mover.limiteDireita = xDireita;
            mover.velocidade = velocidadeMovimento;
        }

        // Cria obst�culo como filho da cesta
        if (arenaIndex == arenaComObstaculo && obstaculoPrefab != null)
        {
            Vector3 posObstaculo = cestaAtual.transform.position + Vector3.up * alturaDoObstaculo;
            posObstaculo.x = cestaAtual.transform.position.x; // centraliza no X da cesta
            GameObject obstaculo = Instantiate(obstaculoPrefab, posObstaculo, Quaternion.identity);

            // Coloca obst�culo como filho da cesta
            obstaculo.transform.SetParent(cestaAtual.transform);

            ObstaculoMover moverObstaculo = obstaculo.GetComponent<ObstaculoMover>();
            if (moverObstaculo != null)
            {
                // Limites relativos � cesta, sim�tricos
                moverObstaculo.SetLimites(-faixaMovimentoObstaculo, faixaMovimentoObstaculo);
            }
        }

        // Define cesta alvo para a bola
        Ball ballScript = ball.GetComponent<Ball>();
        if (ballScript != null)
        {
            ballScript.SetCestaAlvo(cestaAtual.transform);
        }
    }
}
