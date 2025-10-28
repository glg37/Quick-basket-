using UnityEngine;
using System.Collections;

public class BasketSpawnerMenu : MonoBehaviour
{
    [Header("Configurações da Cesta")]
    public GameObject cestaPrefab;
    public Transform ball;

    [Header("Limites de Posição")]
    public float alturaMinimaRelativa = 0.5f;
    public float alturaMaximaRelativa = 2f;
    public float xEsquerda = -3f;
    public float xDireita = 3f;

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

        float yPos = Random.Range(alturaMinimaRelativa, alturaMaximaRelativa);

        // Evita spawn muito próximo da bola
        float minDistanciaY = 1f;
        if (Mathf.Abs(yPos - ball.position.y) < minDistanciaY)
            yPos = ball.position.y + minDistanciaY;

        cestaAtual = Instantiate(cestaPrefab, new Vector2(xPos, yPos), Quaternion.identity);
        cestaAtual.tag = "Basket";

        Debug.Log("Nova cesta spawnada em: " + cestaAtual.transform.position);
    }
}
