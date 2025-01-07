using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of camera movement
    public float rotationSpeed = 30f; // Speed of camera rotation

    void Update()
    {
        // Get input from keyboard
        float horizontal = Input.GetAxis("Horizontal"); // A/D or Left/Right Arrow
        float vertical = Input.GetAxis("Vertical"); // W/S or Up/Down Arrow

        // Create a movement vector
        Vector3 movement = new Vector3(horizontal, 0, vertical) * moveSpeed * Time.deltaTime;

        // Move the camera
        transform.Translate(movement);

        // Adjust the height with Z and X
        if (Input.GetKey(KeyCode.Z))
        {
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.X))
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        }

        // Adjust the rotation with Q and E
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.down * rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }
    }
}
