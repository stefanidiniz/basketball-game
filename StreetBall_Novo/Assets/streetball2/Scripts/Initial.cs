using UnityEngine;
using UnityEngine.SceneManagement;

public class Initial : MonoBehaviour
{
    void Awake()
    {
        Debug.Log("InitialLoader - Cena atual: " + SceneManager.GetActiveScene().buildIndex);

        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            Debug.Log("Carregando cena do Menu (0)");
            SceneManager.LoadScene(0);
        }
    }
}