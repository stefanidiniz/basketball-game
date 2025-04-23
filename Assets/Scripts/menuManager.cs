using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuManager : MonoBehaviour
{
    public void StartMenu()
    {
        SceneManager.LoadScene(2);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(3);
    }
    
    public void StartCreditos()
    {
        SceneManager.LoadScene(4);
    }

    public void StartTutorial()
    {
        SceneManager.LoadScene(5);
    }

    public void StartRanking()
    {
        SceneManager.LoadScene(6);
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
