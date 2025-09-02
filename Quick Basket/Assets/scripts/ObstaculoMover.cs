using UnityEngine;

public class ObstaculoMover : MonoBehaviour
{
    public float velocidade = 2f;
    private float limiteEsquerda;
    private float limiteDireita;
    private bool indoDireita = true;

    void Update()
    {
        Vector3 pos = transform.localPosition; // usa localPosition porque � filho da cesta
        float step = velocidade * Time.deltaTime;

        if (indoDireita)
            pos.x += step;
        else
            pos.x -= step;

        // Mant�m dentro dos limites relativos � cesta
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

    // Define limites m�nimos e m�ximos relativos � cesta
    public void SetLimites(float minimoX, float maximoX)
    {
        limiteEsquerda = minimoX;
        limiteDireita = maximoX;
    }
}
