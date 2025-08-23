using UnityEngine;

public class ArenaManager : MonoBehaviour
{
    [Header("Arenas")]
    public GameObject[] arenas;
    public int[] cestasParaDescer;

    [Header("Câmera")]
    public Camera mainCamera;
    public Vector3[] posicoesCamera;

    private int arenaAtual = 0;
    private int acertos = 0;

    void Start()
    {
        // Carrega progresso salvo, se existir
        if (PlayerPrefs.HasKey("arenaAtual"))
        {
            arenaAtual = PlayerPrefs.GetInt("arenaAtual", 0);
            acertos = PlayerPrefs.GetInt("acertos", 0);
        }

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
        for (int i = 0; i < arenas.Length; i++)
        {
            arenas[i].SetActive(i == arenaAtual);
        }

        if (arenaAtual < posicoesCamera.Length)
        {
            mainCamera.transform.position = posicoesCamera[arenaAtual];
        }
    }

    // Para o BasketSpawner pegar a arena atual
    public Transform GetArenaAtualTransform()
    {
        return arenas[arenaAtual].transform;
    }

    //  SALVAR PROGRESSO
    public void SalvarProgresso()
    {
        PlayerPrefs.SetInt("arenaAtual", arenaAtual);
        PlayerPrefs.SetInt("acertos", acertos);
        PlayerPrefs.Save();
        Debug.Log("Progresso salvo!");
    }
}
