using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class BallMenu : MonoBehaviour
{
    [Header("Configurações de Lançamento")]
    public float forcaLancamento = 15f;
    [Range(0.5f, 5f)]
    public float fatorAltura = 2f;

    [Header("Áudio")]
    public AudioClip somLancamento;
    [Range(0f, 1f)]
    public float volumeSom = 0.3f;
    private AudioSource audioSource;

    private Rigidbody2D rb;
    private Transform cestaAtual;
    private ArenaManager arenaManager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        arenaManager = FindFirstObjectByType<ArenaManager>();

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = somLancamento;
        audioSource.volume = volumeSom;
    }

    void Update()
    {
        // 👉 Verifica se o clique foi em um botão/interface
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return; // Ignora o clique — não lança a bola

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            LancaBola(mousePos);

            if (somLancamento != null)
                audioSource.Play();
        }
    }

    void LancaBola(Vector2 alvo)
    {
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;

        Vector2 posAtual = transform.position;
        Vector2 deslocamento = alvo - posAtual;

        float g = Mathf.Abs(Physics2D.gravity.y);
        float alturaExtra = fatorAltura;
        float alturaTotal = Mathf.Max(deslocamento.y + alturaExtra, 0.1f);

        float tempoVoo = Mathf.Sqrt((2 * alturaExtra) / g) + Mathf.Sqrt((2 * alturaTotal) / g);
        if (tempoVoo < 0.5f) tempoVoo = 0.5f;

        Vector2 velocidade = new Vector2(
            deslocamento.x / tempoVoo,
            Mathf.Sqrt(2 * g * alturaExtra)
        );

        float velocidadeMax = 8f;
        if (velocidade.magnitude > velocidadeMax)
            velocidade = velocidade.normalized * velocidadeMax;

        if (arenaManager != null && arenaManager.ControlesInvertidos())
            velocidade.x = -velocidade.x;

        rb.AddForce(velocidade, ForceMode2D.Impulse);

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
