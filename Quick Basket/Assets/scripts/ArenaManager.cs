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
    public float tempoMovimentoCamera = 1f; // tempo que a c�mera leva para descer

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
            StartCoroutine(DescerArenaCoroutine());
        }
    }

    IEnumerator DescerArenaCoroutine()
    {
        if (arenaAtual + 1 < arenas.Length)
        {
            arenaAtual++;
            acertos = 0;

            AtualizarArenas();

            // Move a c�mera suavemente
            Vector3 posInicial = mainCamera.transform.position;
            Vector3 posFinal = posicoesCamera[arenaAtual];
            float tempo = 0f;

            while (tempo < tempoMovimentoCamera)
            {
                tempo += Time.deltaTime;
                mainCamera.transform.position = Vector3.Lerp(posInicial, posFinal, tempo / tempoMovimentoCamera);
                yield return null;
            }

            mainCamera.transform.position = posFinal; // garante a posi��o final exata
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
    }

    // Para o BasketSpawner pegar a arena atual
    public Transform GetArenaAtualTransform()
    {
        return arenas[arenaAtual].transform;
    }
}
