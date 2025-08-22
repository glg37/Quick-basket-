using UnityEngine;
using System.Collections;

public class ArenaManager : MonoBehaviour
{
    [Header("Arenas")]
    public GameObject[] arenas;
    public int[] cestasParaDescer;

    [Header("C�mera")]
    public Camera mainCamera;
    public Vector3[] posicoesCamera; // posi��es para cada arena

    private int arenaAtual = 0;
    private int acertos = 0;

    void Start()
    {
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
            Debug.Log("�ltima arena alcan�ada!");
        }
    }

    void AtualizarArenas()
    {
        for (int i = 0; i < arenas.Length; i++)
        {
            arenas[i].SetActive(i == arenaAtual);
        }

        // Move a c�mera imediatamente para a posi��o da arena atual
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
}
