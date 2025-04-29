using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections;

public class PauseMenu : MonoBehaviour
{
    public GameObject firstSelectedButton;

    private void OnEnable()
    {
        StartCoroutine(SelectButtonNextFrame());
    }

    private IEnumerator SelectButtonNextFrame()
    {
        yield return null; // Espera UM FRAME para a UI terminar de carregar

        if (firstSelectedButton != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(firstSelectedButton);
        }
        else
        {
            Debug.LogWarning("Nenhum botão inicial atribuído no PauseMenu!");
        }
    }

    public void ContinueGame()
    {
        GameManager.instance.ResumeGame();
    }

    public void GoToMainMenu()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.ResetGame();
        }
        else
        {
            Debug.LogWarning("GameManager não encontrado!");
        }

        SceneManager.LoadScene(0);
    }
}
