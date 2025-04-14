using UnityEngine;
using TMPro;

public class Pontuacao : MonoBehaviour
{

    public TMP_Text textoPontos;  

    private int pontos = 0;       
    private bool bolaDentro = false;

    void Start()
    {
        AtualizarTexto();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Ball") && !bolaDentro)
        {
            pontos++;               
            bolaDentro = true;       
            AtualizarTexto();       
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        
        if (other.CompareTag("Ball"))
        {
            bolaDentro = false;   
        }
    }

    void AtualizarTexto()
    {
       
        if (textoPontos != null)
        {
            textoPontos.text = "Pontos: " + pontos.ToString();
        }
    }
}
