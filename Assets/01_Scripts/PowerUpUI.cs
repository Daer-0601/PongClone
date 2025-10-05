using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PowerUpUI : MonoBehaviour
{
    public Text powerUpText;
    public float displayTime = 3f;
    
    void Start()
    {
        // Ocultar texto al inicio
        if (powerUpText != null)
            powerUpText.gameObject.SetActive(false);
    }
    
    // Método para mostrar mensaje de power-up
    public void ShowPowerUpMessage(string message)
    {
        if (powerUpText != null)
        {
            powerUpText.text = message;
            powerUpText.gameObject.SetActive(true);
            StartCoroutine(HideAfterDelay());
        }
    }
    
    private IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(displayTime);
        if (powerUpText != null)
            powerUpText.gameObject.SetActive(false);
    }
}