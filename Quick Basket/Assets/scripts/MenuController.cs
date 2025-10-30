using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MenuController : MonoBehaviour
{
    [Header("Botões")]
    public Button continuarButton;

    [Header("UI Painéis")]
    public GameObject painelCreditos;
    public GameObject painelLoja;

    [Header("Transição de Fade")]
    public Image fadeImage;
    public float fadeDuration = 1f;

    void Start()
    {
        if (continuarButton != null)
            continuarButton.gameObject.SetActive(PlayerPrefs.HasKey("arenaAtual"));

        if (painelCreditos != null)
            painelCreditos.SetActive(false);

        if (painelLoja != null)
            painelLoja.SetActive(false);

        if (fadeImage != null)
        {
            Color c = fadeImage.color;
            c.a = 0f;
            fadeImage.color = c;
            fadeImage.gameObject.SetActive(false);
        }
    }

    public void Jogar()
    {
        // Flag para zerar moedas da partida ao iniciar cena de jogo
        PlayerPrefs.SetInt("ZerarPartida", 1);
        StartCoroutine(FadeOutAndLoad("Jogo"));
    }

    public void Continuar()
    {
        // Resume jogo com moedas da partida carregadas
        PlayerPrefs.SetInt("ZerarPartida", 0);
        StartCoroutine(FadeOutAndLoad("Jogo"));
    }

    public void AbrirCreditos() => painelCreditos?.SetActive(true);
    public void FecharCreditos() => painelCreditos?.SetActive(false);
    public void AbrirLoja() => painelLoja?.SetActive(true);
    public void FecharLoja() => painelLoja?.SetActive(false);

    private IEnumerator FadeOutAndLoad(string cena)
    {
        if (fadeImage == null)
        {
            SceneManager.LoadScene(cena);
            yield break;
        }

        fadeImage.gameObject.SetActive(true);
        Color c = fadeImage.color;
        c.a = 0f;
        fadeImage.color = c;

        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(0f, 1f, t / fadeDuration);
            fadeImage.color = c;
            yield return null;
        }

        SceneManager.LoadScene(cena);
    }
}
