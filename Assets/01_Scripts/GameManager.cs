using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Puntuaciones de cada jugador
    public int playerLeftScore = 0;
    public int playerRightScore = 0;
    
    // Referencia a los textos de puntuación en la UI
    public Text leftScoreText;
    public Text rightScoreText;
    public Text highScoreText; // Texto para mostrar el high score
    public BallMovement ball;
    
    // Variable para guardar la puntuacion mas alta
    private int highScore = 0;
    
    void Start()
    {
        // Cargar la puntuacion mas alta guardada
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        
        // Actualiza los textos al inicio
        UpdateScoreUI();
        UpdateHighScoreUI();
    }

    public void PlayerLeftScores()
    {
        playerLeftScore++;
        UpdateScoreUI();
        CheckHighScore();
        ball.ResetBall();
    }

    public void PlayerRightScores()
    {
        playerRightScore++;
        UpdateScoreUI();
        CheckHighScore();
        ball.ResetBall();
    }
    
    // Actualiza los textos de puntuación
    void UpdateScoreUI()
    {
        leftScoreText.text = playerLeftScore.ToString();
        rightScoreText.text = playerRightScore.ToString();
    }
    
    // Actualiza el texto del high score
    void UpdateHighScoreUI()
    {
        if (highScoreText != null)
        {
            highScoreText.text = "High Score: " + highScore.ToString();
        }
    }
    
    // Verifica y guarda el high score
    void CheckHighScore()
    {
        // Verifica si algun jugador supero el high score
        int currentMaxScore = Mathf.Max(playerLeftScore, playerRightScore);
        
        if (currentMaxScore > highScore)
        {
            highScore = currentMaxScore;
            PlayerPrefs.SetInt("HighScore", highScore); // Guarda en PlayerPrefs
            PlayerPrefs.Save(); // Asegura de que se guarde
            UpdateHighScoreUI();
        }
    }
}