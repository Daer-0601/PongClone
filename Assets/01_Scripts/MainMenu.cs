using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Text highScoreText;
    
    void Start()
    {
        // Mostrar el high score al iniciar el menu
        if (highScoreText != null)
        {
            int highScore = PlayerPrefs.GetInt("HighScore", 0);
            highScoreText.text = "High Score: " + highScore.ToString();
        }
    }
    
    // Funcion para iniciar el juego
    public void StartGame()
    {
        // Cargar la escena del juego
        SceneManager.LoadScene("ModeGame");
    }
    
    // Funcion para salir del juego
    public void QuitGame()
    {
        // Cerrar la aplicación
        Application.Quit();
        
        // Linea para probar en el Editor de Unity
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}