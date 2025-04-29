// r0b3rT
// 2025-04-25
using System;
using UnityEngine;

public class PlayerRandomShot : MonoBehaviour
{
    // ======================== ENUMS & VARIAVEIS ========================
    [Header("Throw Points")]
    public Transform[] throwPoints;

    [Header("Throw Settings")]
    public GameObject ball;
    public Transform handPosition;
    public float throwForce = 10f;
    public float arcHeight = 2f;
    public float rotationSpeed = 360f;

    [Header("Respawn")]
    public Transform spawnPoint;


    [HideInInspector]
    public bool isBallMoving = false;
    public bool dentroDaZonaDaCesta = false;


    [Header("Probability Settings")]
    [Range(0f, 1f)]
    public float chanceToHitFirstPoint = 0.5f;

    private Animator animator;
    private int chosenIndex;

    public static event Action OnRandomShot;
    public Coroutine ballCoroutine;

    private PlayerMovement playerMovement;

    // ======================== UNITY METODOS ========================

    // inicializa componentes e garante que a bola comece desativada
    void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();

        if (ball != null)
        {
            ball.SetActive(false);
        }
    }

    // verifica tecla de arremesso e inicia a animacao
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            chosenIndex = SelectRandomIndex();
            animator.SetTrigger("Throw");

            if (playerMovement != null)
                playerMovement.canMove = false;
        }
    }

    // ======================== LOGICA ========================

    // escolhe um indice de ponto com base na distancia até o ponto 0 (a cesta)
    private int SelectRandomIndex()
    {
        if (throwPoints.Length == 0) return 0;

        // calcula a distancia entre o jogador e a cesta (ponto 0)
        float distancia = Vector2.Distance(transform.position, throwPoints[0].position);

        // normaliza a distancia: 1 = perto, 10 = longe
        float distanciaNormalizada = Mathf.InverseLerp(1f, 10f, distancia);

        // transforma distancia em chance: quanto mais perto, maior chance
        // se estiver dentro da zona da cesta, chance fixa de 80%
        float chanceDeAcerto = dentroDaZonaDaCesta ? 0.8f : 1f - distanciaNormalizada;

        // sorteia um valor aleatorio
        float randomValue = UnityEngine.Random.value;

        // se caiu dentro da chance, acerta o ponto 0 (cesta)
        if (randomValue < chanceDeAcerto)
        {
            return 0;
        }
        else
        {
            // caso contrário, retorna ponto de erro (elemento aleatório > 0)
            return UnityEngine.Random.Range(1, throwPoints.Length);
        }
    }


    // ======================== ARREMESSO ========================

    // chamado por evento da animacao para arremessar a bola
    public void ThrowBall()
    {
        if (ball != null)
        {
            int index = SelectRandomIndex();

            ball.SetActive(true);
            ball.transform.position = handPosition.position;
            ball.transform.localScale = new Vector3(0.7f, 0.7f, 1f);

            ballCoroutine = StartCoroutine(MoveBallInArc(ball.transform, throwPoints[index].position));

            OnRandomShot?.Invoke();
        }
    }


    public void InterruptShot()
    {
        if (ballCoroutine != null)
        {
            StopCoroutine(ballCoroutine);
            ballCoroutine = null;
            Debug.Log("Movimento interrompido pela função InterruptShot()");
        }

        isBallMoving = false;

        if (ball != null)
        {
            Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
                rb.isKinematic = false;
                rb.gravityScale = 1f;
            }

            // redireciona a bola para o ponto de respawn
            if (spawnPoint != null)
            {
                ball.transform.position = spawnPoint.position;
            }
        }

        if (playerMovement != null)
        {
            playerMovement.OnThrow();       // remove posse da bola
            playerMovement.canMove = true;  // libera movimento
        }
    }


    // move a bola em um arco parabólico com rotacao
    private System.Collections.IEnumerator MoveBallInArc(Transform ballTransform, Vector2 targetPosition)
    {
        // define a posicao inicial da bola
        Vector2 startPosition = ballTransform.position;
        float duration = 1f;
        float elapsedTime = 0f;
        isBallMoving = true;

        while (elapsedTime < duration && isBallMoving)
        {
            // calcula o tempo decorrido e a porcentagem do caminho percorrido
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            // interpolacao linear entre a posicao inicial e a final
            Vector2 currentPos = Vector2.Lerp(startPosition, targetPosition, t);

            // calcula a altura do arco e aplica a funcao de arco
            float height = arcHeight * 4 * (t - t * t);
            ballTransform.position = new Vector3(currentPos.x, currentPos.y + height, ballTransform.position.z);

            // gira a bola no eixo z para simular rotacao
            ballTransform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

            yield return null; // espera o próximo frame
        }

        if (isBallMoving)
        {
            // posiciona a bola exatamente no alvo do arremesso
            ballTransform.position = new Vector3(targetPosition.x, targetPosition.y, ballTransform.position.z);

            if (playerMovement != null)
            {
                playerMovement.OnThrow();       // jogador perde posse da bola
                playerMovement.canMove = true;  // pode se mover novamente
            }

            // ativa física para a bola cair normalmente
            Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.gravityScale = 1f;
                rb.velocity = Vector2.zero; // só pra garantir que não continue com velocidade da interpolação
            }

            // espera 2 segundos e então envia para o ponto de respawn
            yield return new WaitForSeconds(2f);

            if (spawnPoint != null)
            {
                ballTransform.position = spawnPoint.position;
            }
        }

        isBallMoving = false;
    }
}