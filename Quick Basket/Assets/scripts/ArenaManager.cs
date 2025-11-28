using UnityEngine;
using UnityEngine.SceneManagement;

public class ArenaManager : MonoBehaviour
{
    [Header("Arenas e Spawners")]
    public GameObject[] arenas;
    public int[] cestasParaDescer;
    public GameObject[] spawnersDeMoedasGO;
    private CoinSpawner[] spawnersDeMoedas;

    [Header("Câmera e Visual")]
    public Camera mainCamera;
    public Vector3[] posicoesCamera;
    public Color[] coresArenas;

    [Header("Movimentação")]
    public bool[] controlesInvertidos;

    [Header("Outros")]
    public Transform bola;
    private Rigidbody2D rbBola;
    public GameObject fogPanel;
    public int arenaComFog = 1;
    public GameObject[] tetos;

    private int arenaAtual = 0;
    private int acertos = 0;

    // ----------- Pontuação / Recorde -----------
    [Header("Pontuação")]
    public int pontosPartida = 0;
    public int recorde = 0;

    // ----------- Config novo (Jogo Infinito) ------------
    [Header("Jogo Infinito")]
    public float posXMin = -2f;
    public float posXMax = 2f;
    public float offsetAltura = 3f;
    // ----------------------------------------------------

    void Awake()
    {
        spawnersDeMoedas = new CoinSpawner[spawnersDeMoedasGO.Length];
        for (int i = 0; i < spawnersDeMoedasGO.Length; i++)
            spawnersDeMoedas[i] = spawnersDeMoedasGO[i].GetComponent<CoinSpawner>();
    }

    void Start()
    {
        rbBola = bola.GetComponent<Rigidbody2D>();
        rbBola.gravityScale = 3f;

        // Carrega arena e acertos
        if (PlayerPrefs.HasKey("arenaAtual"))
        {
            arenaAtual = PlayerPrefs.GetInt("arenaAtual");
            acertos = PlayerPrefs.GetInt("acertos");
        }
        else
        {
            arenaAtual = 0;
            acertos = 0;
        }

        // Carrega recorde
        if (PlayerPrefs.HasKey("recorde"))
            recorde = PlayerPrefs.GetInt("recorde");
        else
            recorde = 0;

        pontosPartida = 0;

        AtualizarArenas();
    }

    // ======================== JOGO INFINITO =========================

    void TeleportarBolaParaArenaAleatoria()
    {
        int novaArena = Random.Range(0, arenas.Length);
        arenaAtual = novaArena;

        float x = Random.Range(posXMin, posXMax);
        float y = arenas[arenaAtual].transform.position.y + offsetAltura;

        rbBola.linearVelocity = Vector2.zero;
        rbBola.angularVelocity = 0f;

        bola.position = new Vector3(x, y, 0);

        AtualizarArenas();
    }

    // =================================================================

    void DescerArena()
    {
        acertos = 0;
        TeleportarBolaParaArenaAleatoria();
    }

    public void AtivarTetoDaArena(int index)
    {
        if (index >= 0 && index < tetos.Length && tetos[index] != null)
            tetos[index].SetActive(true);
    }

    void AtualizarArenas()
    {
        for (int i = 0; i < arenas.Length; i++)
        {
            bool ativo = (i == arenaAtual);
            arenas[i].SetActive(ativo);

            if (i < spawnersDeMoedas.Length && spawnersDeMoedas[i] != null)
            {
                spawnersDeMoedas[i].gameObject.SetActive(ativo);
                spawnersDeMoedas[i].AtivarSpawner(ativo);
            }
        }

        if (arenaAtual < posicoesCamera.Length)
            mainCamera.transform.position = posicoesCamera[arenaAtual];

        if (arenaAtual < coresArenas.Length)
            mainCamera.backgroundColor = coresArenas[arenaAtual];

        if (fogPanel != null)
            fogPanel.SetActive(arenaAtual == arenaComFog);
    }

    // =================== SISTEMA DE PONTOS / RECORDE ===================

    public void AcertouCesta()
    {
        acertos++;
        pontosPartida++;

        // Atualiza recorde
        if (pontosPartida > recorde)
        {
            recorde = pontosPartida;
            PlayerPrefs.SetInt("recorde", recorde);
            PlayerPrefs.Save();
        }

        // Desce arena se atingiu limite
        if (acertos >= cestasParaDescer[arenaAtual])
            DescerArena();
    }

   

    public void NovoJogo()
    {
        arenaAtual = 0;
        acertos = 0;
        pontosPartida = 0;

        foreach (GameObject teto in tetos)
            if (teto != null) teto.SetActive(false);

        CoinManager.instance.ZerarMoedasDaPartida();
        AtualizarArenas();
    }

    public void VoltarParaMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void SalvarJogo()
    {
        PlayerPrefs.SetInt("arenaAtual", arenaAtual);
        PlayerPrefs.SetInt("acertos", acertos);

        PlayerPrefs.SetInt("recorde", recorde);

        CoinManager.instance?.SalvarPartida();

        PlayerPrefs.Save();
        Debug.Log($"Jogo salvo na arena {arenaAtual} com {acertos} acertos. Recorde: {recorde}");
    }

    public int GetArenaAtualIndex() => arenaAtual;

    public Transform GetArenaAtualTransform()
    {
        if (arenaAtual >= 0 && arenaAtual < arenas.Length)
            return arenas[arenaAtual].transform;
        return null;
    }

    public bool ControlesInvertidos()
    {
        if (arenaAtual >= 0 && arenaAtual < controlesInvertidos.Length)
            return controlesInvertidos[arenaAtual];
        return false;
    }
}
