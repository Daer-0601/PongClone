using UnityEngine;

public class PaddleMovement : MonoBehaviour
{
    public float speed = 10f;
    public bool isLeftPlayer = true; 

    private Rigidbody2D rb;
    private float moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isLeftPlayer)
        {
            moveInput = 0;
            if (Input.GetKey(KeyCode.W)) moveInput = 1;
            if (Input.GetKey(KeyCode.S)) moveInput = -1;
        }
        else
        {
            moveInput = 0;
            if (Input.GetKey(KeyCode.UpArrow)) moveInput = 1;
            if (Input.GetKey(KeyCode.DownArrow)) moveInput = -1;
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(0, moveInput * speed);
    }
}
