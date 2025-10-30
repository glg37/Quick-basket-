using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class AnuncioCoin : MonoBehaviour
{
    [Header("Refer�ncias UI")]
    public GameObject painelAnuncio;
    public Image imagemAnuncio;
    public Button botaoAnuncio;
    public TMP_Text contadorTexto;
    public int moedasPorAnuncio = 10;

    [Header("Configura��es")]
    public float duracaoAnuncio = 5f;
    public int maxUsos = 2;

    [Header("Cores do bot�o X")]
    public Color corTextoBloqueado = Color.gray;
    public Color corTextoAtivo = Color.white;

    private Button botaoFechar;
    private TMP_Text textoFechar;
    private int usos = 0;
    private bool emAnuncio = false;

    void Start()
    {
        if (painelAnuncio == null)
        {
            Debug.LogError("Campo 'Painel Anuncio' n�o est� atribu�do!");
            return;
        }

        if (imagemAnuncio == null)
        {
            Debug.LogError("Campo 'Imagem Anuncio' n�o est� atribu�do!");
            return;
        }

        if (botaoAnuncio == null)
        {
            Debug.LogError("Campo 'Botao Anuncio' n�o est� atribu�do!");
            return;
        }

        painelAnuncio.SetActive(false);

        if (contadorTexto == null)
        {
            GameObject contadorGO = new GameObject("ContadorTexto", typeof(RectTransform));
            contadorGO.transform.SetParent(painelAnuncio.transform, false);
            contadorTexto = contadorGO.AddComponent<TextMeshProUGUI>();
            contadorTexto.alignment = TextAlignmentOptions.TopRight;
            contadorTexto.fontSize = 28;
            contadorTexto.color = Color.white;

            RectTransform rt = contadorGO.GetComponent<RectTransform>();
            rt.anchorMin = new Vector2(1, 1);
            rt.anchorMax = new Vector2(1, 1);
            rt.pivot = new Vector2(1, 1);
            rt.anchoredPosition = new Vector2(-20, -20);
        }

        contadorTexto.text = "";

        botaoFechar = painelAnuncio.GetComponentInChildren<Button>();

        if (botaoFechar == null)
        {
            GameObject botaoGO = new GameObject("BotaoX", typeof(RectTransform));
            botaoGO.transform.SetParent(painelAnuncio.transform, false);
            botaoFechar = botaoGO.AddComponent<Button>();

            Image img = botaoGO.AddComponent<Image>();
            img.color = new Color(1, 0.3f, 0.3f);

            RectTransform rt = botaoGO.GetComponent<RectTransform>();
            rt.anchorMin = new Vector2(1, 1);
            rt.anchorMax = new Vector2(1, 1);
            rt.pivot = new Vector2(1, 1);
            rt.anchoredPosition = new Vector2(-25, -25);
            rt.sizeDelta = new Vector2(45, 45);

            GameObject txtGO = new GameObject("TextoX", typeof(RectTransform));
            txtGO.transform.SetParent(botaoGO.transform, false);
            textoFechar = txtGO.AddComponent<TextMeshProUGUI>();
            textoFechar.text = "X";
            textoFechar.alignment = TextAlignmentOptions.Center;
            textoFechar.fontSize = 36;
            textoFechar.color = corTextoBloqueado;

            RectTransform txtRT = txtGO.GetComponent<RectTransform>();
            txtRT.anchorMin = Vector2.zero;
            txtRT.anchorMax = Vector2.one;
            txtRT.offsetMin = Vector2.zero;
            txtRT.offsetMax = Vector2.zero;
        }
        else
        {
            textoFechar = botaoFechar.GetComponentInChildren<TextMeshProUGUI>();

            if (textoFechar == null)
            {
                GameObject txtGO = new GameObject("TextoX");
                txtGO.transform.SetParent(botaoFechar.transform, false);
                textoFechar = txtGO.AddComponent<TextMeshProUGUI>();
                textoFechar.text = "X";
                textoFechar.alignment = TextAlignmentOptions.Center;
                textoFechar.fontSize = 36;
            }
        }

        botaoFechar.interactable = false;
        textoFechar.color = corTextoBloqueado;

        botaoAnuncio.onClick.AddListener(AssistirAnuncio);
        botaoFechar.onClick.AddListener(FecharAnuncio);
    }

    void AssistirAnuncio()
    {
        if (emAnuncio || usos >= maxUsos) return;
        StartCoroutine(AnuncioCoroutine());
    }

    IEnumerator AnuncioCoroutine()
    {
        emAnuncio = true;
        usos++;

        painelAnuncio.SetActive(true);
        imagemAnuncio.gameObject.SetActive(true);
        botaoFechar.interactable = false;
        textoFechar.color = corTextoBloqueado;
        contadorTexto.gameObject.SetActive(true);

        float tempoRestante = duracaoAnuncio;

        while (tempoRestante > 0)
        {
            contadorTexto.text = $"Fechar em {Mathf.Ceil(tempoRestante)}s";
            yield return new WaitForSeconds(1f);
            tempoRestante -= 1f;
        }

        contadorTexto.text = "Clique no X para fechar!";
        botaoFechar.interactable = true;
        textoFechar.color = corTextoAtivo;
    }

    void FecharAnuncio()
    {
        if (!emAnuncio || !botaoFechar.interactable) return;

        //  CORRE��O: moedas s� v�o para o menu
        if (CoinManager.instance != null)
        {
            CoinManager.instance.AdicionarMoedaPorAnuncio(moedasPorAnuncio);
        }
        else
        {
            // Backup via PlayerPrefs
            int moedasAtuais = PlayerPrefs.GetInt("MoedasTotais", 0);
            moedasAtuais += moedasPorAnuncio;
            PlayerPrefs.SetInt("MoedasTotais", moedasAtuais);
            PlayerPrefs.Save();
        }

        painelAnuncio.SetActive(false);
        contadorTexto.text = "";
        botaoFechar.interactable = false;
        textoFechar.color = corTextoBloqueado;
        emAnuncio = false;

        if (usos >= maxUsos)
            botaoAnuncio.interactable = false;
    }
}
