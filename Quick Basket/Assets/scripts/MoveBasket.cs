using UnityEngine;

public class MoveBasket : MonoBehaviour
{
    
    public float moveSpeed = 5f;

   
    public float upperLimit = 10f;  
    public float lowerLimit = 0f;   

    // Variável para controlar a direção do movimento
    private bool movingUp = true;

    void Update()
    {
        MoveBasketVertically();
    }

    void MoveBasketVertically()
    {
        
        if (movingUp)
        {
           
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;

            
            if (transform.position.y >= upperLimit)
            {
                movingUp = false;
            }
        }
        else
        {
           
            transform.position += Vector3.down * moveSpeed * Time.deltaTime;

            
            if (transform.position.y <= lowerLimit)
            {
                movingUp = true;
            }
        }
    }
}
