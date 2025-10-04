using UnityEngine;

public class PaddleMovement : MonoBehaviour
{
    public float speed = 5f;
    public KeyCode upKey = KeyCode.W;
    public KeyCode downKey = KeyCode.S;

    // Limite de movimiento (para que no salga de la pantalla)
    public float topLimit = 4.5f;
    public float bottomLimit = -4.5f;

    void Update()
    {
        // Variable para guardar el movimiento
        float movement = 0f;
        if (Input.GetKey(upKey))
        {
            movement = speed * Time.deltaTime;
        }
        else if (Input.GetKey(downKey))
        {
            movement = -speed * Time.deltaTime;
        }

        // Aplicar el movimiento a la posicion actual
        Vector3 newPosition = transform.position;
        newPosition.y += movement;

        // Limitar el movimiento para que no salga de la pantalla
        newPosition.y = Mathf.Clamp(newPosition.y, bottomLimit, topLimit);

        // Actualizar la posicion de la raqueta
        transform.position = newPosition;
    }
}