using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finite : MonoBehaviour
{

    public float moveSpeed = 3.0f; // Speed of the character's movement
    public float turnSpeed = 200.0f; // Speed of the character's rotation
    public float directionChangeInterval = 1.5f; // How often the character changes direction
    private float wanderDuration = 15.0f; // Duration of wandering in seconds
    public Transform targetObject; // The object to move to after wandering
    public float waitTimeAtTarget = 5.0f; // Time to wait at the target

    private float timeElapsed = 0f;
    private float directionChangeTimer = 0f;
    private Quaternion targetRotation;
    private bool isMovingToTarget = false;
    private bool isWaiting = false;
    private float waitTimer = 0f;

    void Start()
    {
        // Set the initial random direction
        ChangeDirection();
    }

    void Update()
    {
        if (isWaiting)
        {
            // Handle waiting at the target
            waitTimer += Time.deltaTime;

            if (waitTimer >= waitTimeAtTarget)
            {
                isWaiting = false;
                timeElapsed = 0f; // Reset wander timer to resume wandering

                if (targetObject != null)
                {
                    targetObject.gameObject.SetActive(true);
                }
            }
        }
        else if (isMovingToTarget && targetObject != null)
        {
            MoveToTarget(targetObject);

            // Check if the character has reached the target
            if (Vector3.Distance(transform.position, targetObject.position) < 0.1f)
            {
                isMovingToTarget = false;
                isWaiting = true;
                waitTimer = 0f;

                // Hide the target object
                targetObject.gameObject.SetActive(false);
            }
        }
        else if (timeElapsed < wanderDuration)
        {
            // Update timers
            timeElapsed += Time.deltaTime;
            directionChangeTimer += Time.deltaTime;

            // Change direction if the timer exceeds the interval
            if (directionChangeTimer >= directionChangeInterval)
            {
                ChangeDirection();
                directionChangeTimer = 0f;
            }

            // Move the character forward
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

            // Smoothly rotate to the target direction
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }
        else if (!isMovingToTarget && targetObject != null)
        {
            // Start moving toward the target object after wandering duration
            isMovingToTarget = true;
        }
    }

    private void ChangeDirection()
    {
        // Generate a random rotation
        float randomYRotation = Random.Range(0, 360);
        targetRotation = Quaternion.Euler(0, randomYRotation, 0);
    }

    private void MoveToTarget(Transform target)
    {
        // Move towards the target object
        Vector3 directionToTarget = (target.position - transform.position).normalized;
        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

        // Rotate to face the target
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, turnSpeed * Time.deltaTime);
    }
}
