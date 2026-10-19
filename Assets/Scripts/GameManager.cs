using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public TMP_Text scoreText;

    private static float score = 0f;
    private static bool novaPartida = true;

    private bool gameOver = false;

    void Awake()
    {
        if (SceneManager.GetActiveScene().name == "JogoPrincipal"
            && novaPartida)
        {
            score = 0f;
            novaPartida = false;
        }
    }

    void Start()
    {
        AtualizarScore();
    }

    void Update()
    {
        if (gameOver) return;

        score += Time.deltaTime;
        AtualizarScore();
    }

    void AtualizarScore()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + Mathf.FloorToInt(score);
        }
    }

    public void GameOver()
    {
        if (gameOver) return;

        gameOver = true;

        PlayerPrefs.SetInt("FinalScore", Mathf.FloorToInt(score));
        PlayerPrefs.Save();

        StartCoroutine(GameOverSequence());
    }

    private IEnumerator GameOverSequence()
    {
        float waitTime = 0f;

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.StopChase();
            waitTime = AudioManager.Instance.PlayCapture();
        }

        yield return new WaitForSecondsRealtime(waitTime);

        Time.timeScale = 0.1f;
        SceneManager.LoadScene("GameOverScene");
    }

    public static void PrepararNovaPartida()
    {
        score = 0f;
        novaPartida = true;
    }
}