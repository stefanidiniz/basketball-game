using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cenas : MonoBehaviour
{
    public void StartMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void StartTutorial()
    {
        SceneManager.LoadScene(3);
    }

    public void StartGame()
    {
        // Destroi o GameManager atual, se houver
        if (GameManager.instance != null)
        {
            Destroy(GameManager.instance.gameObject);
            GameManager.instance = null; // Precisa limpar a referência!
        }

        SceneManager.LoadScene(4); // Carrega a cena do jogo diretamente
    }
    
    public void StartCreditos()
    {
        SceneManager.LoadScene(1);
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
