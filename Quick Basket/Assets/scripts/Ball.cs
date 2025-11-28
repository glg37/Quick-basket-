using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Ball : MonoBehaviour
{
    [Header("Configurações de Lançamento")]
    public float forcaLancamento = 15f;
    [Range(0.5f, 5f)]
    public float fatorAltura = 2f;

    [Header("Áudio")]
    public AudioClip somLancamento;
    [Range(0f, 1f)] public float volumeSom = 0.3f;
    private AudioSource audioSource;

    private Rigidbody2D rb;
    private Transform cestaAtual;
    private ArenaManager arenaManager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        arenaManager = FindFirstObjectByType<ArenaManager>();

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = somLancamento;
        audioSource.volume = volumeSom;
    }

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            LancaBola(mousePos);

            if (somLancamento) audioSource.Play();
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

        float tempoVoo = Mathf.Sqrt((2 * alturaExtra) / g) +
                         Mathf.Sqrt((2 * alturaTotal) / g);

        if (tempoVoo < 0.1f) tempoVoo = 0.5f;

        Vector2 velocidade = new Vector2(
            deslocamento.x / tempoVoo,
            Mathf.Sqrt(2 * g * alturaExtra)
        );

        rb.AddForce(velocidade * forcaLancamento, ForceMode2D.Impulse);

        if (cestaAtual != null)
        {
            Collider2D ballCol = GetComponent<Collider2D>();
            Collider2D cestaCol = cestaAtual.GetComponent<Collider2D>();

            if (ballCol && cestaCol)
            {
                Physics2D.IgnoreCollision(ballCol, cestaCol, true);
                StartCoroutine(ResetCollision(ballCol, cestaCol, .5f));
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

    // ---------- Função chamada pelo ArenaManager ----------
    public void TeleportarPara(Vector3 pos)
    {
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;

        transform.position = pos;
    }
}
