using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogoInicial : MonoBehaviour
{
    public bool isPaused = true;
    public GameObject img;

    private void Start()
    {
        if (Time.timeScale == 0f)
        {
            Resume();
        }
    }

    void Update()
    {
        CallPauseResume();
    }

    public void CallPauseResume()
    {
        if (isPaused == true)
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        isPaused = true;
        //Cursor.lockState = CursorLockMode.None;
        //Cursor.visible = true;
        img.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        isPaused = false;
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        img.SetActive(false);
    }
}
