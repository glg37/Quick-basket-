using UnityEngine;
using TMPro;  

public class Pontuacao : MonoBehaviour
{
    public TMP_Text textoPontos;  
    private int pontos = 0;  
    private bool bolaDentro = false;  

    void Start()
    {
        
        if (textoPontos != null)
        {
            textoPontos.text = "Pontos: " + pontos.ToString();
        }
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Ball") && !bolaDentro)  
        {
  
            pontos++;

   
            if (textoPontos != null)
            {
                textoPontos.text = "Pontos: " + pontos.ToString();
            }

      
            bolaDentro = true;
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
       
        if (other.CompareTag("Ball"))
        {
            
            bolaDentro = false;
        }
    }
}
