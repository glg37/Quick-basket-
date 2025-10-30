using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;

    [Header("UI do Jogo")]
    public TMP_Text textoMoedas;   // Mostra moedas do jogo atual
    public Image iconeMoeda;

    private int moedasTotais = 0;       // Total acumulado (menu)
    private int moedasJogoAtual = 0;    // Moedas da partida atual

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
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

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene cena, LoadSceneMode modo)
    {
        // Quando a cena de jogo for carregada, tenta reconectar o HUD
        if (cena.name == "Jogo")
        {
            TMP_Text novoTexto = GameObject.FindWithTag("TextoMoeda")?.GetComponent<TMP_Text>();
            if (novoTexto != null)
            {
                textoMoedas = novoTexto;
                AtualizarUI();
            }
        }
    }

    // Adiciona moedas durante o jogo
    public void AdicionarMoeda(int quantidade)
    {
        moedasJogoAtual += quantidade;
        moedasTotais += quantidade;

        PlayerPrefs.SetInt("MoedasTotais", moedasTotais);
        PlayerPrefs.Save();

        AtualizarUI();
    }

    // Adiciona moedas no menu (por anúncio ou loja)
    public void AdicionarMoedaPorAnuncio(int quantidade)
    {
        moedasTotais += quantidade;
        PlayerPrefs.SetInt("MoedasTotais", moedasTotais);
        PlayerPrefs.Save();
        AtualizarUI();
    }

    //  Zera apenas moedas da partida atual
    public void ZerarMoedasDoJogo()
    {
        moedasJogoAtual = 0;
        AtualizarUI();
        Debug.Log("Moedas da partida zeradas!");
    }

    // Atualiza UI do HUD
    public void AtualizarUI()
    {
        if (textoMoedas != null)
            textoMoedas.text = moedasJogoAtual.ToString();

        if (iconeMoeda != null)
            iconeMoeda.enabled = true;
    }

    // Getters
    public int GetMoedasTotais() => moedasTotais;
    public int GetMoedasDoJogoAtual() => moedasJogoAtual;
}
