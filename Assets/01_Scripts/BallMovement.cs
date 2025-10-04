using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Inicia el movimiento de la pelota en una direccion aleatoria
        Launch();
    }

    // Funcion para lanzar la pelota
    void Launch()
    {
        // Direccion aleatoria: -1 o 1 para X, y un valor aleatorio para Y
        float x = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = Random.Range(-1f, 1f);

        // Aplica velocidad a la pelota
        rb.velocity = new Vector2(x, y).normalized * speed;
    }

    // Reinicia la pelota cuando salga de los límites
    public void ResetBall()
    {
        // Volver al centro
        transform.position = Vector3.zero;
        // Detener movimiento
        rb.velocity = Vector2.zero;
        // Espera un momento y lanzar de nuevo
        Invoke("Launch", 1f);
    }
}