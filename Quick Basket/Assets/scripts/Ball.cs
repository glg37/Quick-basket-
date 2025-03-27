using UnityEngine;
using UnityEngine.UI;  

public class Ball : MonoBehaviour
{
    public GameObject bola;  
    public Button botaoLancar;  
    public float for�aLan�amento = 10f; 
    public Vector2 dire��oLan�amento = new Vector2(1f, 1f);  

    private Rigidbody2D rbBola;  

    void Start()
    {
        rbBola = bola.GetComponent<Rigidbody2D>();  

       
        botaoLancar.onClick.AddListener(Lan�arBola);
    }

    
    void Lan�arBola()
    {
       
        rbBola.velocity = Vector2.zero;  
        rbBola.gravityScale = 1f; 

      
        rbBola.AddForce(dire��oLan�amento.normalized * for�aLan�amento, ForceMode2D.Impulse);  

        
    }
}
