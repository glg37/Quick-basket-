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

            PlayerPrefs.Save();

            jaResetou = true;
            Debug.Log(" Reset feito ao abrir o app.");
        }
        else
        {
            Debug.Log(" Cena recarregada, mas NÃO resetar.");
        }
    }
}
