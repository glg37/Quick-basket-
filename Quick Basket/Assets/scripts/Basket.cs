using UnityEngine;

public class Basket : MonoBehaviour
{
    [Header("Partícula de Confete")]
    public GameObject confetePrefab;
    public Material neonMaterial;
    public Material normalMaterial;
    public int arenaNeon = 1;

    [Header("Áudio")]
    public AudioClip somConfete; // Som da cesta
    [Range(0f, 1f)] public float volumeSom = 0.1f; // Volume do som

    private bool jaExplodiu = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball") && !jaExplodiu)
        {
            jaExplodiu = true;

            // Instancia confete
            if (confetePrefab != null)
            {
                GameObject confete = Instantiate(confetePrefab, transform.position, Quaternion.identity);

                // Define material do confete
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

                // Destroi confete depois da duração do sistema de partículas
                ParticleSystem ps = confete.GetComponent<ParticleSystem>();
                if (ps != null)
                    Destroy(confete, ps.main.duration + ps.main.startLifetime.constantMax);
                else
                    Destroy(confete, 2f);
            }

            // Toca o som usando objeto temporário
            if (somConfete != null)
            {
                GameObject tempAudio = new GameObject("TempAudio");
                tempAudio.transform.position = transform.position;
                AudioSource aSource = tempAudio.AddComponent<AudioSource>();
                aSource.clip = somConfete;
                aSource.volume = volumeSom;
                aSource.Play();
                Destroy(tempAudio, somConfete.length + 0.1f);
            }

            // Adiciona tempo extra
            Timer timer = FindFirstObjectByType<Timer>();
            if (timer != null)
            {
                timer.AdicionarTempo(5f);
            }

            // Notifica ArenaManager e BasketSpawner
            FindFirstObjectByType<ArenaManager>().AcertouCesta();
            FindFirstObjectByType<BasketSpawner>().SpawnNovaCesta();

            Destroy(gameObject);
        }
    }
}
