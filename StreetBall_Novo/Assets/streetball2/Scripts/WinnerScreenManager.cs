using UnityEngine;
using UnityEngine.UI;

public class WinnerScreenManager : MonoBehaviour
{
    public Text finalScoreText;

    void Start()
    {
        // Pega o score salvo
        int finalScore = PlayerPrefs.GetInt("FinalScore", 0);

        // Atualiza o texto na tela
        if (finalScoreText != null)
        {
            finalScoreText.text = "" + finalScore;
        }
    }
}
