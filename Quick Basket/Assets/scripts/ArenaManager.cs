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
        AtualizarArenas();
    }

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
}
