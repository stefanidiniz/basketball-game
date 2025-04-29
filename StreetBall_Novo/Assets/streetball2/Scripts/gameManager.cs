using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("UI References")]
    public Text scoreText;
    public Text timerText;

    [Header("Game Settings")]
    public float gameTime = 150f;
    public int pointsPerBasket = 2;

    private int score = 0;
    private float timeRemaining;
    private bool gameIsOver = false;
    private bool isScoreDoubled = false;
    private bool isPaused = false;

    private AudioSource gameMusic;
    private AudioSource pauseMusic;
    private AudioSource crowdAudio;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 5) // Cena do jogo
        {
            ResetGameState();
            FindAndSetupMusic();
            FindAndSetupUI();
            Time.timeScale = 1f;
            isPaused = false;
        }
    }

    private void FindAndSetupUI()
    {
        if (scoreText == null)
        {
            GameObject scoreObj = GameObject.Find("ScoreText");
            if (scoreObj != null)
                scoreText = scoreObj.GetComponent<Text>();
        }

        if (timerText == null)
        {
            GameObject timerObj = GameObject.Find("TimerText");
            if (timerObj != null)
                timerText = timerObj.GetComponent<Text>();
        }

        UpdateScoreUI();
        UpdateTimerUI();
    }

    private void FindAndSetupMusic()
    {
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();

        foreach (AudioSource source in audioSources)
        {
            if (source.CompareTag("GameMusic"))
                gameMusic = source;
            else if (source.CompareTag("PauseMusic"))
                pauseMusic = source;
            else if (source.CompareTag("CrowdAudio"))
                crowdAudio = source;
        }
    }

    void Update()
    {
        if (!gameIsOver && !isPaused)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerUI();

            if (timeRemaining <= 0)
            {
                EndGame();
            }
        }
    }

    public void PauseGame()
    {
        if (isPaused) return; // Evita pausar duas vezes

        isPaused = true;
        Time.timeScale = 0f;

        if (gameMusic != null) gameMusic.Pause();
        if (crowdAudio != null) crowdAudio.Pause();
        if (pauseMusic != null) pauseMusic.Play();

        SceneManager.LoadScene(4, LoadSceneMode.Additive);
    }

    public void ResumeGame()
    {
        if (!isPaused) return; // Se não estiver pausado, ignora

        StartCoroutine(ResumeAfterPause());
    }

    private IEnumerator ResumeAfterPause()
    {
        // Para a música de pausa
        if (pauseMusic != null) pauseMusic.Stop();

        // Descarrega a cena de Pause
        AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(4);

        // Espera descarregar
        while (!asyncUnload.isDone)
        {
            yield return null;
        }

        if (gameMusic != null) gameMusic.Play();
        if (crowdAudio != null) crowdAudio.Play();

        isPaused = false;
        Time.timeScale = 1f;
    }

    public void AddScore()
    {
        if (!gameIsOver && !isPaused)
        {
            int pointsToAdd = isScoreDoubled ? pointsPerBasket * 2 : pointsPerBasket;
            score += pointsToAdd;
            UpdateScoreUI();
        }
    }

    public void ActivateDoubleScore(float duration)
    {
        if (!gameIsOver && !isPaused)
        {
            isScoreDoubled = true;
            StartCoroutine(DeactivateDoubleScore(duration));
        }
    }

    private IEnumerator DeactivateDoubleScore(float duration)
    {
        yield return new WaitForSeconds(duration);
        isScoreDoubled = false;
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = $"Score: {score}";
    }

    private void UpdateTimerUI()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(timeRemaining / 60);
            int seconds = Mathf.FloorToInt(timeRemaining % 60);
            timerText.text = $"Time: {minutes:00}:{seconds:00}";
        }
    }

    private void ResetGameState()
    {
        score = 0;
        timeRemaining = gameTime;
        gameIsOver = false;
        isScoreDoubled = false;
        isPaused = false;
        Time.timeScale = 1f;

        UpdateScoreUI();
        UpdateTimerUI();
    }

    public void ResetGame()
    {
        StopAllCoroutines();

        if (gameMusic != null) gameMusic.Stop();
        if (pauseMusic != null) pauseMusic.Stop();
        if (crowdAudio != null) crowdAudio.Stop();

        ResetGameState();
    }

    private void EndGame()
    {
        gameIsOver = true;
        PlayerPrefs.SetInt("FinalScore", score);
        PlayerPrefs.Save();
        SceneManager.LoadScene(6); // Cena de Vencedor
    }

    public void PausarGame()
    {
        PauseGame();
    }
}
