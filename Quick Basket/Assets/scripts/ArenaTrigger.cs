using UnityEngine;

public class ArenaTrigger : MonoBehaviour
{
    public ArenaManager arenaManager;
    public int arenaIndex; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            arenaManager.AtivarTetoDaArena(arenaIndex - 1);
        }
    }
}
