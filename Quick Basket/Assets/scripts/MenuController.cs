using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [Header("Bot�es")]
    public Button continuarButton;

    [Header("UI Pain�is")]
    public GameObject painelCreditos; 

    void Start()
    {
        if (continuarButton != null)
            continuarButton.gameObject.SetActive(PlayerPrefs.HasKey("arenaAtual"));

        if (painelCreditos != null)
            painelCreditos.SetActive(false); 
    }

    public void Jogar()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Jogo");
    }

    public void Continuar()
    {
        SceneManager.LoadScene("Jogo");
    }

    public void Sair()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
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
}
