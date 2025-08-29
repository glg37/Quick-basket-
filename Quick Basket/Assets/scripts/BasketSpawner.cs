using UnityEngine;
using System.Collections;

public class BasketSpawner : MonoBehaviour
{
    [Header("Configurações da Cesta")]
    public GameObject cestaPrefab;
    public Transform ball;

    [Header("Limites de Posição dentro da Arena")]
    public float alturaMinimaRelativa = 0.5f;
    public float alturaMaximaRelativa = 2f;
    public float xEsquerda = -3f;
    public float xDireita = 3f;

    [Header("Arena Manager")]
    public ArenaManager arenaManager;

    [Header("Configuração do Movimento da Cesta")]
    public int arenaComMovimento = 0;
    public float velocidadeMovimento = 2f;

    [Header("Obstáculo Móvel")]
    public GameObject obstaculoPrefab;
    public int arenaComObstaculo = 2;
    public float alturaDoObstaculo = 0.3f;

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

        float xPos = ultimaCestaEsquerda ? xDireita : xEsquerda;
        ultimaCestaEsquerda = !ultimaCestaEsquerda;

        Transform arenaAtualTransform = arenaManager.GetArenaAtualTransform();
        float yBase = arenaAtualTransform.position.y;
        float yPos = yBase + Random.Range(alturaMinimaRelativa, alturaMaximaRelativa);

       
        float minDistanciaY = 1f;
        if (Mathf.Abs(yPos - ball.position.y) < minDistanciaY)
            yPos = ball.position.y + minDistanciaY;

        cestaAtual = Instantiate(cestaPrefab, new Vector2(xPos, yPos), Quaternion.identity);
        cestaAtual.tag = "Basket";

        int arenaIndex = arenaManager.GetArenaAtualIndex();

       
        if (arenaIndex == arenaComMovimento)
        {
            BasketMover mover = cestaAtual.AddComponent<BasketMover>();
            mover.limiteEsquerda = xEsquerda;
            mover.limiteDireita = xDireita;
            mover.velocidade = velocidadeMovimento;
        }

       
        if (arenaIndex == arenaComObstaculo && obstaculoPrefab != null)
        {
            Vector3 posObstaculo = cestaAtual.transform.position + Vector3.up * alturaDoObstaculo;
            GameObject obstaculo = Instantiate(obstaculoPrefab, posObstaculo, Quaternion.identity);

            
            obstaculo.transform.SetParent(cestaAtual.transform);

            ObstaculoMover moverObstaculo = obstaculo.GetComponent<ObstaculoMover>();
            if (moverObstaculo != null)
            {
                moverObstaculo.limiteEsquerda = xEsquerda;
                moverObstaculo.limiteDireita = xDireita;
            }
        }

       
        Ball ballScript = ball.GetComponent<Ball>();
        if (ballScript != null)
        {
            ballScript.SetCestaAlvo(cestaAtual.transform);
        }
    }
}
