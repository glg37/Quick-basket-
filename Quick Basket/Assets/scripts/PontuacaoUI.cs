using UnityEngine;
using TMPro;

public class PontuacaoUI : MonoBehaviour
{
    public TMP_Text textoPontos;
    private ArenaManager arena;

    void Start()
    {
        arena = FindAnyObjectByType<ArenaManager>();
    }

    void Update()
    {
        if (arena != null)
            textoPontos.text = "Cestas: " + arena.pontosPartida;
    }
}
