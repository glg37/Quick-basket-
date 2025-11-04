using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;

    [Header("UI da Moeda")]
    public TMP_Text textoMoedas;

    private int moedasTotais = 0;   // Moedas acumuladas (menu/lojinha)
    private int moedasPartida = 0;  // Moedas da partida atual

    private const string KEY_TOTAL = "MoedasTotais";
    private const string KEY_PARTIDA = "MoedasPartida";

    void Awake()
    {
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
        // Carrega apenas na primeira inicialização
        CarregarMoedas();
        AtualizarUI();
    }

    private void OnSceneLoaded(Scene cena, LoadSceneMode modo)
    {
        // Sempre recarregar o texto da UI quando mudar de cena
        TMP_Text novoTexto = GameObject.FindWithTag("TextoMoeda")?.GetComponent<TMP_Text>();
        if (novoTexto != null)
            textoMoedas = novoTexto;

        // Toda vez que voltar ao MENU ? recarregar PlayerPrefs
        if (cena.name != "Jogo")
        {
            moedasTotais = PlayerPrefs.GetInt(KEY_TOTAL, 0);
            AtualizarUI_Menu();
        }
        else
        {
            AtualizarUI_Jogo();
        }
    }

    // ------------------------------------------------
    // CARREGAR / SALVAR
    // ------------------------------------------------

    private void CarregarMoedas()
    {
        moedasTotais = PlayerPrefs.GetInt(KEY_TOTAL, 0);
        moedasPartida = PlayerPrefs.GetInt(KEY_PARTIDA, 0);
    }

    public void SalvarPartida()
    {
        PlayerPrefs.SetInt(KEY_TOTAL, moedasTotais);
        PlayerPrefs.SetInt(KEY_PARTIDA, moedasPartida);
        PlayerPrefs.Save();
    }

    // ------------------------------------------------
    // ADICIONAR MOEDAS
    // ------------------------------------------------

    public void AdicionarMoeda(int quantidade)
    {
        moedasPartida += quantidade;
        moedasTotais += quantidade;

        PlayerPrefs.SetInt(KEY_TOTAL, moedasTotais);
        PlayerPrefs.Save();

        AtualizarUI_Jogo();
    }

    public void AdicionarMoedaPorAnuncio(int quantidade)
    {
        moedasTotais += quantidade;

        PlayerPrefs.SetInt(KEY_TOTAL, moedasTotais);
        PlayerPrefs.Save();

        AtualizarUI_Menu();
    }

    // ------------------------------------------------
    // DESCONTAR MOEDAS (LOJA)
    // ------------------------------------------------

    public bool TentarGastarMoedas(int quantidade)
    {
        if (moedasTotais < quantidade)
            return false;

        moedasTotais -= quantidade;

        PlayerPrefs.SetInt(KEY_TOTAL, moedasTotais);
        PlayerPrefs.Save();

        AtualizarUI_Menu();
        return true;
    }

    // ------------------------------------------------
    // RESET PARTIDA
    // ------------------------------------------------

    public void ZerarMoedasDaPartida()
    {
        moedasPartida = 0;
        PlayerPrefs.SetInt(KEY_PARTIDA, 0);
        PlayerPrefs.Save();

        AtualizarUI_Jogo();
    }

    // ------------------------------------------------
    // UI
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
