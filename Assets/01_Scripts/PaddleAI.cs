using UnityEngine;

public class PaddleAI : MonoBehaviour
{
    public Transform ball;
    public float speed = 8f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (ball == null) return;

        float direction = 0;
        if (ball.position.y > transform.position.y + 0.5f) direction = 1;
        else if (ball.position.y < transform.position.y - 0.5f) direction = -1;

        rb.velocity = new Vector2(0, direction * speed);
    }
}
