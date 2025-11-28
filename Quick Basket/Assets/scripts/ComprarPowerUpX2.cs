using UnityEngine;

public class ComprarPowerUpX2 : MonoBehaviour
{
    public int preco = 10;
    private const string KEY = "PowerUpDoublePoints";

    // ESTA função aparece no OnClick()
    public void Comprar()
    {
        if (PlayerPrefs.GetInt(KEY, 0) == 1)
        {
            Debug.Log("Você já comprou o x2!");
            return;
        }

        bool comprado = CoinManager.instance.TentarGastarMoedas(preco);

        if (comprado)
        {
            PlayerPrefs.SetInt(KEY, 1);
            PlayerPrefs.Save();
            Debug.Log("POWER-UP X2 comprado!");
        }
        else
        {
            Debug.Log("Moedas insuficientes!");
        }
    }
}
