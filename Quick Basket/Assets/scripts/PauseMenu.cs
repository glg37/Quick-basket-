using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI; 

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;  
    public Button continueButton;   
    public Button quitButton;       

    private bool isPaused = false; 

    void Start()
    {
        
        pauseMenuUI.SetActive(false);

     
        continueButton.onClick.AddListener(ContinueGame);
        quitButton.onClick.AddListener(QuitToMenu);
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ContinueGame();  
            }
            else
            {
                PauseGame();  
            }
        }
    }

   
    void PauseGame()
    {
        pauseMenuUI.SetActive(true); 
        Time.timeScale = 0f;  
        isPaused = true;  
    }

    
    void ContinueGame()
    {
        pauseMenuUI.SetActive(false); 
        Time.timeScale = 1f;  
        isPaused = false;  
    }

   
    void QuitToMenu()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("Menu");  
    }
}
