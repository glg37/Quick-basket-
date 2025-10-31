using UnityEngine;
using UnityEngine.UI;

public class PausaTimerJogo : MonoBehaviour
{
    public Button botaoJogo;    // Botão que já está na cena (Canvas)
    public Timer timerScript;   // Referência ao Timer

    private const string ITEM_COMPRADO_KEY = "ItemPausaComprado";

    void Start()
    {
        // Esconde o botão no início
        botaoJogo.gameObject.SetActive(false);

        // Se o item foi comprado na loja, mostra o botão
        if (PlayerPrefs.GetInt(ITEM_COMPRADO_KEY, 0) == 1)
            botaoJogo.gameObject.SetActive(true);

        // Adiciona listener para usar o item
        botaoJogo.onClick.AddListener(UsarItem);
    }

    void UsarItem()
    {
        timerScript.PausarTimer(10f);             // pausa 10 segundos
        botaoJogo.gameObject.SetActive(false);    // esconde o botão
        PlayerPrefs.SetInt(ITEM_COMPRADO_KEY, 0); // permite comprar de novo
        PlayerPrefs.Save();
    }
}
