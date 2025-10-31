using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    private const string JOGO_INICIALIZADO_KEY = "JogoInicializado";

    void Awake()
    {
        // Se ainda não inicializou, zera todas as moedas e flags
        if (PlayerPrefs.GetInt(JOGO_INICIALIZADO_KEY, 0) == 0)
        {
            PlayerPrefs.SetInt("MoedasTotais", 0);
            PlayerPrefs.SetInt("MoedasPartida", 0);
            PlayerPrefs.SetInt("ItemPausaComprado", 0); // se tiver itens da loja
            PlayerPrefs.SetInt(JOGO_INICIALIZADO_KEY, 1); // marca que já inicializou
            PlayerPrefs.Save();

            Debug.Log("PlayerPrefs zerado na primeira execução!");
        }
    }
}
