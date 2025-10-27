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
    public Button botaoFechar;         // Botão "X" para fechar
    public TMP_Text contadorTexto;     // Contagem regressiva
    public int moedasPorAnuncio = 10;  // Quantas moedas o jogador ganha

    [Header("Configurações")]
    public float duracaoAnuncio = 5f;  // Contagem regressiva do anúncio
    public int maxUsos = 2;            // Máximo de vezes que o anúncio pode ser usado

    private int usos = 0;               // Quantas vezes já usou
    private bool emAnuncio = false;

    private void Start()
    {
        botaoAnuncio.onClick.AddListener(AssistirAnuncio);
        botaoFechar.onClick.AddListener(FecharAnuncio);

        painelAnuncio.SetActive(false);
        contadorTexto.text = "";
        botaoFechar.interactable = false; // X inicialmente desativado
    }

    void AssistirAnuncio()
    {
        if (emAnuncio || usos >= maxUsos)
            return;

        StartCoroutine(AnuncioCoroutine());
    }

    IEnumerator AnuncioCoroutine()
    {
        emAnuncio = true;
        usos++;

        painelAnuncio.SetActive(true);
        imagemAnuncio.gameObject.SetActive(true);

        botaoFechar.interactable = false;
        contadorTexto.gameObject.SetActive(true);

        float tempoRestante = duracaoAnuncio;

        // Contagem regressiva real
        while (tempoRestante > 0)
        {
            contadorTexto.text = $"Fechar em {Mathf.Ceil(tempoRestante)}s";
            yield return new WaitForSeconds(1f);
            tempoRestante -= 1f;
        }

        contadorTexto.text = "Clique no X para fechar!";
        botaoFechar.interactable = true;
    }

    void FecharAnuncio()
    {
        if (!emAnuncio || !botaoFechar.interactable)
            return;

        // Dá as moedas
        PlayerPrefs.SetInt("Moedas", PlayerPrefs.GetInt("Moedas", 0) + moedasPorAnuncio);

        // Fecha o painel
        painelAnuncio.SetActive(false);
        contadorTexto.text = "";
        botaoFechar.interactable = false;

        emAnuncio = false;

        // Se já atingiu o limite, desativa o botão
        if (usos >= maxUsos)
            botaoAnuncio.interactable = false;
    }
}
