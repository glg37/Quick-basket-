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
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Mantém entre cenas
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        //  Carrega o total salvo
        moedasTotais = PlayerPrefs.GetInt("MoedasTotais", 0);
        AtualizarUI();
    }

    public void AdicionarMoeda(int quantidade)
    {
        moedasTotais += quantidade;

        //  Salva o novo total
        PlayerPrefs.SetInt("MoedasTotais", moedasTotais);
        PlayerPrefs.Save();

        AtualizarUI();
    }

    public void AdicionarMoedaPorAnuncio(int quantidade)
    {
        AdicionarMoeda(quantidade);
        Debug.Log("Ganhou " + quantidade + " moedas pelo anúncio!");
    }

    void AtualizarUI()
    {
        if (textoMoedas != null)
            textoMoedas.text = moedasTotais.ToString();
    }

    public int GetMoedasTotais()
    {
        return moedasTotais;
    }
}
