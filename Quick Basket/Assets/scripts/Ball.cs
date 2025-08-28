using UnityEngine;

public class Ball : MonoBehaviour
{
    public float forcaLancamento = 15f;
    [Range(1f, 5f)]
    public float fatorAltura = 2f;

    private Rigidbody2D rb;
    private Transform cestaAtual;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
      
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

        
        if (diferenca.magnitude < 0.5f)
        {
            diferenca = diferenca.normalized * 0.5f;
        }

        Vector2 direcao = diferenca.normalized;
        Vector2 forca = new Vector2(direcao.x, direcao.y + fatorAltura) * forcaLancamento;

        rb.AddForce(forca, ForceMode2D.Impulse);
    }


    public void SetCestaAlvo(Transform novaCesta)
    {
        cestaAtual = novaCesta;
    }
}
