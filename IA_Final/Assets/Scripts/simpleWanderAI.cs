using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class WanderAI : MonoBehaviour
{

    public float moveSpeed = 3f;
    public float rotSpeed = 100f;
    private bool isWandering = false;
    private bool isRotatingLeft = false;
    private bool isRotatingRight = false;
    private bool isWalking = false;

    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Debug.Log("Start Wander");
        //transform.position = new Vector3(Random.Range(25.0f, -25.0f), 1, Random.Range(25.0f, -25.0f));
    }

    // Update is called once per frame
    void Update()
    {
        if(isWandering == false)
        {
            StartCoroutine(Wander());
        }

        if(isRotatingRight == true)
        {
            transform.Rotate(transform.up * Time.deltaTime * rotSpeed);
        }
        if(isRotatingLeft == true)
        {
            transform.Rotate(transform.up * Time.deltaTime * -rotSpeed);
        }
        if(isWalking == true)
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }

    }

    IEnumerator Wander()
    {
        int rotTime = Random.Range(1,3);
        int rotateLoR = Random.Range(1,2);
        int walkTime = Random.Range(1,3);

        isWandering = true;
        yield return new WaitForSeconds(0.1f);
        isWalking = true;
        yield return new WaitForSeconds(walkTime);
        isWalking = false;
        yield return new WaitForSeconds(0.1f);
        if(rotateLoR == 1)
        {
            isRotatingRight = true;
            yield return new WaitForSeconds(rotTime);
            isRotatingRight = false;
        }

        if(rotateLoR == 2)
        {
            isRotatingLeft = true;
            yield return new WaitForSeconds(rotTime);
            isRotatingLeft = false;
        }
        isWandering = false;
    }
}
