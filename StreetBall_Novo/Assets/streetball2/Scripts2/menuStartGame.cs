using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuStartGame : MonoBehaviour
{
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
}
