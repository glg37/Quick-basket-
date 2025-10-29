using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CoinDisplayMenu : MonoBehaviour
{
    [Header("Referências da UI")]
    public TMP_Text textoMoedasMenu;   // Mostra a quantidade de moedas
    public Image iconeMoeda;            // Ícone da moeda

    [Header("Configuração")]
    public bool atualizarEmTempoReal = true; // Atualiza sempre (para menus dinâmicos)

    private int moedasAtuais;

    void Start()
    {
        AtualizarUI();
    }

    void Update()
    {
        // Atualiza automaticamente se as moedas mudarem
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

    void AtualizarUI()
    {
        moedasAtuais = PlayerPrefs.GetInt("MoedasTotais", 0);

        if (textoMoedasMenu != null)
            textoMoedasMenu.text = moedasAtuais.ToString();

        if (iconeMoeda != null)
            iconeMoeda.enabled = true; // Garante que o ícone aparece
    }
}
