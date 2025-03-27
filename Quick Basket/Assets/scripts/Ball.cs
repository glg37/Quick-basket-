using UnityEngine;
using UnityEngine.UI;  

public class Ball : MonoBehaviour
{
    public GameObject bola;  
    public Button botaoLancar;  
    public float forçaLançamento = 10f; 
    public Vector2 direçãoLançamento = new Vector2(1f, 1f);  

    private Rigidbody2D rbBola;  

    void Start()
    {
        rbBola = bola.GetComponent<Rigidbody2D>();  

       
        botaoLancar.onClick.AddListener(LançarBola);
    }

    
    void LançarBola()
    {
       
        rbBola.velocity = Vector2.zero;  
        rbBola.gravityScale = 1f; 

      
        rbBola.AddForce(direçãoLançamento.normalized * forçaLançamento, ForceMode2D.Impulse);  

        
    }
}
