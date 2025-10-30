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
    public GameObject painelVitoria;

    private int arenaAtual = 0;
    private int acertos = 0;

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

        AtualizarArenas();

        if (painelVitoria != null)
            painelVitoria.SetActive(false);
    }

    void DescerArena()
    {
        if (arenaAtual + 1 < arenas.Length)
        {
            arenaAtual++;
            acertos = 0;
            AtualizarArenas();
        }
        else
        {
            if (painelVitoria != null)
                painelVitoria.SetActive(true);
        }
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

    public void AcertouCesta()
    {
        acertos++;
        if (acertos >= cestasParaDescer[arenaAtual])
            DescerArena();
    }

    public void NovoJogo()
    {
        arenaAtual = 0;
        acertos = 0;

        foreach (GameObject teto in tetos)
            if (teto != null) teto.SetActive(false);

        CoinManager.instance.ZerarMoedasDaPartida();

        AtualizarArenas();

        if (painelVitoria != null)
            painelVitoria.SetActive(false);
    }

    public void VoltarParaMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void SalvarJogo()
    {
        PlayerPrefs.SetInt("arenaAtual", arenaAtual);
        PlayerPrefs.SetInt("acertos", acertos);
        PlayerPrefs.Save();
        CoinManager.instance.SalvarPartida();
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
