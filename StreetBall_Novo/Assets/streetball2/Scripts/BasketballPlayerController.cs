using System.Collections;
using UnityEngine;

public class BasketballPlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float directionChangeThreshold = 0.7f; // Sensibilidade para mudan�a de dire��o

    private Rigidbody2D rb;
    private Animator animator;
    private bool isFacingRight = true;
    private float lastMoveX = 1f; // Come�a virado para direita
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
        originalSpeed = speed; // Supondo que voc� tenha uma vari�vel 'speed'
        originalForce = force; // Supondo que voc� tenha uma vari�vel 'force'
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        rb.velocity = new Vector2(moveX, moveY).normalized * speed;


        // Detecta mudan�a brusca de dire��o
        if (Mathf.Abs(moveX) > directionChangeThreshold)
        {
            bool movingRight = moveX > 0;

            if (movingRight != isFacingRight)
            {
                isFacingRight = movingRight;
                ForceAnimationUpdate(); // For�a atualiza��o imediata
            }
        }

        // Controle de anima��es
        if (rb.velocity.magnitude > 0.1f) // Est� se movendo
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
        else // Est� parado/marcando
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

        Block(); // Anima��o de bloqueio

    }

    void ForceAnimationUpdate()
    {
        // For�a uma atualiza��o imediata do Animator
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
        // Verifica se j� est� no estado correto
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(triggerName))
            return;

        ResetAllTriggers();
        animator.SetTrigger(triggerName);
    }

    public void Block()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            // Usa a mesma l�gica de dire��o que o resto do c�digo
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

            // Inicia a corrotina para voltar ao normal ap�s o tempo
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