using UnityEngine;

public class Demo_moving : MonoBehaviour
{
    public float speed = 5f; // Speed of the movement

    void Update()
    {
        // Get input from arrow keys
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Calculate movement direction based on input
        Vector3 movementDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // Move the GameObject based on the movement direction and speed
        transform.Translate(movementDirection * speed * Time.deltaTime);
    }
}
