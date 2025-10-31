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
        moedasTotais = PlayerPrefs.GetInt("MoedasTotais", 0);
        moedasPartida = PlayerPrefs.GetInt("MoedasPartida", 0);
        AtualizarUI();
    }

    private void OnSceneLoaded(Scene cena, LoadSceneMode modo)
    {
        TMP_Text novoTexto = GameObject.FindWithTag("TextoMoeda")?.GetComponent<TMP_Text>();
        if (novoTexto != null)
        {
            textoMoedas = novoTexto;
            AtualizarUI();
        }

        if (cena.name == "Jogo")
            AtualizarUI_Jogo();
        else
            AtualizarUI_Menu();
    }

    // ------------------------------------------------
    // MÉTODOS DE ADIÇÃO DE MOEDAS
    // ------------------------------------------------

    public void AdicionarMoeda(int quantidade)
    {
        moedasPartida += quantidade;

        // Agora cada moeda coletada também aumenta o total
        moedasTotais += quantidade;
        PlayerPrefs.SetInt("MoedasTotais", moedasTotais);

        AtualizarUI_Jogo();
    }

    public void AdicionarMoedaPorAnuncio(int quantidade)
    {
        moedasTotais += quantidade;
        PlayerPrefs.SetInt("MoedasTotais", moedasTotais);
        PlayerPrefs.Save();
        AtualizarUI_Menu();
        Debug.Log($"Ganhou {quantidade} moedas no menu (por anúncio)");
    }

    // ------------------------------------------------
    // SALVAR / RESETAR
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
