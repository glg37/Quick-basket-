using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CoinDisplayMenu : MonoBehaviour
{
    [Header("Refer�ncias da UI")]
    public TMP_Text textoMoedasMenu;   // Texto da moeda no HUD
    public Image iconeMoeda;           // �cone da moeda

    [Header("Configura��o")]
    public bool atualizarEmTempoReal = true; // Atualiza automaticamente se as moedas mudarem

    private int moedasAtuais = -1;

    void OnEnable()
    {
        // Sempre atualiza a UI ao ativar a cena ou voltar do menu
        AtualizarUI();
    }

    void Start()
    {
        // Inicializa a UI
        AtualizarUI();
    }

    void Update()
    {
        if (atualizarEmTempoReal)
        {
            int moedasSalvas = CoinManager.instance != null ?
                               CoinManager.instance.GetMoedasTotais() :
                               PlayerPrefs.GetInt("MoedasTotais", 0);

            if (moedasSalvas != moedasAtuais)
            {
                moedasAtuais = moedasSalvas;
                AtualizarUI();
            }
        }
    }

    public void AtualizarUI()
    {
        // Pega o total de moedas do CoinManager, se existir, sen�o pega do PlayerPrefs
        if (CoinManager.instance != null)
            moedasAtuais = CoinManager.instance.GetMoedasTotais();
        else
            moedasAtuais = PlayerPrefs.GetInt("MoedasTotais", 0);

        // Atualiza o texto da moeda
        if (textoMoedasMenu != null)
            textoMoedasMenu.text = moedasAtuais.ToString();

        // Garante que o �cone da moeda esteja vis�vel
        if (iconeMoeda != null)
            iconeMoeda.enabled = true;
    }
}
