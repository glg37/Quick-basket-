using UnityEngine;
using UnityEngine.EventSystems; 

public class BallMenu : MonoBehaviour
{
    public float forcaLancamento = 15f;
    [Range(1f, 5f)]
    public float fatorAltura = 2f;

    private Rigidbody2D rb;
    private Transform cestaAtual;

    [Header("Lan�amento invertido (simula��o)")]
    public bool lancamentoInvertido = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
           
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                LancaBola();
            }
        }
    }

    void LancaBola()
    {
        if (cestaAtual == null) return;

        rb.linearVelocity = Vector2.zero;

        Vector2 diferenca = cestaAtual.position - transform.position;
        float distanciaMinima = 2f;
        if (diferenca.magnitude < distanciaMinima)
            diferenca = diferenca.normalized * distanciaMinima;

        Vector2 direcao = diferenca.normalized;
        Vector2 forca;

        if (lancamentoInvertido)
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
