using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CoinDisplayMenu : MonoBehaviour
{
    [Header("Refer�ncias da UI")]
    public TMP_Text textoMoedasMenu;   // Mostra a quantidade de moedas
    public Image iconeMoeda;            // �cone da moeda

    [Header("Configura��o")]
    public bool atualizarEmTempoReal = true; // Atualiza sempre (para menus din�micos)

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
            iconeMoeda.enabled = true; // Garante que o �cone aparece
    }
}
