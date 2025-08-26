using UnityEngine;

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

    [Header("Configuração do Movimento")]
    [Tooltip("Índice da arena que terá cestas móveis. A primeira arena é 0.")]
    public int arenaComMovimento = 0;
    public float velocidadeMovimento = 2f;

    private GameObject cestaAtual;
    private bool ultimaCestaEsquerda = false;

    void Start()
    {
        SpawnNovaCesta();
    }

    public void SpawnNovaCesta()
    {
       
        if (cestaAtual != null)
            Destroy(cestaAtual);

    
        float xPos = ultimaCestaEsquerda ? xDireita : xEsquerda;
        ultimaCestaEsquerda = !ultimaCestaEsquerda;

       
        Transform arenaAtualTransform = arenaManager.GetArenaAtualTransform();
        float yBase = arenaAtualTransform.position.y;
        float yPos = yBase + Random.Range(alturaMinimaRelativa, alturaMaximaRelativa);

        
        cestaAtual = Instantiate(cestaPrefab, new Vector2(xPos, yPos), Quaternion.identity);
        cestaAtual.tag = "Basket";

        if (arenaManager.GetArenaAtualIndex() == arenaComMovimento)
        {
            BasketMover mover = cestaAtual.AddComponent<BasketMover>();
            mover.limiteEsquerda = xEsquerda;
            mover.limiteDireita = xDireita;
            mover.velocidade = velocidadeMovimento;
        }

        
        Ball ballScript = ball.GetComponent<Ball>();
        if (ballScript != null)
        {
            ballScript.SetCestaAlvo(cestaAtual.transform);
        }
    }
}
