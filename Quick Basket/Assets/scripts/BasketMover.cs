using UnityEngine;

public class BasketMover : MonoBehaviour
{
    public float velocidade = 2f;
    public float limiteEsquerda = -3f;
    public float limiteDireita = 3f;

    private int direcao = 1;

    void Update()
    {
        transform.Translate(Vector2.right * direcao * velocidade * Time.deltaTime);

        if (transform.position.x >= limiteDireita)
            direcao = -1;
        else if (transform.position.x <= limiteEsquerda)
            direcao = 1;
    }
}
