using UnityEngine;
using System.Collections;

public class HardCoin : MonoBehaviour
{
    [Header("Configura��es de Movimento")]
    public float velocidade = 5f;

    [Tooltip("Amplitude do movimento ondulado (vertical)")]
    public float altura = 2f;

    [Tooltip("Altura m�nima e m�xima em que a moeda pode voar")]
    public float alturaMin = -1f;
    public float alturaMax = 3f;

    public float tempoVisivel = 3f;      // Tempo que ela fica voando
    public float tempoReaparecer = 10f;  // Tempo at� aparecer de novo

    [Header("Limites da tela")]
    public Transform pontoEsquerda;
    public Transform pontoDireita;

    private bool indoDireita = true;
    private bool ativa = false;
    private Vector3 posInicial;

    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private Collider2D col2d;

    // Nomes dos par�metros opcionais no Animator
    private const string PARAM_VOANDO = "Voando";
    private const string PARAM_COLETADA = "Coletada";

    void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        col2d = GetComponent<Collider2D>();
    }

    void Start()
    {
        if (spriteRenderer != null) spriteRenderer.enabled = false;
        if (col2d != null) col2d.enabled = false;

        StartCoroutine(GerenciarAparicoes());
    }

    void Update()
    {
        if (!ativa) return;

        float direcao = indoDireita ? 1f : -1f;
        transform.Translate(Vector2.right * direcao * velocidade * Time.deltaTime);

        // Movimento ondulado baseado na posInicial.y e altura vari�vel
        transform.position = new Vector3(
            transform.position.x,
            posInicial.y + Mathf.Sin(Time.time * 3f) * altura * 0.2f,
            transform.position.z
        );

        if ((indoDireita && transform.position.x > pontoDireita.position.x) ||
            (!indoDireita && transform.position.x < pontoEsquerda.position.x))
        {
            StartCoroutine(Desaparecer());
        }
    }

    private IEnumerator GerenciarAparicoes()
    {
        while (true)
        {
            yield return new WaitForSeconds(tempoReaparecer);

            // Escolhe dire��o e altura aleat�ria
            indoDireita = Random.value > 0.5f;
            float alturaY = Random.Range(alturaMin, alturaMax);

            // Define posi��o inicial
            transform.position = new Vector3(
                indoDireita ? pontoEsquerda.position.x : pontoDireita.position.x,
                alturaY,
                transform.position.z
            );

            posInicial = transform.position;

            // Espelha sprite de acordo com a dire��o
            Vector3 scale = transform.localScale;
            scale.x = indoDireita ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
            transform.localScale = scale;

            if (spriteRenderer != null) spriteRenderer.enabled = true;
            if (col2d != null) col2d.enabled = true;
            ativa = true;

            if (anim != null && AnimatorHasParameter(anim, PARAM_VOANDO))
                anim.SetBool(PARAM_VOANDO, true);

            yield return new WaitForSeconds(tempoVisivel);

            if (ativa)
                StartCoroutine(Desaparecer());
        }
    }

    private IEnumerator Desaparecer()
    {
        ativa = false;

        if (anim != null && AnimatorHasParameter(anim, PARAM_VOANDO))
            anim.SetBool(PARAM_VOANDO, false);

        if (spriteRenderer != null) spriteRenderer.enabled = false;
        if (col2d != null) col2d.enabled = false;

        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Ball")) return;

        Debug.Log(" Moeda coletada!");
        if (anim != null && AnimatorHasParameter(anim, PARAM_COLETADA))
            anim.SetTrigger(PARAM_COLETADA);

        Destroy(gameObject);
    }

    private bool AnimatorHasParameter(Animator a, string paramName)
    {
        if (a == null) return false;
        foreach (var p in a.parameters)
        {
            if (p.name == paramName) return true;
        }
        return false;
    }
}
