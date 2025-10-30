using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [Header("Configurações de Tempo")]
    public float tempoLimite = 30f;
    private float tempoRestante;
    public float bonusPorCesta = 5f;
    public float velocidadeTempo = 1.5f;

    [Header("UI e Painel de Game Over")]
    public TextMeshProUGUI timerTexto;
    public GameObject gameOverPanel;
    public Button restartButton;

    private bool tempoPausado = false;

    void Awake()
    {
        DontDestroyOnLoad(gameObject); // Mantém o Timer entre cenas
    }

    void Start()
    {
        Time.timeScale = 1f;
        gameOverPanel.SetActive(false);
        restartButton.gameObject.SetActive(false);

        // Carrega o tempo salvo, se houver
        if (PlayerPrefs.HasKey("tempoRestante"))
            tempoRestante = PlayerPrefs.GetFloat("tempoRestante");
        else
            tempoRestante = tempoLimite;

        AtualizarTextoTimer();
        restartButton.onClick.AddListener(RestartGame);
    }

    void Update()
    {
        if (!tempoPausado && tempoRestante > 0)
        {
            tempoRestante -= Time.deltaTime * velocidadeTempo;
            AtualizarTextoTimer();
        }
        else if (tempoRestante <= 0)
        {
            TempoAcabou();
        }
    }

    private void AtualizarTextoTimer()
    {
        if (timerTexto != null)
            timerTexto.text = Mathf.CeilToInt(tempoRestante) + "s";
    }

    public void AdicionarTempo(float quantidade)
    {
        tempoRestante += quantidade;
        if (tempoRestante > tempoLimite)
            tempoRestante = tempoLimite;
        AtualizarTextoTimer();
    }

    public void PausarTimer(float duracao)
    {
        StartCoroutine(PausarTemporario(duracao));
    }

    private System.Collections.IEnumerator PausarTemporario(float duracao)
    {
        tempoPausado = true;
        yield return new WaitForSecondsRealtime(duracao);
        tempoPausado = false;
    }

    void TempoAcabou()
    {
        gameOverPanel.SetActive(true);
        restartButton.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public float GetTempoRestante() => tempoRestante;

    public void SetTempoRestante(float tempo)
    {
        tempoRestante = tempo;
        AtualizarTextoTimer();
    }

 
    public void SalvarTempo()
    {
        PlayerPrefs.SetFloat("tempoRestante", tempoRestante);
        PlayerPrefs.Save();
    }

    public void CarregarTempo()
    {
        if (PlayerPrefs.HasKey("tempoRestante"))
        {
            tempoRestante = PlayerPrefs.GetFloat("tempoRestante");
            AtualizarTextoTimer();
        }
    }
}
