using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Launch();
    }

    void Launch()
    {
        float x = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = Random.Range(-1f, 1f);
        rb.velocity = new Vector2(x, y).normalized * speed;
    }

    public void ResetBall()
    {
        transform.position = Vector3.zero;
        rb.velocity = Vector2.zero;
        Invoke("Launch", 1f);
    }
}