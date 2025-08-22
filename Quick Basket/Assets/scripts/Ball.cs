using UnityEngine;

public class Ball : MonoBehaviour
{
    public float forcaLancamento = 15f;
    [Range(1f, 5f)]
    public float fatorAltura = 2f; // controla a curvatura

    private Rigidbody2D rb;
    private Transform cestaAtual;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cestaAtual = GameObject.FindGameObjectWithTag("Basket").transform;
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

        // Zera a velocidade antes do lançamento
        rb.linearVelocity = Vector2.zero;

        Vector2 direcao = (cestaAtual.position - transform.position).normalized;

        // Adiciona o fatorAltura na direção vertical
        Vector2 forca = new Vector2(direcao.x, direcao.y + fatorAltura) * forcaLancamento;

        rb.AddForce(forca, ForceMode2D.Impulse);
    }

    public void SetCestaAlvo(Transform novaCesta)
    {
        cestaAtual = novaCesta;
    }
}
