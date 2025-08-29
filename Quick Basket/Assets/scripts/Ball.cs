using UnityEngine;

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

       
        rb.linearVelocity = Vector2.zero;

       
        Vector2 diferenca = cestaAtual.position - transform.position;

        
        float distanciaMinima = 2f; 
        if (diferenca.magnitude < distanciaMinima)
        {
            diferenca = diferenca.normalized * distanciaMinima;
        }

        Vector2 direcao = diferenca.normalized;
        Vector2 forca;

        if (arenaManager.GetArenaAtualIndex() == arenaInvertida)
            forca = new Vector2(direcao.x, direcao.y - fatorAltura) * forcaLancamento;
        else
            forca = new Vector2(direcao.x, direcao.y + fatorAltura) * forcaLancamento;

        rb.AddForce(forca, ForceMode2D.Impulse);
    }


    public void SetCestaAlvo(Transform novaCesta)
    {
        cestaAtual = novaCesta;
    }
}
