using UnityEngine;
using TMPro;

public class PontuacaoUI : MonoBehaviour
{
    public TMP_Text textoPontos;   // Texto na HUD da cena do Jogo
    private ArenaManager arena;

    void Start()
    {
        arena = FindAnyObjectByType<ArenaManager>();
    }

    void Update()
    {
        if (arena != null && textoPontos != null)
        {
            textoPontos.text = "Cestas: " + arena.pontosPartida;
        }
    }
}
