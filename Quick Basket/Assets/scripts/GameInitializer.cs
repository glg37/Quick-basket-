using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    private static bool jaResetou = false;

    void Awake()
    {
        // Só reseta uma única vez enquanto o app estiver aberto
        if (!jaResetou)
        {
            PlayerPrefs.SetInt("MoedasTotais", 0);
            PlayerPrefs.SetInt("MoedasPartida", 0);
            PlayerPrefs.SetInt("ItemPausaComprado", 0);

            // RESETANDO TAMBÉM O RECORDE
            PlayerPrefs.SetInt("recorde", 0);

            PlayerPrefs.Save();

            jaResetou = true;
            Debug.Log("Reset feito ao abrir o app, incluindo recorde.");
        }
        else
        {
            Debug.Log("Cena recarregada, mas NÃO resetar.");
        }
    }
}
