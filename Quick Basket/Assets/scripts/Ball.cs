using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{
    public float forcaLancamento = 15f;
    [Range(1f, 5f)]
    public float fatorAltura = 2f;

    private Rigidbody2D rb;
    private Transform cestaAtual;
    private ArenaManager arenaManager;

    [Header("Arena com lan�amento invertido")]
    public int arenaInvertida = 1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        arenaManager = FindFirstObjectByType<ArenaManager>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LancaBola();
        }
    }

    void LancaBola()
    {
        if (cestaAtual == null) return;

        // Zera velocidade antes do lan�amento
        rb.linearVelocity = Vector2.zero;

        // Calcula diferen�a entre bola e cesta
        Vector2 diferenca = cestaAtual.position - transform.position;

        float distanciaMinima = 2f;

        // Se estiver muito perto -> for�a uma dist�ncia m�nima
        if (diferenca.magnitude < distanciaMinima)
        {
            diferenca = diferenca.normalized * distanciaMinima;
        }

        Vector2 direcao = diferenca.normalized;

        // Ajusta for�a com fator de altura
        Vector2 forca;
        if (arenaManager.GetArenaAtualIndex() == arenaInvertida)
            forca = new Vector2(direcao.x, direcao.y - fatorAltura) * forcaLancamento;
        else
            forca = new Vector2(direcao.x, direcao.y + fatorAltura) * forcaLancamento;

        rb.AddForce(forca, ForceMode2D.Impulse);

        // Ignora colis�o com a cesta por um tempo
        Collider2D ballCollider = GetComponent<Collider2D>();
        Collider2D cestaCollider = cestaAtual.GetComponent<Collider2D>();
        if (ballCollider != null && cestaCollider != null)
        {
            Physics2D.IgnoreCollision(ballCollider, cestaCollider, true);
            StartCoroutine(ResetCollision(ballCollider, cestaCollider, 0.5f));
        }
    }

    private IEnumerator ResetCollision(Collider2D a, Collider2D b, float delay)
    {
        yield return new WaitForSeconds(delay);
        Physics2D.IgnoreCollision(a, b, false);
    }

    public void SetCestaAlvo(Transform novaCesta)
    {
        cestaAtual = novaCesta;
    }
}
