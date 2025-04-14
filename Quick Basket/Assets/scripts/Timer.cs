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

    
    void Start()
    {
        tempoRestante = tempoLimite;
        gameOverPanel.SetActive(false); 
        restartButton.gameObject.SetActive(false); 

        if (timerTexto != null)
        {
            timerTexto.text = "Tempo: " + tempoRestante.ToString("F0");
        }

        
        restartButton.onClick.AddListener(RestartGame);
    }

  
    void Update()
    {
        if (tempoRestante > 0)
        {
            tempoRestante -= Time.deltaTime; 
            if (timerTexto != null)
            {
                timerTexto.text = "Tempo: " + tempoRestante.ToString("F0"); 
            }
        }
        else
        {
        
            TempoAcabou();
        }
    }

  
    void TempoAcabou()
    {
       
        gameOverPanel.SetActive(true);
        restartButton.gameObject.SetActive(true); 

       
        PausarJogo();
    }

    void PausarJogo()
    {
        Time.timeScale = 0f; 
    }

   
    void RestartGame()
    {

        Time.timeScale = 1f; 

     
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        
        tempoRestante = tempoLimite;

        gameOverPanel.SetActive(false);
        restartButton.gameObject.SetActive(false);
    }
}
