using UnityEngine;

public class PaddleMovement : MonoBehaviour
{
    public float speed = 5f;
    public KeyCode upKey = KeyCode.W;
    public KeyCode downKey = KeyCode.S;
    public float topLimit = 4.5f;
    public float bottomLimit = -4.5f;

    void Update()
    {
        float movement = 0f;

        if (Input.GetKey(upKey))
        {
            movement = speed * Time.deltaTime;
        }
        else if (Input.GetKey(downKey))
        {
            movement = -speed * Time.deltaTime;
        }

        Vector3 newPosition = transform.position;
        newPosition.y += movement;

        newPosition.y = Mathf.Clamp(newPosition.y, bottomLimit, topLimit);
        transform.position = newPosition;
    }
}