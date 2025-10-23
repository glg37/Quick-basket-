using UnityEngine;

public class Basket : MonoBehaviour
{
    [Header("Partícula de Confete")]
    public GameObject confetePrefab;
    public Material neonMaterial;
    public Material normalMaterial;
    public int arenaNeon = 1;

    private bool jaExplodiu = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball") && !jaExplodiu)
        {
            jaExplodiu = true;

           
            if (confetePrefab != null)
            {
                GameObject confete = Instantiate(confetePrefab, transform.position, Quaternion.identity);

                
                ArenaManager arenaManager = FindFirstObjectByType<ArenaManager>();
                if (arenaManager != null)
                {
                    int arenaIndex = arenaManager.GetArenaAtualIndex();

                    ParticleSystemRenderer pr = confete.GetComponent<ParticleSystemRenderer>();
                    if (pr != null)
                    {
                        if (arenaIndex == arenaNeon && neonMaterial != null)
                            pr.material = neonMaterial;
                        else if (normalMaterial != null)
                            pr.material = normalMaterial;
                    }
                }

               
                ParticleSystem ps = confete.GetComponent<ParticleSystem>();
                if (ps != null)
                    Destroy(confete, ps.main.duration + ps.main.startLifetime.constantMax);
                else
                    Destroy(confete, 2f);
            }

            
            Timer timer = FindFirstObjectByType<Timer>();
            if (timer != null)
            {
                timer.AdicionarTempo(5f);
            }

           
            FindFirstObjectByType<ArenaManager>().AcertouCesta();
            FindFirstObjectByType<BasketSpawner>().SpawnNovaCesta();
            Destroy(gameObject);
        }
    }
}
