using UnityEngine;

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
    public ArenaManager arenaManager; // refer�ncia ao ArenaManager

    private GameObject cestaAtual;
    private bool ultimaCestaEsquerda = false;

    void Start()
    {
        // Spawn inicial da cesta na arena atual
        SpawnNovaCesta();
    }

    public void SpawnNovaCesta()
    {
        // Destr�i a cesta anterior
        if (cestaAtual != null)
            Destroy(cestaAtual);

        // Alterna lado da nova cesta
        float xPos = ultimaCestaEsquerda ? xDireita : xEsquerda;
        ultimaCestaEsquerda = !ultimaCestaEsquerda;

        // Pega a posi��o da arena atual e define altura relativa
        Transform arenaAtual = arenaManager.GetArenaAtualTransform();
        float yBase = arenaAtual.position.y;
        float yPos = yBase + Random.Range(alturaMinimaRelativa, alturaMaximaRelativa);

        // Cria a nova cesta
        cestaAtual = Instantiate(cestaPrefab, new Vector2(xPos, yPos), Quaternion.identity);
        cestaAtual.tag = "Basket";

        // Define a nova cesta como alvo da bola
        ball.GetComponent<Ball>().SetCestaAlvo(cestaAtual.transform);
    }
}
