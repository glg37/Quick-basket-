using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class AnuncioCoin : MonoBehaviour
{
    [Header("Referências UI")]
    public GameObject painelAnuncio;   // Painel que contém a propaganda
    public Image imagemAnuncio;        // Imagem da propaganda
    public Button botaoAnuncio;        // Botão "Assistir Anúncio"
    public TMP_Text contadorTexto;     // Texto da contagem regressiva
    public int moedasPorAnuncio = 10;  // Moedas que o jogador recebe

    [Header("Configurações")]
    public float duracaoAnuncio = 5f;  // Duração do anúncio
    public int maxUsos = 2;            // Máximo de vezes que pode usar

    [Header("Cores do botão X")]
    public Color corTextoBloqueado = Color.gray;
    public Color corTextoAtivo = Color.white;

    private Button botaoFechar;
    private TMP_Text textoFechar;
    private int usos = 0;
    private bool emAnuncio = false;

    void Start()
    {
        // Se não tiver painel ou imagem, dá erro
        if (painelAnuncio == null || imagemAnuncio == null || botaoAnuncio == null)
        {
            Debug.LogError("Painel, imagem ou botão principal não atribuídos no Inspector!");
            return;
        }

        painelAnuncio.SetActive(false);

        // Se não tiver contador, cria um novo
        if (contadorTexto == null)
        {
            GameObject go = new GameObject("ContadorTexto");
            go.transform.SetParent(painelAnuncio.transform);
            contadorTexto = go.AddComponent<TMP_Text>();
            contadorTexto.alignment = TextAlignmentOptions.TopRight;
            contadorTexto.fontSize = 24;
            contadorTexto.rectTransform.anchorMin = new Vector2(1, 1);
            contadorTexto.rectTransform.anchorMax = new Vector2(1, 1);
            contadorTexto.rectTransform.pivot = new Vector2(1, 1);
            contadorTexto.rectTransform.anchoredPosition = new Vector2(-10, -10);
        }
        contadorTexto.text = "";

        // Tenta encontrar o botão X dentro do painel
        botaoFechar = painelAnuncio.GetComponentInChildren<Button>();
        if (botaoFechar == null)
        {
            // Se não existir, cria um botão X automaticamente
            GameObject btn = new GameObject("BotaoX");
            btn.transform.SetParent(painelAnuncio.transform);
            btn.AddComponent<RectTransform>().sizeDelta = new Vector2(50, 50);
            botaoFechar = btn.AddComponent<Button>();

            // Adiciona imagem para o botão
            Image img = btn.AddComponent<Image>();
            img.color = Color.red;

            // Adiciona texto "X"
            GameObject txt = new GameObject("TextoX");
            txt.transform.SetParent(btn.transform);
            textoFechar = txt.AddComponent<TMP_Text>();
            textoFechar.text = "X";
            textoFechar.alignment = TextAlignmentOptions.Center;
            textoFechar.fontSize = 24;
            textoFechar.color = corTextoBloqueado;
            textoFechar.rectTransform.anchorMin = new Vector2(0, 0);
            textoFechar.rectTransform.anchorMax = new Vector2(1, 1);
            textoFechar.rectTransform.offsetMin = Vector2.zero;
            textoFechar.rectTransform.offsetMax = Vector2.zero;
        }
        else
        {
            textoFechar = botaoFechar.GetComponentInChildren<TMP_Text>();
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

        // Dá moedas
        PlayerPrefs.SetInt("Moedas", PlayerPrefs.GetInt("Moedas", 0) + moedasPorAnuncio);

        painelAnuncio.SetActive(false);
        contadorTexto.text = "";
        botaoFechar.interactable = false;
        textoFechar.color = corTextoBloqueado;

        emAnuncio = false;

        if (usos >= maxUsos)
            botaoAnuncio.interactable = false;
    }
}
