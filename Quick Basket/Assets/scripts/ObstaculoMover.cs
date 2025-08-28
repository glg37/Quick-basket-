using UnityEngine;

public class ObstaculoMover : MonoBehaviour
{
    public float limiteEsquerda = -2f;
    public float limiteDireita = 2f;
    public float velocidade = 2f;

    private Vector3 posInicial;
    private bool indoDireita = true;

    void Start()
    {
        posInicial = transform.localPosition; 
    }

    void Update()
    {
        Vector3 localPos = transform.localPosition;
        float step = velocidade * Time.deltaTime;

        if (indoDireita)
            localPos.x += step;
        else
            localPos.x -= step;

       
        if (localPos.x > posInicial.x + limiteDireita)
            indoDireita = false;
        else if (localPos.x < posInicial.x + limiteEsquerda)
            indoDireita = true;

        transform.localPosition = localPos;
    }
}
