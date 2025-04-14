using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(2);
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
