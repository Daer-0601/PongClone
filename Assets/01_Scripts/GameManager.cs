using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Puntuaciones de cada jugador
    public int playerLeftScore = 0;
    public int playerRightScore = 0;
    // Puntuacion necesaria para ganar
    public int scoreToWin = 5;
    // Referencia a los textos de puntuacion en la UI
    public Text leftScoreText;
    public Text rightScoreText;
    public Text highScoreText;
    // Referencias al panel de Game Over
    public GameObject gameOverPanel;
    public Text winnerText;

    public BallMovement ball;
    // Sonido cuando alguien anota
    public AudioClip scoreSound;
    private int highScore = 0;
    private bool gameEnded = false;

    void Start()
    {
        // Cargar la puntuacion mas alta guardada
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        // Asegurarse de que el panel de Game Over este desactivado al inicio
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
        // Actualizar los textos al inicio
        UpdateScoreUI();
        UpdateHighScoreUI();
    }

    // Funcion para añadir puntos al jugador izquierdo
    public void PlayerLeftScores()
    {
        if (gameEnded) return; // No hacer nada si el juego ya termino

        playerLeftScore++;
        // Reproducir sonido de puntuacion
        PlayScoreSound();

        UpdateScoreUI();
        CheckHighScore();
        CheckWinCondition();

        if (!gameEnded)
        {
            ball.ResetBall();
        }
    }

    // Funcion para añadir puntos al jugador derecho
    public void PlayerRightScores()
    {
        if (gameEnded) return; // No hacer nada si el juego ya termino

        playerRightScore++;
        // Reproducir sonido de puntuacion
        PlayScoreSound();

        UpdateScoreUI();
        CheckHighScore();
        CheckWinCondition();

        if (!gameEnded)
        {
            ball.ResetBall();
        }
    }

    // Funcion para reproducir el sonido de puntuacion
    void PlayScoreSound()
    {
        if (scoreSound != null)
        {
            GetComponent<AudioSource>().PlayOneShot(scoreSound);
        }
    }

    // Actualizar los textos de puntuacion
    void UpdateScoreUI()
    {
        leftScoreText.text = playerLeftScore.ToString();
        rightScoreText.text = playerRightScore.ToString();
    }

    // Actualizar el texto del high score
    void UpdateHighScoreUI()
    {
        if (highScoreText != null)
        {
            highScoreText.text = "High Score: " + highScore.ToString();
        }
    }

    // Verificar y guardar el high score
    void CheckHighScore()
    {
        // Verificar si algun jugador supero el high score
        int currentMaxScore = Mathf.Max(playerLeftScore, playerRightScore);

        if (currentMaxScore > highScore)
        {
            highScore = currentMaxScore;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
            UpdateHighScoreUI();
        }
    }

    // Verificar si alguien gano
    void CheckWinCondition()
    {
        if (playerLeftScore >= scoreToWin)
        {
            GameOver("LEFT PLAYER WINS!");
        }
        else if (playerRightScore >= scoreToWin)
        {
            GameOver("RIGHT PLAYER WINS!");
        }
    }

    // Mostrar pantalla de Game Over
    void GameOver(string winner)
    {
        gameEnded = true;

        // Reproducir sonido de puntuacion
        if (scoreSound != null)
        {
            GetComponent<AudioSource>().PlayOneShot(scoreSound);
        }

        // Detener la pelota
        ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        // Mostrar el panel de Game Over
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        // Mostrar quien gano
        if (winnerText != null)
        {
            winnerText.text = winner;
        }
    }

    // Funcion para reiniciar el juego
    public void RestartGame()
    {
        // Recargar la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Funcion para volver al menu principal
    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}