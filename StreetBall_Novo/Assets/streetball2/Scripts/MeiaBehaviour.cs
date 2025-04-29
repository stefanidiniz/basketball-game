using UnityEngine;

public class MeiaBehaviour : MonoBehaviour
{
    [Header("Efeito de Velocidade")]
    public float reducaoVelocidade = 2f;
    public float duracaoEfeito = 3f; // <<<<< NOVO

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            if (player != null)
            {
                player.ReduzirVelocidade(reducaoVelocidade, duracaoEfeito); // passa a duração também
            }

            if (SpawnSystem.Instance != null)
            {
                SpawnSystem.Instance.MeiaColetada();
            }

            Destroy(gameObject);
        }
    }
}
