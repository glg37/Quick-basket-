using UnityEngine;
using UnityEngine.SceneManagement;

public class ArenaManager : MonoBehaviour
{
    [Header("Arenas")]
    public GameObject[] arenas;
    public int[] cestasParaDescer;

    [Header("Câmera")]
    public Camera mainCamera;
    public Vector3[] posicoesCamera;

    [Header("Cores das Arenas")]
    public Color[] coresArenas;

    [Header("Bola")]
    public Transform bola;
    private Rigidbody2D rbBola;

    [Header("Configuração de Gravidade por Arena")]
    public float[] gravidadePorArena;
    public float gravidadePadrao = 1f;

    [Header("Fog / Neblina")]
    public GameObject fogPanel;
    public int arenaComFog = 1;

    [Header("Teto das Arenas")]
    public GameObject[] tetos;

    [Header("UI")]
    public GameObject painelVitoria; // seu painel de vitória

    private int arenaAtual = 0;
    private int acertos = 0;

    void Start()
    {
        rbBola = bola.GetComponent<Rigidbody2D>();
        if (PlayerPrefs.HasKey("arenaAtual")) CarregarJogo();
        else AtualizarArenas();

        if (painelVitoria != null)
            painelVitoria.SetActive(false); // garante que esteja desativado no começo
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
            // Última arena completada  Vitória
            Debug.Log("Vitória!");
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
            arenas[i].SetActive(i == arenaAtual);

        if (arenaAtual < posicoesCamera.Length)
            mainCamera.transform.position = posicoesCamera[arenaAtual];

        if (arenaAtual < coresArenas.Length)
            mainCamera.backgroundColor = coresArenas[arenaAtual];

        if (fogPanel != null)
            fogPanel.SetActive(arenaAtual == arenaComFog);

        if (arenaAtual < gravidadePorArena.Length)
            rbBola.gravityScale = gravidadePorArena[arenaAtual];
        else
            rbBola.gravityScale = gravidadePadrao;
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

        AtualizarArenas();
        SalvarJogo();

        if (painelVitoria != null)
            painelVitoria.SetActive(false); 
    }

    public void VoltarParaMenu()
    {
        SceneManager.LoadScene("Menu"); 
    }

    public Transform GetArenaAtualTransform() => arenas[arenaAtual].transform;
    public int GetArenaAtualIndex() => arenaAtual;

    public void SalvarJogo()
    {
        PlayerPrefs.SetInt("arenaAtual", arenaAtual);
        PlayerPrefs.SetInt("acertos", acertos);
        PlayerPrefs.SetFloat("bolaX", bola.position.x);
        PlayerPrefs.SetFloat("bolaY", bola.position.y);
        PlayerPrefs.SetFloat("bolaZ", bola.position.z);
        PlayerPrefs.Save();
        Debug.Log("Jogo salvo!");
    }

    public void CarregarJogo()
    {
        arenaAtual = PlayerPrefs.GetInt("arenaAtual", 0);
        acertos = PlayerPrefs.GetInt("acertos", 0);
        float x = PlayerPrefs.GetFloat("bolaX", 0f);
        float y = PlayerPrefs.GetFloat("bolaY", 0f);
        float z = PlayerPrefs.GetFloat("bolaZ", 0f);
        bola.position = new Vector3(x, y, z);

        foreach (GameObject teto in tetos)
            if (teto != null) teto.SetActive(false);

        for (int i = 0; i < arenaAtual && i < tetos.Length; i++)
            if (tetos[i] != null) tetos[i].SetActive(true);

        AtualizarArenas();
        Debug.Log("Jogo carregado!");

        if (PlayerPrefs.HasKey("tempoRestante"))
        {
            Timer timer = FindFirstObjectByType<Timer>();
            if (timer != null)
                timer.SetTempoRestante(PlayerPrefs.GetFloat("tempoRestante"));
        }
    }
}
