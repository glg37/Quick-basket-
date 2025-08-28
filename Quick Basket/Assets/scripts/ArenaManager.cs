using UnityEngine;

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

    [Header("Neblina (Fog)")]
    public GameObject fogPanel;   // Arraste o Panel aqui
    public int arenaComFog = 1;   // Índice da arena que terá neblina (0 = primeira, 1 = segunda...)

    private int arenaAtual = 0;
    private int acertos = 0;
    public Transform bola;

    void Start()
    {
        if (PlayerPrefs.HasKey("arenaAtual"))
        {
            CarregarJogo();
        }
        else
        {
            AtualizarArenas();
        }
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
            Debug.Log("Última arena alcançada!");
        }
    }

    void AtualizarArenas()
    {
        // Ativar apenas a arena atual
        for (int i = 0; i < arenas.Length; i++)
            arenas[i].SetActive(i == arenaAtual);

        // Atualizar posição da câmera
        if (arenaAtual < posicoesCamera.Length)
            mainCamera.transform.position = posicoesCamera[arenaAtual];

        // Atualizar cor de fundo
        if (arenaAtual < coresArenas.Length)
            mainCamera.backgroundColor = coresArenas[arenaAtual];

        // Ativar neblina apenas na arena configurada
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
        AtualizarArenas();
        Debug.Log("Jogo carregado!");
    }
}
