using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Button continuarButton; // arraste o botão "Continuar" no inspector

    void Start()
    {
        AtualizarMenu();
    }

    void AtualizarMenu()
    {
        // Se já existe progresso salvo  ativa o botão continuar
        if (PlayerPrefs.HasKey("arenaAtual"))
        {
            continuarButton.gameObject.SetActive(true);
        }
        else
        {
            continuarButton.gameObject.SetActive(false);
        }
    }

    public void Jogar()
    {
        // Novo jogo  limpa save antigo
        PlayerPrefs.DeleteKey("arenaAtual");
        PlayerPrefs.DeleteKey("acertos");
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
}
