using UnityEngine;
using System.Collections;

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
    public GameObject[] tetos; // um teto para cada arena

    private int arenaAtual = 0;
    private int acertos = 0;

    void Start()
    {
        rbBola = bola.GetComponent<Rigidbody2D>();

        if (PlayerPrefs.HasKey("arenaAtual"))
            CarregarJogo();
        else
            AtualizarArenas();
    }

    void DescerArena()
    {
        if (arenaAtual + 1 < arenas.Length)
        {
            arenaAtual++;
            acertos = 0;
            AtualizarArenas();

            // a ativação do teto será feita via trigger ou função externa
        }
        else
        {
            Debug.Log("Última arena alcançada!");
        }
    }

    // função pública para ativar o teto de uma arena específica
    public void AtivarTetoDaArena(int index)
    {
        if (index >= 0 && index < tetos.Length && tetos[index] != null)
            tetos[index].SetActive(true);
    }

    void AtualizarArenas()
    {
        // ativa só a arena atual
        for (int i = 0; i < arenas.Length; i++)
            arenas[i].SetActive(i == arenaAtual);

        // posiciona câmera
        if (arenaAtual < posicoesCamera.Length)
            mainCamera.transform.position = posicoesCamera[arenaAtual];

        // cor de fundo
        if (arenaAtual < coresArenas.Length)
            mainCamera.backgroundColor = coresArenas[arenaAtual];

        // fog / neblina
        if (fogPanel != null)
            fogPanel.SetActive(arenaAtual == arenaComFog);

        // gravidade da bola
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

        // desativa todos os tetos
        foreach (GameObject teto in tetos)
            if (teto != null) teto.SetActive(false);

        AtualizarArenas();
        SalvarJogo();
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
