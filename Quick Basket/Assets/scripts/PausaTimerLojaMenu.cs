using UnityEngine;
using UnityEngine.UI;

public class PausaTimerLojaMenu : MonoBehaviour
{
    public Button botaoComprar;     // Botão da loja
    public int precoMoedas = 10;    // Preço do item
    private const string ITEM_COMPRADO_KEY = "ItemPausaComprado";
    private const string MOEDAS_KEY = "MoedasTotais";

    void Start()
    {
        botaoComprar.onClick.AddListener(ComprarItem);
    }

    void ComprarItem()
    {
        int moedasTotais = PlayerPrefs.GetInt(MOEDAS_KEY, 0);

        if (PlayerPrefs.GetInt(ITEM_COMPRADO_KEY, 0) == 1)
        {
            Debug.Log("Item já comprado, use na partida.");
            return;
        }

        if (moedasTotais >= precoMoedas)
        {
            moedasTotais -= precoMoedas;                       // desconta moedas
            PlayerPrefs.SetInt(MOEDAS_KEY, moedasTotais);      // salva no PlayerPrefs
            PlayerPrefs.SetInt(ITEM_COMPRADO_KEY, 1);          // marca item comprado
            PlayerPrefs.Save();                                 // salva tudo imediatamente

            Debug.Log("Item comprado! Moedas restantes: " + moedasTotais);

            // Atualiza a UI do menu imediatamente
            Object.FindFirstObjectByType<CoinDisplayMenu>()?.AtualizarUI();
        }
        else
        {
            Debug.Log("Moedas insuficientes!");
        }
    }
}
