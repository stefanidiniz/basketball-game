using UnityEngine;

public class ScoreSound : MonoBehaviour
{
    public AudioSource cestaAudioSource; // Som da cesta
    public AudioSource torcidaAudioSource; // Som extra da torcida

    private bool canScore = true; // Controla se pode pontuar
    private float scoreCooldown = 1f; // Tempo de espera entre pontuações (em segundos)
    private float lastScoreTime; // Última vez que pontuou

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball") && canScore && Time.time >= lastScoreTime + scoreCooldown)
        {
            Debug.Log($"Colisão detectada com a bola! Tempo: {Time.time}");

            // Toca o som da cesta
            if (cestaAudioSource != null)
            {
                cestaAudioSource.Play();
            }
            else
            {
                Debug.LogWarning("Cesta AudioSource não atribuído no Inspector.");
            }

            // Toca o som da torcida
            if (torcidaAudioSource != null)
            {
                torcidaAudioSource.Play();
            }
            else
            {
                Debug.LogWarning("Torcida AudioSource não atribuído no Inspector.");
            }

            // Adiciona pontos ao placar
            if (GameManager.instance != null)
            {
                GameManager.instance.AddScore();
                Debug.Log("Chamando AddScore no GameManager");
            }
            else
            {
                Debug.LogWarning("GameManager não encontrado!");
            }

            // Desativa a pontuação temporariamente
            canScore = false;
            lastScoreTime = Time.time;
            Invoke(nameof(ResetScoreCooldown), scoreCooldown);
        }
    }

    private void ResetScoreCooldown()
    {
        canScore = true;
        Debug.Log("Cooldown de pontuação resetado. Pode pontuar novamente.");
    }
}