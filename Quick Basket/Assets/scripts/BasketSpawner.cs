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

       
        Transform arenaAtual = arenaManager.GetArenaAtualTransform();
        float yBase = arenaAtual.position.y;
        float yPos = yBase + Random.Range(alturaMinimaRelativa, alturaMaximaRelativa);

      
        cestaAtual = Instantiate(cestaPrefab, new Vector2(xPos, yPos), Quaternion.identity);
        cestaAtual.tag = "Basket";

        
        ball.GetComponent<Ball>().SetCestaAlvo(cestaAtual.transform);
    }
}
