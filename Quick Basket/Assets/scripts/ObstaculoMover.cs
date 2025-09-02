using UnityEngine;

public class ObstaculoMover : MonoBehaviour
{
    public float velocidade = 2f;
    private float limiteEsquerda;
    private float limiteDireita;
    private bool indoDireita = true;

    void Update()
    {
        Vector3 pos = transform.localPosition; // usa localPosition porque é filho da cesta
        float step = velocidade * Time.deltaTime;

        if (indoDireita)
            pos.x += step;
        else
            pos.x -= step;

        // Mantém dentro dos limites relativos à cesta
        if (pos.x > limiteDireita)
        {
            pos.x = limiteDireita;
            indoDireita = false;
        }
        else if (pos.x < limiteEsquerda)
        {
            pos.x = limiteEsquerda;
            indoDireita = true;
        }

        transform.localPosition = pos;
    }

    // Define limites mínimos e máximos relativos à cesta
    public void SetLimites(float minimoX, float maximoX)
    {
        limiteEsquerda = minimoX;
        limiteDireita = maximoX;
    }
}
