using UnityEngine;


public class Ball : MonoBehaviour
{
    public Rigidbody2D rbBola;  
    public float forcaMinima = 5f;  
    public float forcaMaxima = 30f;  
    public Vector2 direcionamento = new Vector2(1f, 1f);  

    

    private float tempoPressionado = 0f; 

    void Start()
    {
       
        if (rbBola == null)
        {
            rbBola = GetComponent<Rigidbody2D>();
        }

        
    }

   
    public void IniciarPressionamento()
    {
        tempoPressionado = 0f;  
    }

    
    public void FinalizarPressionamento()
    {
        LancarBola();  
    }

    void Update()
    {
       
        if (tempoPressionado >= 0f)
        {
            tempoPressionado += Time.deltaTime;  
        }
    }

    
    public void LancarBola()
    {
        
        rbBola.linearVelocity = Vector2.zero;
        rbBola.gravityScale = 1f; 

        
        float forcaLancamento = Mathf.Lerp(forcaMinima, forcaMaxima, tempoPressionado);

       
        rbBola.AddForce(direcionamento.normalized * forcaLancamento, ForceMode2D.Impulse);

       
        
    }

    
    

    
  
    
}
