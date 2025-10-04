using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public float initialSpeed = 5f;
    // Velocidad actual de la pelota (aumentara con cada golpe)
    private float currentSpeed;
    // Cantidad de velocidad que se añade cada vez que golpea una raqueta
    public float speedIncrease = 0.2f;
    // Velocidad maxima que puede alcanzar
    public float maxSpeed = 15f;
    public AudioClip bounceSound;
    private Rigidbody2D rb;
    // Reproducir sonidos
    private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        // Establecer la velocidad actual como la velocidad inicial
        currentSpeed = initialSpeed;
        // Iniciar el movimiento de la pelota en una direccion aleatoria
        Launch();
    }

    // Funcion para lanzar la pelota
    void Launch()
    {
        // Direccion aleatoria: -1 o 1 para X, y un valor aleatorio para Y
        float x = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = Random.Range(-1f, 1f);
        // Aplicar velocidad a la pelota
        rb.velocity = new Vector2(x, y).normalized * currentSpeed;
    }

    // Detectar colisiones con las raquetas y paredes
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Reproducir sonido de rebote
        if (bounceSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(bounceSound);
        }
        // Si la pelota golpea una raqueta, aumentar velocidad
        if (collision.gameObject.name == "PaddleLeft" || collision.gameObject.name == "PaddleRight")
        {
            // Aumentar la velocidad actual
            currentSpeed += speedIncrease;

            // Limitar la velocidad máxima
            if (currentSpeed > maxSpeed)
            {
                currentSpeed = maxSpeed;
            }

            // Aplicar la nueva velocidad manteniendo la direccion actual
            rb.velocity = rb.velocity.normalized * currentSpeed;
        }
    }

    // Reiniciar la pelota cuando salga de los limites
    public void ResetBall()
    {
        // Volver al centro
        transform.position = Vector3.zero;

        // Detener movimiento
        rb.velocity = Vector2.zero;

        // Resetear la velocidad a la inicial
        currentSpeed = initialSpeed;

        // Esperar un momento y lanzar de nuevo
        Invoke("Launch", 1f);
    }
}