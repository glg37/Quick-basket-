using UnityEngine;

public class Basket : MonoBehaviour
{
    [Header("Partícula de Confete")]
    public GameObject confetePrefab;
    public Material neonMaterial;
    public Material normalMaterial;
    public int arenaNeon = 1;

    [Header("Áudio")]
    public AudioClip somConfete;        
    [Range(0f, 1f)] public float volumeSom = 0.1f; 

    [Header("Tempo extra")]
    public float tempoExtra = 5f;

    private bool jaExplodiu = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!jaExplodiu && collision.CompareTag("Ball"))
        {
            jaExplodiu = true;

            
            if (confetePrefab != null)
            {
                GameObject confete = Instantiate(confetePrefab, transform.position, Quaternion.identity);

                ParticleSystemRenderer pr = confete.GetComponent<ParticleSystemRenderer>();
                if (pr != null)
                {
                    ArenaManager arenaManager = FindFirstObjectByType<ArenaManager>();
                    if (arenaManager != null)
                    {
                        int arenaIndex = arenaManager.GetArenaAtualIndex();
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

            
            if (somConfete != null)
            {
               
                AudioSource.PlayClipAtPoint(somConfete, transform.position, volumeSom);
            }

            
            Timer timer = FindFirstObjectByType<Timer>();
            if (timer != null)
                timer.AdicionarTempo(tempoExtra);

            
            ArenaManager am = FindFirstObjectByType<ArenaManager>();
            if (am != null)
                am.AcertouCesta();

            BasketSpawner bs = FindFirstObjectByType<BasketSpawner>();
            if (bs != null)
                bs.SpawnNovaCesta();

            
            Destroy(gameObject);
        }
    }
}
