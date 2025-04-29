using System.Collections;
using UnityEngine;

public class BasketballPlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float directionChangeThreshold = 0.7f; // Sensibilidade para mudança de direção

    private Rigidbody2D rb;
    private Animator animator;
    private bool isFacingRight = true;
    private float lastMoveX = 1f; // Começa virado para direita
    private float originalSpeed;
    private float originalForce;
    private bool hasPowerUp = false;
    public float force = 2f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        ResetAllTriggers();
        animator.SetTrigger("markingRight");
        originalSpeed = speed; // Supondo que você tenha uma variável 'speed'
        originalForce = force; // Supondo que você tenha uma variável 'force'
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        rb.velocity = new Vector2(moveX, moveY).normalized * speed;


        // Detecta mudança brusca de direção
        if (Mathf.Abs(moveX) > directionChangeThreshold)
        {
            bool movingRight = moveX > 0;

            if (movingRight != isFacingRight)
            {
                isFacingRight = movingRight;
                ForceAnimationUpdate(); // Força atualização imediata
            }
        }

        // Controle de animações
        if (rb.velocity.magnitude > 0.1f) // Está se movendo
        {
            if (isFacingRight)
            {
                SetAnimationTrigger("runningRight");
            }
            else
            {
                SetAnimationTrigger("runningLeft");
            }
        }
        else // Está parado/marcando
        {
            if (isFacingRight)
            {
                SetAnimationTrigger("markingRight");
            }
            else
            {
                SetAnimationTrigger("markingLeft");
            }
        }

        Block(); // Animação de bloqueio

    }

    void ForceAnimationUpdate()
    {
        // Força uma atualização imediata do Animator
        animator.Update(0f);
        ResetAllTriggers();
    }

    void ResetAllTriggers()
    {
        animator.ResetTrigger("runningLeft");
        animator.ResetTrigger("runningRight");
        animator.ResetTrigger("markingRight");
        animator.ResetTrigger("markingLeft");
        animator.ResetTrigger("tapa");
        animator.ResetTrigger("tapaL");
    }

    void SetAnimationTrigger(string triggerName)
    {
        // Verifica se já está no estado correto
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(triggerName))
            return;

        ResetAllTriggers();
        animator.SetTrigger(triggerName);
    }

    public void Block()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            // Usa a mesma lógica de direção que o resto do código
            if (isFacingRight)
            {
                SetAnimationTrigger("tapa"); // Bloqueio direito
                rb.velocity = Vector2.zero;
            }
            else
            {
                SetAnimationTrigger("tapaL"); // Bloqueio esquerdo
                rb.velocity = Vector2.zero;
            }
        }
    }

    public void ApplySpeedPowerUp(float speedMult, float forceMult, float duration)
    {
        if (!hasPowerUp)
        {
            hasPowerUp = true;

            // Aplica os multiplicadores
            speed *= speedMult;
            force *= forceMult;

            // Inicia a corrotina para voltar ao normal após o tempo
            StartCoroutine(ResetPowerUp(duration));
        }
    }

    private IEnumerator ResetPowerUp(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Volta aos valores originais
        speed = originalSpeed;
        force = originalForce;
        hasPowerUp = false;
    }
}