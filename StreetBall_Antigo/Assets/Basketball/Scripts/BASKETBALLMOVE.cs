using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class BASKETBALLMOVE : MonoBehaviour
{
    private bool turnRight = true;
    private Rigidbody2D rb;
    private Animator playerController;
    private PlayerMovement playerMovement;
    private PlayerRandomShot playerShot;

    public bool isRunning = false;
    private bool isShooting = false;
    private bool isLayUp = false;
    private bool isDunking = false;
    private bool isBlocking = false;

    [SerializeField] public float TempoParaBolaVoltar = 0.1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerController = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShot = GetComponent<PlayerRandomShot>();
        isRunning = false;
    }

    void Update()
    {
        if (!isShooting && !isLayUp && !isDunking && !isBlocking)
        {
            Flip();
        }
        if (playerMovement != null)
            isRunning = playerMovement.animator.GetBool("isRunning");

        //LayUp();
        //JumpShot();
        //JumpShotSpin();
        //StepBack();
    }

    void Flip()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        if (horizontalInput > 0 && !turnRight)
        {
            FlipCharacter();
        }
        else if (horizontalInput < 0 && turnRight)
        {
            FlipCharacter();
        }
    }

    void FlipCharacter()
    {
        turnRight = !turnRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
        playerController.SetFloat("eixoX", turnRight ? 1 : -1);
    }

    void Block()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            isBlocking = true;
            if (playerMovement != null) playerMovement.canMove = false;
            playerController.SetTrigger("Block");
            StartCoroutine(FinalizarBlock());
        }
    }

    public void JumpShot()
    {
        if (!isShooting)
        {
            isShooting = true;
            if (playerMovement != null) playerMovement.canMove = false;
            rb.velocity = Vector2.zero;
            playerController.SetBool("isRunning", false);
            playerController.SetTrigger("JumpShot");

            StartCoroutine(FinalizarShoot());
        }
    }

    public void JumpShotSpin()
    {
        if (!isShooting)
        {
            isShooting = true;
            if (playerMovement != null) playerMovement.canMove = false;
            rb.velocity = Vector2.zero;
            playerController.SetBool("isRunning", false);
            playerController.SetTrigger("JumpShotSpin");
            StartCoroutine(FinalizarShootSpin());
        }
    }

    public void StepBack()
    {
        if (!isShooting)
        {
            isShooting = true;
            if (playerMovement != null) playerMovement.canMove = false;
            rb.velocity = Vector2.zero;
            playerController.SetBool("isRunning", false);
            playerController.SetTrigger("StepBack");
            StartCoroutine(FinalizarShoot());
        }
    }

    public void LayUp()
    {
        if (!isLayUp)
        {
            if (playerShot != null && playerShot.dentroDaZonaDaCesta)
            {
                isLayUp = true;
                playerController.SetTrigger("LayUp");
                StartCoroutine(FinalizarLayUp());
            }
        }
    }

    IEnumerator FinalizarShoot()
    {
        yield return new WaitForSeconds(0.5f);
        isShooting = false;
        if (playerMovement != null) playerMovement.canMove = true;
        playerController.SetBool("isRunning", false);
    }

    IEnumerator FinalizarShootSpin()
    {
        yield return new WaitForSeconds(1.2f);
        isShooting = false;
        if (playerMovement != null) playerMovement.canMove = true;
        playerController.SetBool("isRunning", false);
    }

    IEnumerator FinalizarLayUp()
    {
        yield return new WaitForSeconds(0.5f);
        isLayUp = false;
        //if (playerMovement != null) playerMovement.canMove = true;
        playerController.SetBool("isRunning", false);
    }

    IEnumerator FinalizarBlock()
    {
        yield return new WaitForSeconds(0.4f);
        isBlocking = false;
        if (playerMovement != null) playerMovement.canMove = true;
        playerController.SetBool("isRunning", false);
    }
}