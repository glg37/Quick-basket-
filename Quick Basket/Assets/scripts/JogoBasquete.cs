using UnityEngine;

public class JogoBasquete : MonoBehaviour
{
    public Transform[] novasPosicoes;  // Posições para a cesta
    public GameObject bola;  // Referência à bola no jogo
    public float tempoEntreMudancas = 2f;  // Tempo entre uma mudança de posição e outra

    private bool bolaAcertou = false;  // Flag para verificar se a bola acertou a cesta

    private void Update()
    {
        // Se a bola acertou a cesta e a flag não foi acionada
        if (bolaAcertou)
        {
            // Espera um pouco e depois muda a posição da cesta
            Invoke("MudarPosicao", tempoEntreMudancas);
            bolaAcertou = false;  // Reseta a flag para esperar o próximo acerto
        }
    }

    // Método que verifica se a bola acertou a cesta
    private void OnTriggerEnter(Collider other)
    {
        // Se a bola entrou na área da cesta
        if (other.gameObject == bola)
        {
            bolaAcertou = true;  // Marca que a bola acertou a cesta
        }
    }

    // Método para mudar a posição da cesta
    private void MudarPosicao()
    {
        // Escolhe uma nova posição aleatória para a cesta
        int posicaoAleatoria = Random.Range(0, novasPosicoes.Length);
        transform.position = novasPosicoes[posicaoAleatoria].position;
    }
}
