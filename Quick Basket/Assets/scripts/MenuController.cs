using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [Header("Botões")]
    public Button continuarButton;

    [Header("UI Painéis")]
    public GameObject painelCreditos; // Arraste seu painel de créditos aqui no Inspector

    void Start()
    {
        if (continuarButton != null)
            continuarButton.gameObject.SetActive(PlayerPrefs.HasKey("arenaAtual"));

        if (painelCreditos != null)
            painelCreditos.SetActive(false); // garante que começa fechado
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
