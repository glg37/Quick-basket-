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

    [Header("Arena com lançamento invertido")]
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

        // Zera velocidade antes do lançamento
        rb.linearVelocity = Vector2.zero;

        // Calcula diferença entre bola e cesta
        Vector2 diferenca = cestaAtual.position - transform.position;

        float distanciaMinima = 2f;

        // Se estiver muito perto -> força uma distância mínima
        if (diferenca.magnitude < distanciaMinima)
        {
            diferenca = diferenca.normalized * distanciaMinima;
        }

        Vector2 direcao = diferenca.normalized;

        // Ajusta força com fator de altura
        Vector2 forca;
        if (arenaManager.GetArenaAtualIndex() == arenaInvertida)
            forca = new Vector2(direcao.x, direcao.y - fatorAltura) * forcaLancamento;
        else
            forca = new Vector2(direcao.x, direcao.y + fatorAltura) * forcaLancamento;

        rb.AddForce(forca, ForceMode2D.Impulse);

        // Ignora colisão com a cesta por um tempo
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
