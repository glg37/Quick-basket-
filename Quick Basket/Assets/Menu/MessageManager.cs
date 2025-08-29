using UnityEngine;
using TMPro;
using System.Collections;

public class MessageManager : MonoBehaviour
{
    public static MessageManager Instance; 
    public TMP_Text mensagemCesta;
    public float tempoAtivo = 1.5f;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        if (mensagemCesta != null)
            mensagemCesta.gameObject.SetActive(false);
    }

    public void MostrarMensagem()
    {
        StartCoroutine(MostrarCoroutine());
    }

    private IEnumerator MostrarCoroutine()
    {
        mensagemCesta.gameObject.SetActive(true);
        yield return new WaitForSeconds(tempoAtivo);
        mensagemCesta.gameObject.SetActive(false);
    }
}
