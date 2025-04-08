using UnityEngine;

public class JogoBasquete : MonoBehaviour
{
    public Transform[] novasPosicoes;  // Posi��es para a cesta
    public GameObject bola;  // Refer�ncia � bola no jogo
    public float tempoEntreMudancas = 2f;  // Tempo entre uma mudan�a de posi��o e outra

    private bool bolaAcertou = false;  // Flag para verificar se a bola acertou a cesta

    private void Update()
    {
        // Se a bola acertou a cesta e a flag n�o foi acionada
        if (bolaAcertou)
        {
            // Espera um pouco e depois muda a posi��o da cesta
            Invoke("MudarPosicao", tempoEntreMudancas);
            bolaAcertou = false;  // Reseta a flag para esperar o pr�ximo acerto
        }
    }

    // M�todo que verifica se a bola acertou a cesta
    private void OnTriggerEnter(Collider other)
    {
        // Se a bola entrou na �rea da cesta
        if (other.gameObject == bola)
        {
            bolaAcertou = true;  // Marca que a bola acertou a cesta
        }
    }

    // M�todo para mudar a posi��o da cesta
    private void MudarPosicao()
    {
        // Escolhe uma nova posi��o aleat�ria para a cesta
        int posicaoAleatoria = Random.Range(0, novasPosicoes.Length);
        transform.position = novasPosicoes[posicaoAleatoria].position;
    }
}
