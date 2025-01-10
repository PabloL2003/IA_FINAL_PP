using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of camera movement
    public float rotationSpeed = 30f; // Speed of camera rotation

    void Update()
    {

            Vector3 movement = Vector3.zero;

            // Check for arrow key presses
            if (Input.GetKey(KeyCode.UpArrow))
            {
                movement += Vector3.forward; // Move forward
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                movement += Vector3.back; // Move backward
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                movement += Vector3.left; // Move left
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                movement += Vector3.right; // Move right
            }

            // Move the camera based on input
            transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
        
        
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
