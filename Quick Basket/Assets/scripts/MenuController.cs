using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Button continuarButton; // arraste o botão "Continuar" no inspector

    void Start()
    {
        // Desativa o botão continuar, pois não há mais save
        if (continuarButton != null)
            continuarButton.gameObject.SetActive(false);
    }

    public void Jogar()
    {
        // Novo jogo
        SceneManager.LoadScene("Jogo");
    }

    public void Continuar()
    {
        // Agora Continuar faz a mesma coisa que Jogar
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
