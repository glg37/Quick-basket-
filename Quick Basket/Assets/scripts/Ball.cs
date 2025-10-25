using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{
    [Header("Configurações de Lançamento")]
    public float forcaLancamento = 15f;
    [Range(0.5f, 5f)]
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
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            LancaBola(mousePos);
        }
    }

    void LancaBola(Vector2 alvo)
    {
        rb.linearVelocity = Vector2.zero; 

        Vector2 posAtual = transform.position;
        Vector2 deslocamento = alvo - posAtual;

        float g = Mathf.Abs(Physics2D.gravity.y);
        float alturaExtra = fatorAltura;

        
        float alturaTotal = deslocamento.y + alturaExtra;
        if (alturaTotal < 0.1f) alturaTotal = 0.1f;

     
        float tempoVoo = Mathf.Sqrt((2 * alturaExtra) / g) + Mathf.Sqrt((2 * alturaTotal) / g);
        if (tempoVoo <= 0.1f) tempoVoo = 0.5f;

      
        Vector2 velocidade = new Vector2(
            deslocamento.x / tempoVoo,
            Mathf.Sqrt(2 * g * alturaExtra)
        );

       
        if (arenaManager != null && arenaManager.GetArenaAtualIndex() == arenaInvertida)
        {
            velocidade.y = -velocidade.y;
        }

     
        rb.AddForce(velocidade * forcaLancamento, ForceMode2D.Impulse);


        if (cestaAtual != null)
        {
            Collider2D ballCollider = GetComponent<Collider2D>();
            Collider2D cestaCollider = cestaAtual.GetComponent<Collider2D>();
            if (ballCollider && cestaCollider)
            {
                Physics2D.IgnoreCollision(ballCollider, cestaCollider, true);
                StartCoroutine(ResetCollision(ballCollider, cestaCollider, 0.5f));
            }
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
