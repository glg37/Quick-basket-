using UnityEngine;
using UnityEngine.UI;

public class PausaTimerJogo : MonoBehaviour
{
    public Button botaoJogo;            // Botão que já está na cena (Canvas)
    public Timer timerScript;           // Referência ao Timer
    public Image telaCongelada;         // A imagem azulada da tela

    private const string ITEM_COMPRADO_KEY = "ItemPausaComprado";

    void Start()
    {
        // Esconde o botão no início
        botaoJogo.gameObject.SetActive(false);

        // Esconde o efeito de congelamento no início
        if (telaCongelada != null)
            telaCongelada.gameObject.SetActive(false);

        // Se o item foi comprado na loja, mostra o botão
        if (PlayerPrefs.GetInt(ITEM_COMPRADO_KEY, 0) == 1)
            botaoJogo.gameObject.SetActive(true);

        // Listener de clique
        botaoJogo.onClick.AddListener(UsarItem);
    }

    void UsarItem()
    {
        // Aplica a pausa
        timerScript.PausarTimer(10f);

        // Ativa a tela azulada
        if (telaCongelada != null)
            telaCongelada.gameObject.SetActive(true);

        // Some o botão
        botaoJogo.gameObject.SetActive(false);

        // Marca item como não comprado
        PlayerPrefs.SetInt(ITEM_COMPRADO_KEY, 0);
        PlayerPrefs.Save();

        // Remove o efeito após os 10 segundos
        Invoke(nameof(VoltarTelaNormal), 10f);
    }

    void VoltarTelaNormal()
    {
        if (telaCongelada != null)
            telaCongelada.gameObject.SetActive(false);
    }
}
