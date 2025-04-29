using UnityEngine;

public class NPCBlock : MonoBehaviour
{
    [Header("Referências")]
    public Animator enemyAnimator;                  // Animator do NPC
    public PlayerRandomShot playerShotScript;       // Script do jogador que arremessa

    void Start()
    {
        // Se não for atribuído manualmente, tenta encontrar automaticamente na cena
        if (playerShotScript == null)
        {
            playerShotScript = FindObjectOfType<PlayerRandomShot>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball") && playerShotScript != null && playerShotScript.isBallMoving)
        {
            Debug.Log("NPC BLOQUEOU O ARREMESSO!");

            // Ativa animação de bloqueio
            enemyAnimator.SetTrigger("Block");

            // Interrompe a coroutine do arremesso
            playerShotScript.InterruptShot();

            // (Opcional) aplica física para a bola "cair"
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
                rb.isKinematic = false;
                rb.gravityScale = 1f;
            }
        }
    }
}
