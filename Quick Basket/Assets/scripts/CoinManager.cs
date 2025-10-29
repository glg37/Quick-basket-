using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded; //  Detecta mudança de cena
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        moedasTotais = PlayerPrefs.GetInt("MoedasTotais", 0);
        AtualizarUI();
    }

    private void OnSceneLoaded(Scene cena, LoadSceneMode modo)
    {
        //  Tenta encontrar o texto da moeda novamente na nova cena
        if (textoMoedas == null)
        {
            TMP_Text novoTexto = GameObject.FindWithTag("TextoMoeda")?.GetComponent<TMP_Text>();
            if (novoTexto != null)
            {
                textoMoedas = novoTexto;
                AtualizarUI();
            }
        }
    }

    public void AdicionarMoeda(int quantidade)
    {
        moedasTotais += quantidade;
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
    public void ZerarMoedas()
    {
        moedasTotais = 0;
        PlayerPrefs.SetInt("MoedasTotais", moedasTotais);
        PlayerPrefs.Save();
        AtualizarUI();
    }
}
