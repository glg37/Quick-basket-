using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float tempoLimite = 60f;
    private float tempoRestante;
    public TextMeshProUGUI timerTexto;
    public GameObject gameOverPanel;
    public Button restartButton;

    private bool tempoPausado = false;

    void Start()
    {
        
        Time.timeScale = 1f;

        tempoRestante = tempoLimite;
        gameOverPanel.SetActive(false);
        restartButton.gameObject.SetActive(false);

        if (timerTexto != null)
            timerTexto.text = "Tempo: " + tempoRestante.ToString("F0");

        restartButton.onClick.AddListener(RestartGame);
    }

    void Update()
    {
        if (!tempoPausado && tempoRestante > 0)
        {
            tempoRestante -= Time.deltaTime;
            if (timerTexto != null)
                timerTexto.text = "Tempo: " + tempoRestante.ToString("F0");
        }
        else if (tempoRestante <= 0)
        {
            TempoAcabou();
        }
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


    public float GetTempoRestante()
    {
        return tempoRestante;
    }

    
    public void SetTempoRestante(float tempo)
    {
        tempoRestante = tempo;
        if (timerTexto != null)
            timerTexto.text = "Tempo: " + tempoRestante.ToString("F0");
    }
}
