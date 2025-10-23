using UnityEngine;
using System.Collections;

public class HardCoin : MonoBehaviour
{
    [Header("Configurações de Movimento")]
    public float velocidade = 5f;
    public float alturaMin = -1f;
    public float alturaMax = 3f;
    public float tempoVisivel = 3f;
    public float tempoReaparecer = 10f;

    private bool indoDireita = true;
    private bool ativa = false;

    private float limiteEsquerdo;
    private float limiteDireito;

    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private Collider2D col2d;

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
        // Calcula limites com base na câmera principal
        Camera cam = Camera.main;
        float alturaCamera = 2f * cam.orthographicSize;
        float larguraCamera = alturaCamera * cam.aspect;
        limiteEsquerdo = cam.transform.position.x - larguraCamera / 2f;
        limiteDireito = cam.transform.position.x + larguraCamera / 2f;

        spriteRenderer.enabled = false;
        col2d.enabled = false;
    }

    void Update()
    {
        if (!ativa) return;

        // Movimento horizontal reto
        float direcao = indoDireita ? 1f : -1f;
        transform.Translate(Vector2.right * direcao * velocidade * Time.deltaTime);

        // Se sair dos limites, desaparece
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

            // Define direção e altura aleatória
            indoDireita = Random.value > 0.5f;
            float alturaY = Random.Range(alturaMin, alturaMax);
            float posX = indoDireita ? limiteEsquerdo : limiteDireito;
            transform.position = new Vector3(posX, alturaY, transform.position.z);

            // Espelha sprite conforme direção
            Vector3 scale = transform.localScale;
            scale.x = indoDireita ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
            transform.localScale = scale;

            spriteRenderer.enabled = true;
            col2d.enabled = true;
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

        spriteRenderer.enabled = false;
        col2d.enabled = false;

        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Ball")) return;

        Debug.Log("Moeda coletada!");

        if (anim != null && AnimatorHasParameter(anim, PARAM_COLETADA))
            anim.SetTrigger(PARAM_COLETADA);

        // Desativa temporariamente
        StartCoroutine(Desaparecer());
    }

    private bool AnimatorHasParameter(Animator a, string paramName)
    {
        if (a == null) return false;
        foreach (var p in a.parameters)
            if (p.name == paramName) return true;
        return false;
    }

    //  Chamado pelo ArenaManager quando a arena atual ativa
    public void AtivarNaArena()
    {
        StopAllCoroutines();
        ativa = false;

        spriteRenderer.enabled = false;
        col2d.enabled = false;

        StartCoroutine(GerenciarAparicoes());
    }

    //  Chamado quando a arena deixa de ser a atual
    public void PararMoeda()
    {
        StopAllCoroutines();
        ativa = false;

        spriteRenderer.enabled = false;
        col2d.enabled = false;
    }
}
