using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CoinDisplayMenu : MonoBehaviour
{
    [Header("Referências da UI")]
    public TMP_Text textoMoedasMenu;   // Texto da moeda no HUD
    public Image iconeMoeda;           // Ícone da moeda

    [Header("Configuração")]
    public bool atualizarEmTempoReal = true; // Atualiza automaticamente se as moedas mudarem

    private int moedasAtuais = -1;

    void OnEnable()
    {
        AtualizarUI();
    }

    void Start()
    {
        AtualizarUI();
    }

    void Update()
    {
        if (atualizarEmTempoReal)
        {
            int moedasSalvas = PlayerPrefs.GetInt("MoedasTotais", 0);

            if (moedasSalvas != moedasAtuais)
            {
                moedasAtuais = moedasSalvas;
                AtualizarUI();
            }
        }
    }

    public void AtualizarUI()
    {
        moedasAtuais = PlayerPrefs.GetInt("MoedasTotais", 0);

        if (textoMoedasMenu != null)
            textoMoedasMenu.text = moedasAtuais.ToString();

        if (iconeMoeda != null)
            iconeMoeda.enabled = true;
    }
}
