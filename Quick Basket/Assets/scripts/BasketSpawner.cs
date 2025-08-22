using UnityEngine;

public class BasketSpawner : MonoBehaviour
{
    [Header("Configura��es da Cesta")]
    public GameObject cestaPrefab;
    public Transform ball;

    [Header("Limites de Posi��o")]
    public float alturaMinima = 1f;
    public float alturaMaxima = 3f;
    public float xEsquerda = -3f;
    public float xDireita = 2f;

    private GameObject cestaAtual;
    private bool ultimaCestaEsquerda = false; // Guarda de onde veio a �ltima cesta

    void Start()
    {
        cestaAtual = GameObject.FindGameObjectWithTag("Basket");
        if (cestaAtual != null)
        {
            ball.GetComponent<Ball>().SetCestaAlvo(cestaAtual.transform);
            // Define lado inicial baseado na posi��o da cesta
            ultimaCestaEsquerda = cestaAtual.transform.position.x < 0;
        }
        else
        {
            SpawnNovaCesta();
        }
    }

    public void SpawnNovaCesta()
    {
        // Destr�i a cesta anterior
        if (cestaAtual != null)
        {
            Destroy(cestaAtual);
        }

        // Alterna lado da nova cesta
        float xPos = ultimaCestaEsquerda ? xDireita : xEsquerda;
        ultimaCestaEsquerda = !ultimaCestaEsquerda; // Atualiza para a pr�xima vez

        // Escolhe altura aleat�ria dentro dos limites
        float yPos = Random.Range(alturaMinima, alturaMaxima);

        // Cria a nova cesta
        cestaAtual = Instantiate(cestaPrefab, new Vector2(xPos, yPos), Quaternion.identity);
        cestaAtual.tag = "Basket";
        cestaAtual.SetActive(true);

        // Define a nova cesta como alvo da bola
        ball.GetComponent<Ball>().SetCestaAlvo(cestaAtual.transform);
    }
}
