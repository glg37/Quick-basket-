using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

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

    // ----------- POWER-UP DOBRAR PONTOS -----------
    private bool powerUpAtivo = false; // se está ativo temporariamente
    private float multiplicador = 1f;  // multiplicador atual
    private bool powerUpComprado = false; // se o jogador comprou o power-up

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
        arenaAtual = PlayerPrefs.GetInt("arenaAtual", 0);
        acertos = PlayerPrefs.GetInt("acertos", 0);

        // Carrega recorde
        recorde = PlayerPrefs.GetInt("recorde", 0);

        pontosPartida = 0;

        // Verifica se jogador comprou power-up
        powerUpComprado = PlayerPrefs.GetInt("PowerUpDoublePoints", 0) == 1;

        AtualizarArenas();
    }

    // ======================== JOGO INFINITO =========================

    void TeleportarBolaParaArenaAleatoria()
    {
        int novaArena = Random.Range(0, arenas.Length);
        arenaAtual = novaArena;

        float x = Random.Range(posXMin, posXMax);
        float y = arenas[arenaAtual].transform.position.y + offsetAltura;

        rbBola.linearVelocity = Vector2.zero;  // antes: velocity
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

    // =================== SISTEMA DE PONTOS / POWER-UP ===================

    public void AcertouCesta()
    {
        acertos++;

        int pontosGanhos = Mathf.RoundToInt(1 * multiplicador);
        pontosPartida += pontosGanhos;

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

    // Método público para ativar X2 temporário
    public void AtivarX2(float duracao)
    {
        if (powerUpComprado)
        {
            if (!powerUpAtivo)
                StartCoroutine(X2Coroutine(duracao));
            else
                Debug.Log("Power-up X2 já está ativo!");
        }
        else
        {
            Debug.Log("Power-up X2 não comprado!");
        }
    }

    private IEnumerator X2Coroutine(float duracao)
    {
        powerUpAtivo = true;
        multiplicador = 2f;
        Debug.Log("Power-up X2 ATIVADO!");

        yield return new WaitForSeconds(duracao);

        multiplicador = 1f;
        powerUpAtivo = false;
        Debug.Log("Power-up X2 DESATIVADO!");
    }

    // ======================== OUTROS MÉTODOS =========================

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
