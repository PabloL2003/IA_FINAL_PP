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

    public void CameraRotation()
    {
        float lookspeed = 3;
        float mousex = Input.GetAxis("Mouse X");
        float mousey = Input.GetAxis("Mouse Y");

        mousex = mousex * lookspeed;
        mousey = mousey * lookspeed;

        rotationx -= mousey;
        rotationy = Mathf.Clamp(rotationx,-50,50);
        cam.transform.localRotation = Quaternion.Euler(rotationx, 0, 0);
        transform.Rotate(Vector3.up * mousex);



    }


    void Start()
    {
        
    }


    void Update()
    {
        PlayerMovement();
        CameraRotation();
    }
}
