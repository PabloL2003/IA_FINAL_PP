using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drive : MonoBehaviour
{
    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;
    public float currentSpeed = 0;
    public Camera camera;

    void Update()
    {
        // Initialize translation and rotation
        float translation = 0f;
        float rotation = 0f;

        // Check for WASD inputs
        if (Input.GetKey(KeyCode.W))    translation += speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.S))    translation -= speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))    rotation -= rotationSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.D))    rotation += rotationSpeed * Time.deltaTime;

        // Apply movement
        transform.Translate(0, 0, translation);
        currentSpeed = translation;

        // Apply rotation
        transform.Rotate(0, rotation, 0);
    }
}