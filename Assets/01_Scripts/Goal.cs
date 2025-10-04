using UnityEngine;

public class Goal : MonoBehaviour
{
    public bool isLeftGoal;
    private GameManager gameManager;

    void Start()
    {
        // Encontrar el GameManager en la escena
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Detecta cuando la pelota entra al gol
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica que sea la pelota
        if (collision.gameObject.name == "Ball")
        {
            // Si es el gol izquierdo, el jugador derecho anota
            if (isLeftGoal)
            {
                gameManager.PlayerRightScores();
            }
            // Si es el gol derecho, el jugador izquierdo anota
            else
            {
                gameManager.PlayerLeftScores();
            }
        }
    }
}