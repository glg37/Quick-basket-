using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;

    [Header("UI da Moeda")]
    public TMP_Text textoMoedas;   
    public Image iconeMoeda;

    private int moedasTotais = 0;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        AtualizarUI();
    }

    public void AdicionarMoeda(int quantidade)
    {
        moedasTotais += quantidade;
        AtualizarUI();
    }

    void AtualizarUI()
    {
        if (textoMoedas != null)
            textoMoedas.text = moedasTotais.ToString();
    }
}
