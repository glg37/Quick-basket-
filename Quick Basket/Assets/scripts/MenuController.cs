using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using TMPro;   // <--- IMPORTANTE para TextMeshPro

public class MenuController : MonoBehaviour
{
    [Header("Botões")]
    public Button continuarButton;
    public Button resetarButton;

    [Header("UI Painéis")]
    public GameObject painelCreditos;
    public GameObject painelLoja;

    [Header("Transição de Fade")]
    public Image fadeImage;
    public float fadeDuration = 1f;

    [Header("Recorde")]
    public TMP_Text recordeText;   // <-- AGORA É TMP_Text

    void Awake()
    {
        CoinManager.GarantirInstancia();
    }

    void Start()
    {
        bool existeJogoSalvo = PlayerPrefs.HasKey("arenaAtual");

        if (continuarButton != null)
            continuarButton.gameObject.SetActive(existeJogoSalvo);

        if (resetarButton != null)
            resetarButton.gameObject.SetActive(existeJogoSalvo);

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

        // Atualiza o recorde na UI
        AtualizarRecorde();

        continuarButton?.onClick.AddListener(Continuar);
        resetarButton?.onClick.AddListener(ResetarPartida);
    }

    private void AtualizarRecorde()
    {
        if (recordeText == null) return;

        // Se NÃO existir recorde, o texto some
        if (!PlayerPrefs.HasKey("recorde"))
        {
            recordeText.gameObject.SetActive(false);
            return;
        }

        int recorde = PlayerPrefs.GetInt("recorde", 0);
        recordeText.text = "Recorde: " + recorde;
        recordeText.gameObject.SetActive(true);
    }

    public void Jogar()
    {
        PlayerPrefs.SetInt("ZerarPartida", 1);
        StartCoroutine(FadeOutAndLoad("Jogo"));
    }

    public void Continuar()
    {
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

    public void ResetarPartida()
    {
        if (CoinManager.instance != null)
            CoinManager.instance.ZerarMoedasDaPartida();

        PlayerPrefs.DeleteKey("arenaAtual");
        PlayerPrefs.DeleteKey("acertos");
        PlayerPrefs.DeleteKey("ZerarPartida");

        continuarButton?.gameObject.SetActive(false);
        resetarButton?.gameObject.SetActive(false);

        AtualizarRecorde();
    }
}
