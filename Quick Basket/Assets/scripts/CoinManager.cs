using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;

    [Header("UI da Moeda")]
    public TMP_Text textoMoedas;

    private int moedasTotais = 0;   // Moedas acumuladas (menu)
    private int moedasPartida = 0;  // Moedas da partida atual

    void Awake()
    {
        // Singleton
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        // Carrega moedas salvas
        moedasTotais = PlayerPrefs.GetInt("MoedasTotais", 0);
        moedasPartida = PlayerPrefs.GetInt("MoedasPartida", 0);
        AtualizarUI();
    }

    private void OnSceneLoaded(Scene cena, LoadSceneMode modo)
    {
        // Atualiza referência do texto de moedas
        TMP_Text novoTexto = GameObject.FindWithTag("TextoMoeda")?.GetComponent<TMP_Text>();
        if (novoTexto != null)
        {
            textoMoedas = novoTexto;
            AtualizarUI();
        }

        // Atualiza UI conforme a cena
        if (cena.name == "Jogo")
            AtualizarUI_Jogo();
        else
            AtualizarUI_Menu();
    }

    // ------------------------------------------------
    // MÉTODOS DE ADIÇÃO DE MOEDAS
    // ------------------------------------------------

    // Moedas coletadas jogando
    public void AdicionarMoeda(int quantidade)
    {
        moedasPartida += quantidade;
        AtualizarUI_Jogo();
    }

    // Moedas ganhas assistindo anúncio (menu)
    public void AdicionarMoedaPorAnuncio(int quantidade)
    {
        moedasTotais += quantidade;
        PlayerPrefs.SetInt("MoedasTotais", moedasTotais);
        PlayerPrefs.Save();
        AtualizarUI_Menu();
        Debug.Log($"Ganhou {quantidade} moedas no menu (por anúncio)");
    }

    // Quando termina a partida
    public void AdicionarMoedaAoTotal()
    {
        moedasTotais += moedasPartida;
        moedasPartida = 0;
        PlayerPrefs.SetInt("MoedasTotais", moedasTotais);
        PlayerPrefs.SetInt("MoedasPartida", 0);
        PlayerPrefs.Save();
        AtualizarUI_Menu();
    }

    // ------------------------------------------------
    // CONTROLE DE SALVAR / RESETAR
    // ------------------------------------------------

    public void SalvarPartida()
    {
        PlayerPrefs.SetInt("MoedasPartida", moedasPartida);
        PlayerPrefs.SetInt("MoedasTotais", moedasTotais);
        PlayerPrefs.Save();
    }

    public void ZerarMoedasDaPartida()
    {
        moedasPartida = 0;
        PlayerPrefs.SetInt("MoedasPartida", 0);
        PlayerPrefs.Save();
        AtualizarUI_Jogo();
    }

    // ------------------------------------------------
    // ATUALIZAÇÃO DE UI
    // ------------------------------------------------

    void AtualizarUI()
    {
        if (textoMoedas == null) return;

        if (SceneManager.GetActiveScene().name == "Jogo")
            textoMoedas.text = moedasPartida.ToString();
        else
            textoMoedas.text = moedasTotais.ToString();
    }

    void AtualizarUI_Jogo()
    {
        if (textoMoedas != null)
            textoMoedas.text = moedasPartida.ToString();
    }

    void AtualizarUI_Menu()
    {
        if (textoMoedas != null)
            textoMoedas.text = moedasTotais.ToString();
    }

    // ------------------------------------------------
    // GETTERS
    // ------------------------------------------------

    public int GetMoedasTotais() => moedasTotais;
    public int GetMoedasPartida() => moedasPartida;

    // ------------------------------------------------
    // GARANTIR INSTÂNCIA NO MENU
    // ------------------------------------------------
    public static void GarantirInstancia()
    {
        if (instance == null)
        {
            GameObject go = new GameObject("CoinManager");
            go.AddComponent<CoinManager>();
        }
    }
}
