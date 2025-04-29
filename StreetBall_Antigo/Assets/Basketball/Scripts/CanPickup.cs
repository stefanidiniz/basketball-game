using UnityEngine;

public class CanPowerUp : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float doubleScoreDuration = 10f; // Duração do efeito de pontuação dobrada (segundos)

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o jogador coletou a latinha
        if (other.CompareTag("Player"))
        {
            Debug.Log($"Lata coletada! Ativando pontuação dobrada por {doubleScoreDuration} segundos.");

            // Ativa o efeito de pontuação dobrada no GameManager
            if (GameManager.instance != null)
            {
                GameManager.instance.ActivateDoubleScore(doubleScoreDuration);
            }
            else
            {
                Debug.LogWarning("GameManager não encontrado!");
            }

            // OBS: A destruição da latinha é gerenciada pelo seu script existente
        }
    }
}