using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    [Header("Configurações do Boost")]
    public float duracao = 4f;
    public float aumentoVelocidade = 2f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            if (player != null) player.ActivateSpeedBoost(aumentoVelocidade, duracao);

            Destroy(gameObject);
        }
    }
}