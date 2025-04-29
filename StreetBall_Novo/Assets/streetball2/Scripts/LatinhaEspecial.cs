using UnityEngine;

public class LatinhaEspecial : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            if (player != null)
            {
                player.RemoverLentidao(); // limpa o efeito da meia
            }

            Destroy(gameObject);
        }
    }
}
