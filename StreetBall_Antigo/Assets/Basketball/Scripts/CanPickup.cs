using UnityEngine;

public class CanPowerUp : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float doubleScoreDuration = 10f; // Dura��o do efeito de pontua��o dobrada (segundos)

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o jogador coletou a latinha
        if (other.CompareTag("Player"))
        {
            Debug.Log($"Lata coletada! Ativando pontua��o dobrada por {doubleScoreDuration} segundos.");

            // Ativa o efeito de pontua��o dobrada no GameManager
            if (GameManager.instance != null)
            {
                GameManager.instance.ActivateDoubleScore(doubleScoreDuration);
            }
            else
            {
                Debug.LogWarning("GameManager n�o encontrado!");
            }

            // OBS: A destrui��o da latinha � gerenciada pelo seu script existente
        }
    }
}