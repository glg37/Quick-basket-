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
    public Color[] coresArenas; // cada elemento é a cor de uma arena

    private int arenaAtual = 0;
    private int acertos = 0;

    void Start()
    {
        // Não carrega save automaticamente
        AtualizarArenas();
    }

    // Chamado pelo MenuController quando clicar em Continuar
    public void CarregarProgresso()
    {
        if (PlayerPrefs.HasKey("arenaAtual"))
        {
            arenaAtual = PlayerPrefs.GetInt("arenaAtual", 0);
            acertos = PlayerPrefs.GetInt("acertos", 0);
            AtualizarArenas();
            Debug.Log("Progresso carregado! Arena: " + arenaAtual + " | Acertos: " + acertos);
        }
    }

    // Chamado pelo MenuController quando clicar em Jogar (novo jogo)
    public void NovoJogo()
    {
        arenaAtual = 0;
        acertos = 0;
        AtualizarArenas();
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
        // Ativa apenas a arena atual
        for (int i = 0; i < arenas.Length; i++)
        {
            arenas[i].SetActive(i == arenaAtual);
        }

        // Move a câmera
        if (arenaAtual < posicoesCamera.Length)
        {
            mainCamera.transform.position = posicoesCamera[arenaAtual];
        }

        // Aplica a cor da arena
        if (arenaAtual < coresArenas.Length)
        {
            mainCamera.backgroundColor = coresArenas[arenaAtual];
        }
    }

    // Para o BasketSpawner pegar a arena atual
    public Transform GetArenaAtualTransform()
    {
        return arenas[arenaAtual].transform;
    }

    // Salvar progresso
    public void SalvarProgresso()
    {
        PlayerPrefs.SetInt("arenaAtual", arenaAtual);
        PlayerPrefs.SetInt("acertos", acertos);
        PlayerPrefs.Save();
        Debug.Log("Progresso salvo! Arena: " + arenaAtual + " | Acertos: " + acertos);
    }
}
