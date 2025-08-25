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

    public void NovoJogo()
    {
        arenaAtual = 0;
        acertos = 0;
        AtualizarArenas();
        SalvarJogo();
    }

    public void AcertouCesta()
    {
        acertos++;

        if (acertos >= cestasParaDescer[arenaAtual])
        {
            DescerArena();
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
        for (int i = 0; i < arenas.Length; i++)
        {
            arenas[i].SetActive(i == arenaAtual);
        }

        if (arenaAtual < posicoesCamera.Length)
            mainCamera.transform.position = posicoesCamera[arenaAtual];

        if (arenaAtual < coresArenas.Length)
            mainCamera.backgroundColor = coresArenas[arenaAtual];
    }

    public Transform GetArenaAtualTransform()
    {
        return arenas[arenaAtual].transform;
    }

    
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
