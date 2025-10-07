using UnityEngine;

public class Basket : MonoBehaviour
{
    [Header("Partícula de Confete")]
    public GameObject confetePrefab;           // Prefab da partícula
    public Material neonMaterial;              // Material neon para fases escuras
    public Material normalMaterial;            // Material padrão para fases normais
    public int arenaNeon = 1;                  // Índice da arena onde deve usar neon (ajuste conforme sua cena)

    private bool jaExplodiu = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball") && !jaExplodiu)
        {
            jaExplodiu = true;

            // --- Partícula ---
            if (confetePrefab != null)
            {
                GameObject confete = Instantiate(confetePrefab, transform.position, Quaternion.identity);

                // Pega o ArenaManager
                ArenaManager arenaManager = FindFirstObjectByType<ArenaManager>();
                if (arenaManager != null)
                {
                    int arenaIndex = arenaManager.GetArenaAtualIndex();

                    // Verifica se está na arena escura e aplica material neon
                    ParticleSystemRenderer pr = confete.GetComponent<ParticleSystemRenderer>();
                    if (pr != null)
                    {
                        if (arenaIndex == arenaNeon && neonMaterial != null)
                            pr.material = neonMaterial;
                        else if (normalMaterial != null)
                            pr.material = normalMaterial;
                    }
                }

                // Destrói a partícula após terminar
                ParticleSystem ps = confete.GetComponent<ParticleSystem>();
                if (ps != null)
                    Destroy(confete, ps.main.duration + ps.main.startLifetime.constantMax);
                else
                    Destroy(confete, 2f);
            }

            // Lógica da cesta
            FindFirstObjectByType<ArenaManager>().AcertouCesta();
            FindFirstObjectByType<BasketSpawner>().SpawnNovaCesta();
            Destroy(gameObject);
        }
    }
}
