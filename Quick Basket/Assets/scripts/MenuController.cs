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
        // Remove apenas dados de progresso da arena
        PlayerPrefs.DeleteKey("arenaAtual");
        PlayerPrefs.DeleteKey("acertos");

        //  Zera apenas as moedas da partida atual
        if (CoinManager.instance != null)
            CoinManager.instance.ZerarMoedasDoJogo();

        // Carrega a cena do jogo com fade
        StartCoroutine(FadeOutAndLoad("Jogo"));
    }

    public void Continuar()
    {
        SceneManager.LoadScene("Jogo");
    }

    public void AbrirCreditos()
    {
        if (painelCreditos != null)
            painelCreditos.SetActive(true);
    }

    public void FecharCreditos()
    {
        if (painelCreditos != null)
            painelCreditos.SetActive(false);
    }

    public void AbrirLoja()
    {
        if (painelLoja != null)
            painelLoja.SetActive(true);
    }

    public void FecharLoja()
    {
        if (painelLoja != null)
            painelLoja.SetActive(false);
    }

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
