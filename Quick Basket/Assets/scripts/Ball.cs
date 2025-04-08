using UnityEngine;
using TMPro;  // Importar o TextMeshPro para exibição de pontos

public class Ball : MonoBehaviour
{
    public Rigidbody2D rbBola;  // Referência ao Rigidbody2D da bola
    public float forcaMinima = 5f;  // Força mínima para o lançamento
    public float forcaMaxima = 20f;  // Força máxima para o lançamento
    public Vector2 direcionamento = new Vector2(1f, 1f);  // Direção do lançamento

    public TMP_Text textoPontos;  // Texto para exibir a pontuação
    private int pontos = 0;  // Contador de pontos

    private float tempoPressionado = 0f;  // Tempo que o botão está pressionado

    void Start()
    {
        // Certifica-se de que a bola possui um Rigidbody2D
        if (rbBola == null)
        {
            rbBola = GetComponent<Rigidbody2D>();
        }

        // Certifica-se de que o texto começa com 0 pontos
        if (textoPontos != null)
        {
            textoPontos.text = "Pontos: " + pontos.ToString();
        }
    }

    // Este método será chamado quando o botão for pressionado
    public void IniciarPressionamento()
    {
        tempoPressionado = 0f;  // Reseta o tempo de pressionamento quando o botão é pressionado
    }

    // Este método será chamado quando o botão for liberado
    public void FinalizarPressionamento()
    {
        LancarBola();  // Lança a bola quando o botão é solto
    }

    void Update()
    {
        // Se o botão estiver sendo pressionado, aumenta o tempo pressionado
        if (tempoPressionado >= 0f)
        {
            tempoPressionado += Time.deltaTime;  // Aumenta o tempo pressionado
        }
    }

    // Método que será chamado para lançar a bola
    public void LancarBola()
    {
        // Reseta a velocidade da bola e aplica a gravidade
        rbBola.linearVelocity = Vector2.zero;
        rbBola.gravityScale = 1f;  // Ativa a gravidade

        // Calcula a força com base no tempo pressionado
        float forcaLancamento = Mathf.Lerp(forcaMinima, forcaMaxima, tempoPressionado);

        // Aplica a força no Rigidbody2D para lançar a bola
        rbBola.AddForce(direcionamento.normalized * forcaLancamento, ForceMode2D.Impulse);

        // Incrementa os pontos
        IncrementarPontos();
    }

    // Método para incrementar os pontos e atualizar o texto
    private void IncrementarPontos()
    {
        pontos++;  // Incrementa a pontuação

        // Atualiza o texto do contador de pontos
        if (textoPontos != null)
        {
            textoPontos.text = "Pontos: " + pontos.ToString();
        }
    }

    // Método chamado quando a bola colide com um trigger (detectar colisão)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ZonaPontuacao"))  // Verifique se a colisão é com a zona de pontuação
        {
            IncrementarPontos();  // Incrementa a pontuação sempre que a bola entra na zona de pontuação
        }
    }
}
