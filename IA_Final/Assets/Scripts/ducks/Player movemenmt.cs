using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Playermovemenmt : MonoBehaviour
{

    public Camera cam;
    public float rotationx = 0f;
    public float rotationy = 0f;


    public void PlayerMovement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * 0.01f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * 0.01f;
        } if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * 0.01f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= transform.right * 0.01f;
        }

    }


    void Start()
    {
        
    }


    void Update()
    {
        PlayerMovement();
        
    }
}
