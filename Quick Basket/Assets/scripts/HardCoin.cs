using UnityEngine;
using System.Collections;

public class HardCoin : MonoBehaviour
{
    [Header("Configurações de Movimento")]
    public float velocidade = 5f;
    [Tooltip("Amplitude do movimento ondulado (vertical)")]
    public float altura = 2f;
    [Tooltip("Altura mínima e máxima em que a moeda pode voar")]
    public float alturaMin = -1f;
    public float alturaMax = 3f;
    public float tempoVisivel = 3f;      // Tempo que ela fica voando
    public float tempoReaparecer = 10f;  // Tempo até aparecer de novo

    private bool indoDireita = true;
    private bool ativa = false;
    private Vector3 posInicial;

    private float limiteEsquerdo;
    private float limiteDireito;

    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private Collider2D col2d;

    // Nomes dos parâmetros opcionais no Animator
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
        // Calcula limites da tela com base na câmera principal
        Camera cam = Camera.main;
        float alturaCamera = 2f * cam.orthographicSize;
        float larguraCamera = alturaCamera * cam.aspect;
        limiteEsquerdo = cam.transform.position.x - larguraCamera / 2f;
        limiteDireito = cam.transform.position.x + larguraCamera / 2f;

        if (spriteRenderer != null) spriteRenderer.enabled = false;
        if (col2d != null) col2d.enabled = false;

        StartCoroutine(GerenciarAparicoes());
    }

    void Update()
    {
        if (!ativa) return;

        float direcao = indoDireita ? 1f : -1f;
        transform.Translate(Vector2.right * direcao * velocidade * Time.deltaTime);

        // Movimento ondulado baseado na posInicial.y e altura variável
        transform.position = new Vector3(
            transform.position.x,
            posInicial.y + Mathf.Sin(Time.time * 3f) * altura * 0.2f,
            transform.position.z
        );

        if ((indoDireita && transform.position.x > limiteDireito) ||
            (!indoDireita && transform.position.x < limiteEsquerdo))
        {
            StartCoroutine(Desaparecer());
        }
    }

    private IEnumerator GerenciarAparicoes()
    {
        while (true)
        {
            yield return new WaitForSeconds(tempoReaparecer);

            // Escolhe direção e altura aleatória
            indoDireita = Random.value > 0.5f;
            float alturaY = Random.Range(alturaMin, alturaMax);

            // Define posição inicial
            float posX = indoDireita ? limiteEsquerdo : limiteDireito;
            transform.position = new Vector3(posX, alturaY, transform.position.z);
            posInicial = transform.position;

            // Espelha sprite de acordo com a direção
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

        Debug.Log("Moeda coletada!");
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
