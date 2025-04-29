using UnityEngine;

public class TorcidaAudio : MonoBehaviour
{
    public AudioSource torcidaAudio;
    public float fadeSpeed = 1f;
    private float targetVolume = 0f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            targetVolume = 1f;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            targetVolume = 0f;
        }
    }

    void Update()
    {
        torcidaAudio.volume = Mathf.Lerp(torcidaAudio.volume, targetVolume, fadeSpeed * Time.deltaTime);
    }
}