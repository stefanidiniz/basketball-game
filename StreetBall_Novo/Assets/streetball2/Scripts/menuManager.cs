using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        // Destroi o GameManager atual, se houver
        if (GameManager.instance != null)
        {
            Destroy(GameManager.instance.gameObject);
            GameManager.instance = null; // Precisa limpar a referência!
        }

        SceneManager.LoadScene(5); // Carrega a cena do jogo diretamente
    }

    public void OpenControls()
    {
        SceneManager.LoadScene(3); // TutorialBotoes
    }

    public void OpenCredits()
    {
        SceneManager.LoadScene(1); // Creditos
    }

    public void OpenRanking()
    {
        SceneManager.LoadScene(2); // Ranking (placeholder, sem funcionalidade)
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBGL
        Application.OpenURL("about:blank");
#else
        Application.Quit();
#endif
    }
}
