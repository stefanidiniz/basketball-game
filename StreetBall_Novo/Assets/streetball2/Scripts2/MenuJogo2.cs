using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuJogo2 : MonoBehaviour
{

    void Update()
    {
        ReturnGame();
    }

    public void ReturnGame()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.UnloadSceneAsync(7);
        }
    }

    public void Resume()
    {
        SceneManager.UnloadSceneAsync(7);
    }
}