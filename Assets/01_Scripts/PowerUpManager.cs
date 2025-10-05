using UnityEngine;
using System.Collections;

public class PowerUpManager : MonoBehaviour
{
    public static PowerUpManager Instance;
    private PowerUpUI powerUpUI;
    
    // Referencias a las raquetas y pelota
    public GameObject leftPaddle;
    public GameObject rightPaddle;
    public GameObject ball;
    
    // Tamaños originales para restaurar después
    private Vector3 originalLeftPaddleSize;
    private Vector3 originalRightPaddleSize;
    private float originalBallSpeed;
    
    // Control de power-ups activos
    private bool isDoublePointsActive = false;
    private Coroutine currentPowerUpCoroutine;

    void Awake()
    {
        // Patrón Singleton para fácil acceso
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Guardar tamaños originales
        if (leftPaddle != null) originalLeftPaddleSize = leftPaddle.transform.localScale;
        if (rightPaddle != null) originalRightPaddleSize = rightPaddle.transform.localScale;

        // Encontrar la pelota si no está asignada
        if (ball == null) ball = GameObject.Find("Ball");

        // Obtener la velocidad original de la pelota
        if (ball != null)
        {
            BallMovement ballMovement = ball.GetComponent<BallMovement>();
            if (ballMovement != null)
            {
                // Usamos reflexión para acceder al campo privado currentSpeed
                originalBallSpeed = GetBallCurrentSpeed(ballMovement);
            }
        }

    // Buscar el UI de power-ups en la escena
         powerUpUI = FindObjectOfType<PowerUpUI>();
        if (powerUpUI == null)
        {
        Debug.LogWarning("PowerUpUI no encontrado en la escena");
        }
    }
    
    // ========== POWER-UP 1: RAQUETA MÁS GRANDE ==========
    public void ActivateBigPaddle(bool isLeftPlayer)
    {
        GameObject paddle = isLeftPlayer ? leftPaddle : rightPaddle;
        Vector3 originalSize = isLeftPlayer ? originalLeftPaddleSize : originalRightPaddleSize;
        
        if (paddle != null)
        {
            // Agrandar la raqueta
            paddle.transform.localScale = new Vector3(originalSize.x, originalSize.y * 1.5f, originalSize.z);
            
            // Programar para volver al tamaño normal después de 5 segundos
            StartCoroutine(ResetPaddleSize(paddle, originalSize, 5f));
        }
    }
    
    // ========== POWER-UP 2: RAQUETA MÁS PEQUEÑA (OPONENTE) ==========
    public void ActivateSmallPaddle(bool isLeftPlayer)
    {
        GameObject paddle = isLeftPlayer ? rightPaddle : leftPaddle; // Opuesto al jugador
        Vector3 originalSize = isLeftPlayer ? originalRightPaddleSize : originalLeftPaddleSize;
        
        if (paddle != null)
        {
            // Reducir la raqueta del oponente
            paddle.transform.localScale = new Vector3(originalSize.x, originalSize.y * 0.6f, originalSize.z);
            
            // Programar para volver al tamaño normal después de 5 segundos
            StartCoroutine(ResetPaddleSize(paddle, originalSize, 5f));
        }
    }
    
