using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int playerLeftScore = 0;
    public int playerRightScore = 0;
    public Text leftScoreText;
    public Text rightScoreText;
    public BallMovement ball;

    void Start()
    {
        UpdateScoreUI();
    }

    public void PlayerLeftScores()
    {
        playerLeftScore++;
        UpdateScoreUI();
        ball.ResetBall();
    }

    public void PlayerRightScores()
    {
        playerRightScore++;
        UpdateScoreUI();
        ball.ResetBall();
    }

    void UpdateScoreUI()
    {
        leftScoreText.text = playerLeftScore.ToString();
        rightScoreText.text = playerRightScore.ToString();
    }
}