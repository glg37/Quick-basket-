using UnityEngine;

public class PausaTimerLojaMenu : MonoBehaviour
{
    public int precoMoedas = 10;
    private const string ITEM_COMPRADO_KEY = "ItemPausaComprado";

    // ESTA função aparece no OnClick()
    public void ComprarItem()
    {
        if (PlayerPrefs.GetInt(ITEM_COMPRADO_KEY, 0) == 1)
        {
            Debug.Log("Item já comprado! Você já pode usar na partida.");
            return;
        }

        bool comprado = CoinManager.instance.TentarGastarMoedas(precoMoedas);

        if (comprado)
        {
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
