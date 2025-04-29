using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class LatinhaBehaviour : MonoBehaviour
{
    [SerializeField] private float tempoVida = 10f;
    [SerializeField] private float forcaQuicar = 4f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, tempoVida);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chao"))
        {
            rb.velocity = new Vector2(
                rb.velocity.x,
                Mathf.Min(forcaQuicar, rb.velocity.y * -0.5f)
            );
        }
    }
}