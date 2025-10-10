using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [Header("Botões")]
    public Button continuarButton;

    [Header("UI Painéis")]
    public GameObject painelCreditos;
    public GameObject painelLoja; 

    void Start()
    {
        if (continuarButton != null)
            continuarButton.gameObject.SetActive(PlayerPrefs.HasKey("arenaAtual"));

        if (painelCreditos != null)
            painelCreditos.SetActive(false);

        if (painelLoja != null)
            painelLoja.SetActive(false); 
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
}
