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
    public GameObject paddleLeft;
    public GameObject paddleRight;

    // Sonido cuando alguien anota
    public AudioClip scoreSound;
    private int highScore = 0;
    private bool gameEnded = false;

    void Start()
    {
        // Detectar el modo de juego guardado
        int mode = PlayerPrefs.GetInt("GameMode", 2); // por defecto 2 jugadores

        if (mode == 1)
        {
            // Activar IA en el paddle derecho
            if (paddleRight.GetComponent<PaddleMovement>() != null)
                paddleRight.GetComponent<PaddleMovement>().enabled = false;

            PaddleAI ai = paddleRight.GetComponent<PaddleAI>();
            if (ai == null) ai = paddleRight.AddComponent<PaddleAI>();
            ai.ball = ball.transform;
        }
        else
        {
            // Ambos jugadores humanos
            if (paddleRight.GetComponent<PaddleAI>() != null)
                Destroy(paddleRight.GetComponent<PaddleAI>());

            if (paddleRight.GetComponent<PaddleMovement>() != null)
                paddleRight.GetComponent<PaddleMovement>().enabled = true;
        }

        // Cargar la puntuacion mas alta guardada
        highScore = PlayerPrefs.GetInt("HighScore", 0);

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        UpdateScoreUI();
        UpdateHighScoreUI();
    }

    // Funcion para añadir puntos al jugador izquierdo
    public void PlayerLeftScores()
    {
        if (gameEnded) return;

        playerLeftScore++;
        PlayScoreSound();

        UpdateScoreUI();
        CheckHighScore();
        CheckWinCondition();

        if (!gameEnded) ball.ResetBall();
    }

    // Funcion para añadir puntos al jugador derecho
    public void PlayerRightScores()
    {
        if (gameEnded) return;

        playerRightScore++;
        PlayScoreSound();

        UpdateScoreUI();
        CheckHighScore();
        CheckWinCondition();

        if (!gameEnded) ball.ResetBall();
    }

    void PlayScoreSound()
    {
        if (scoreSound != null)
            GetComponent<AudioSource>().PlayOneShot(scoreSound);
    }

    void UpdateScoreUI()
    {
        leftScoreText.text = playerLeftScore.ToString();
        rightScoreText.text = playerRightScore.ToString();
    }

    void UpdateHighScoreUI()
    {
        if (highScoreText != null)
            highScoreText.text = "High Score: " + highScore.ToString();
    }

    void CheckHighScore()
    {
        int currentMaxScore = Mathf.Max(playerLeftScore, playerRightScore);

        if (currentMaxScore > highScore)
        {
            highScore = currentMaxScore;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
            UpdateHighScoreUI();
        }
    }

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

    void GameOver(string winner)
    {
        gameEnded = true;

        if (scoreSound != null)
            GetComponent<AudioSource>().PlayOneShot(scoreSound);

        ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        if (winnerText != null)
            winnerText.text = winner;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