    // ========== POWER-UP 3: PELOTA MÁS LENTA ==========
    public void ActivateSlowBall()
    {
        if (ball == null)
        {
            Debug.LogError("Ball reference is null in PowerUpManager");
            return;
        }
        
        BallMovement ballMovement = ball.GetComponent<BallMovement>();
        if (ballMovement != null)
        {
            // Guardar velocidad original si es la primera vez
            if (originalBallSpeed == 0) 
                originalBallSpeed = GetBallCurrentSpeed(ballMovement);
            
            // Reducir velocidad a la mitad usando reflexión
            SetBallCurrentSpeed(ballMovement, originalBallSpeed * 0.5f);
            
            // Actualizar la velocidad del Rigidbody2D
            Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = rb.velocity.normalized * (originalBallSpeed * 0.5f);
            }
            
            // Programar para volver a la velocidad normal después de 8 segundos
            StartCoroutine(ResetBallSpeed(ballMovement, originalBallSpeed, 8f));
        }
        else
        {
            Debug.LogError("No se encontró el componente BallMovement en el objeto Ball");
        }
    }
    
    // ========== POWER-UP 4: PUNTOS DOBLES ==========
    public void ActivateDoublePoints(float duration = 10f)
    {
        if (currentPowerUpCoroutine != null)
            StopCoroutine(currentPowerUpCoroutine);
            
        isDoublePointsActive = true;
        currentPowerUpCoroutine = StartCoroutine(DeactivateDoublePoints(duration));
    }
    
    public bool IsDoublePointsActive()
    {
        return isDoublePointsActive;
    }
    
    // ========== MÉTODOS DE ACTIVACIÓN DESDE GAME MANAGER ==========
    public void ActivatePowerUp(bool isLeftPlayerScored)
{
    int randomPowerUp = Random.Range(1, 5); // 1-4
    string powerUpMessage = "";
    string powerUpType = "";
    
    switch (randomPowerUp)
    {
        case 1:
            ActivateBigPaddle(isLeftPlayerScored);
            powerUpMessage = "¡POWER-UP! Raqueta Grande";
            powerUpType = "BigPaddle";
            break;
        case 2:
            ActivateSmallPaddle(isLeftPlayerScored);
            powerUpMessage = "¡POWER-UP! Raqueta Pequeña";
            powerUpType = "SmallPaddle";
            break;
        case 3:
            ActivateSlowBall();
            powerUpMessage = "¡POWER-UP! Pelota Lenta";
            powerUpType = "SlowBall";
            break;
        case 4:
            ActivateDoublePoints();
            powerUpMessage = "¡POWER-UP! Puntos Dobles";
            powerUpType = "DoublePoints";
            break;
    }
    


    if (powerUpUI != null)
        {
            powerUpUI.ShowPowerUpMessage(powerUpMessage);
        }
        else
        {
            Debug.Log("PowerUp UI no asignado, pero power-up activado: " + powerUpMessage);
        }
    
    Debug.Log("Power-up activado: " + powerUpMessage);
}
    
    // ========== CORRUTINAS PARA RESETEO ==========
    private IEnumerator ResetPaddleSize(GameObject paddle, Vector3 originalSize, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (paddle != null)
            paddle.transform.localScale = originalSize;
    }
    
    private IEnumerator ResetBallSpeed(BallMovement ballMovement, float originalSpeed, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (ballMovement != null)
        {
            SetBallCurrentSpeed(ballMovement, originalSpeed);
            
            // Actualizar también el Rigidbody2D
            Rigidbody2D rb = ballMovement.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = rb.velocity.normalized * originalSpeed;
            }
        }
    }
    
    private IEnumerator DeactivateDoublePoints(float delay)
    {
        yield return new WaitForSeconds(delay);
        isDoublePointsActive = false;
    }
    
    // ========== MÉTODOS DE REFLEXIÓN PARA ACCEDER A currentSpeed (privado) ==========
    private float GetBallCurrentSpeed(BallMovement ballMovement)
    {
        // Usar reflexión para acceder al campo privado currentSpeed
        System.Reflection.FieldInfo fieldInfo = typeof(BallMovement).GetField("currentSpeed", 
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        
        if (fieldInfo != null)
        {
            return (float)fieldInfo.GetValue(ballMovement);
        }
        
        // Si falla, usar la velocidad inicial como respaldo
        return ballMovement.initialSpeed;
    }
    
    private void SetBallCurrentSpeed(BallMovement ballMovement, float newSpeed)
    {
        // Usar reflexión para modificar el campo privado currentSpeed
        System.Reflection.FieldInfo fieldInfo = typeof(BallMovement).GetField("currentSpeed", 
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        
        if (fieldInfo != null)
        {
            fieldInfo.SetValue(ballMovement, newSpeed);
        }
    }
}