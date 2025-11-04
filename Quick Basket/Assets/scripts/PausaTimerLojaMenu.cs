using UnityEngine;
using UnityEngine.UI;

public class PausaTimerLojaMenu : MonoBehaviour
{
    public Button botaoComprar;     // Botão da loja
    public int precoMoedas = 10;    // Preço do item

    private const string ITEM_COMPRADO_KEY = "ItemPausaComprado";

    void Start()
    {
        botaoComprar.onClick.AddListener(ComprarItem);
    }

    void ComprarItem()
    {
        // Se já comprou o item antes
        if (PlayerPrefs.GetInt(ITEM_COMPRADO_KEY, 0) == 1)
        {
            Debug.Log("Item já comprado! Você já pode usar na partida.");
            return;
        }

        // Tenta gastar moedas usando o CoinManager
        bool comprado = CoinManager.instance.TentarGastarMoedas(precoMoedas);

        if (comprado)
        {
            // Marca item como comprado
            PlayerPrefs.SetInt(ITEM_COMPRADO_KEY, 1);
            PlayerPrefs.Save();

            Debug.Log("Item comprado com sucesso!");
        }
        else
        {
            Debug.Log("Moedas insuficientes!");
        }
    }
}
