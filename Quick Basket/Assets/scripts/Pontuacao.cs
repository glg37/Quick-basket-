using UnityEngine;
using TMPro;  // Adicione essa linha para usar o TextMeshPro

public class Pontuacao : MonoBehaviour
{
    public TMP_Text textoPontos;  // Refer�ncia ao componente TextMeshPro
    private int pontos = 0;  // Contador de pontos

    void Start()
    {
        // Certifica-se de que o texto come�a com 0 pontos
        if (textoPontos != null)
        {
            textoPontos.text = "Pontos: " + pontos.ToString();
        }
    }

    // Fun��o chamada quando algo entra no trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o objeto que colidiu � a bola (voc� pode ajustar isso com a tag ou nome do objeto)
        if (other.CompareTag("Ball"))  // Aqui a bola deve ter a tag "Bola"
        {
            // Incrementa os pontos
            pontos++;

            // Atualiza o texto do contador de pontos
            if (textoPontos != null)
            {
                textoPontos.text = "Pontos: " + pontos.ToString();
            }
        }
    }
}
