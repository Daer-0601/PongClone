using UnityEngine;
using UnityEngine.SceneManagement;

public class ModeSelect : MonoBehaviour
{
    // Cuando elija 1 jugador
    public void SelectSinglePlayer()
    {
        PlayerPrefs.SetInt("GameMode", 1);
        SceneManager.LoadScene("GameScene"); 
    }

    // Cuando elija 2 jugadores
    public void SelectTwoPlayers()
    {
        PlayerPrefs.SetInt("GameMode", 2);
        SceneManager.LoadScene("GameScene");
    }
}
