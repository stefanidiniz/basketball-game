// r0b3rt
// date: 2025-04-22
using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public bool canMove = true;

    [Header("References")]
    public Rigidbody2D rb;
    public Animator animator;
    public Transform handPosition;

    private Vector2 movement;
    private bool hasBall = true;
    private GameObject currentBall;
    private Vector3 originalScale;

    private float velocidadeOriginal;
    private Coroutine boostCorrente;

    void Start()
    {
        originalScale = transform.localScale;
        velocidadeOriginal = moveSpeed;
    }

    void Update()
    {
        mover();
    }

    public void mover()
    {
        if (canMove)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }
        else
        {
            movement = Vector2.zero;
        }

        animator.SetBool("isRunning", movement != Vector2.zero);
        animator.SetBool("hasBall", hasBall);

        if (movement.x != 0)
        {
            transform.localScale = new Vector3(originalScale.x * Mathf.Sign(movement.x), originalScale.y, originalScale.z);
        }

        if (hasBall && currentBall != null)
        {
            currentBall.transform.position = handPosition.position;
        }
    }
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball") && !hasBall)
        {
            currentBall = collision.gameObject;
            currentBall.transform.SetParent(null);
            currentBall.transform.localScale = Vector3.one;
            currentBall.transform.rotation = Quaternion.identity;
            currentBall.SetActive(false);
            hasBall = true;
            animator.SetBool("hasBall", hasBall);
        }
    }

    public void OnThrow()
    {
        hasBall = false;
        animator.SetBool("hasBall", hasBall);
        currentBall = null;
        canMove = true;
    }

    public bool HasBall()
    {
        return hasBall;
    }

    // ========== REDUÇÃO DE VELOCIDADE ==========

    public void ReduzirVelocidade(float fator, float duracao)
    {
        moveSpeed /= fator;
        CancelInvoke(nameof(RestaurarVelocidade));
        Invoke(nameof(RestaurarVelocidade), duracao);
    }

    private void RestaurarVelocidade()
    {
        moveSpeed = velocidadeOriginal;
    }

    // ========== BOOST DE VELOCIDADE TEMPORÁRIO ==========

    public void ActivateSpeedBoost(float aumento, float duracao)
    {
        if (boostCorrente != null)
            StopCoroutine(boostCorrente);

        boostCorrente = StartCoroutine(BoostTemporario(aumento, duracao));
    }

    private IEnumerator BoostTemporario(float aumento, float duracao)
    {
        moveSpeed = velocidadeOriginal + aumento;

        yield return new WaitForSeconds(duracao);

        moveSpeed = velocidadeOriginal;
        boostCorrente = null;
    }
    public void RemoverLentidao()
    {
        CancelInvoke(nameof(RestaurarVelocidade));
        moveSpeed = velocidadeOriginal;
    }

}
