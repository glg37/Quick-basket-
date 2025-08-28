using UnityEngine;

public class WindZone : MonoBehaviour
{
    [Header("Força do Vento")]
    public float windForce = 2f; 

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            rb.AddForce(Vector2.right * windForce);
        }
    }
}
