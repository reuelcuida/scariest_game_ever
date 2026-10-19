using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public TMP_Text finalScoreText;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1f;

        if (finalScoreText != null)
        {
            finalScoreText.text =
                "Score: " + PlayerPrefs.GetInt("FinalScore", 0);
        }
    }

    public void Retry()
    {
        Time.timeScale = 1f;

        GameManager.PrepararNovaPartida();

        SceneManager.LoadScene("JogoPrincipal");
    }
}