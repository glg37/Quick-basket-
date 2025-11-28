using UnityEngine;
using UnityEngine.UI;

public class PowerUpX2Ativador : MonoBehaviour
{
    public Button botaoAtivar; // botão dentro da HUD do jogo

    private const string KEY = "PowerUpDoublePoints";

    private ArenaManager arena;

    void Start()
    {
        arena = FindAnyObjectByType<ArenaManager>();
        botaoAtivar.onClick.AddListener(Ativar);

        // Só mostra o botão se tiver comprado
        if (PlayerPrefs.GetInt(KEY, 0) == 0)
            botaoAtivar.gameObject.SetActive(false);
    }

    void Ativar()
    {
        if (arena != null)
        {
            arena.AtivarX2(20f);  // ativa por 20 segundos
            botaoAtivar.gameObject.SetActive(false); // esconde após usar
        }
    }
}
