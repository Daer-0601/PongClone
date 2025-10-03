using UnityEngine;

public class Goal : MonoBehaviour
{
    public bool isLeftGoal;
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Ball")
        {
            if (isLeftGoal)
            {
                gameManager.PlayerRightScores();
            }
            else
            {
                gameManager.PlayerLeftScores();
            }
        }
    }
}