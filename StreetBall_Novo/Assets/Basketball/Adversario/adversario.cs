using UnityEngine;

public class AdversarioAI : MonoBehaviour
{
    public Transform player; // Referência ao jogador
    public float followSpeed = 5f;
    public float offsetDistance = 2f; // Distância ao lado direito do jogador
    public float tolerance = 0.1f;
    public float verticalFollowSpeed = 3f;

    private Animator animator;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        ForceLeftFlip(); // Garante que começa virado para esquerda
    }

    void Update()
    {
        MoveToRightSide();
    }

    void MoveToRightSide()
    {
        // Posição alvo: lado DIREITO do jogador (X positivo)
        float targetX = player.position.x + offsetDistance;
        float targetY = player.position.y;

        // Movimento no eixo X
        float directionX = targetX > transform.position.x ? 1 : -1;
        float velocityX = Mathf.Abs(transform.position.x - targetX) > tolerance ?
                         directionX * followSpeed : 0;

        // Movimento no eixo Y
        float directionY = targetY > transform.position.y ? 1 : -1;
        float velocityY = Mathf.Abs(transform.position.y - targetY) > tolerance ?
                         directionY * verticalFollowSpeed : 0;

        rb.velocity = new Vector2(velocityX, velocityY);
        animator.SetBool("isRunning", rb.velocity.magnitude > 0);

        // Mantém sempre virado para ESQUERDA
        ForceLeftFlip();
    }

    void ForceLeftFlip()
    {
        // Flip para esquerda (scale X negativo)
        if (transform.localScale.x > 0)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
}