using UnityEngine;

public class Ball : MonoBehaviour
{
    
    public float forcaLancamento = 15f;

    
    public Vector2 direcaoLancamento = new Vector2(1f, 1f);

    private Rigidbody2D rb;

   
    private int pontos = 0;

    void Start()
    {
       
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
     
        if (Input.GetMouseButtonDown(0))
        {
            LancaBola();
        }
    }

    
    void LancaBola()
    {
    
        rb.linearVelocity = Vector2.zero; 

    
        Vector2 direcao = direcaoLancamento.normalized;
        rb.AddForce(direcao * forcaLancamento, ForceMode2D.Impulse);
    }

   
}
