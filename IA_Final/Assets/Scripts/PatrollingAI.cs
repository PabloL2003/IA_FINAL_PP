using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingAI : MonoBehaviour
{
    public Transform[] waypoints;  // Array de puntos de patrullaje
    public float moveSpeed = 3f;
    public float waitTime = 15f;
    private int currentWaypointIndex = 0;

    void Start()
    {
        // Start the patrol coroutine
        StartCoroutine(Patrol());
    }

    IEnumerator Patrol()
    {
        while (true)
        {
            // Visualize the forward direction of the bot
            UnityEngine.Debug.DrawRay(this.transform.position, this.transform.forward * 4, Color.blue);

            // Get the target waypoint
            Transform targetWaypoint = waypoints[currentWaypointIndex];

            // Move towards the target waypoint
            while (Vector3.Distance(transform.position, targetWaypoint.position) > 0.5f)
            {
                // Move the object towards the target waypoint
                transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, moveSpeed * Time.deltaTime);

                // Calculate the direction to the next waypoint and rotate smoothly
                Vector3 directionToWaypoint = targetWaypoint.position - transform.position;
                directionToWaypoint.y = 0; // Keep the movement on the same horizontal plane

                if (directionToWaypoint != Vector3.zero) // Avoid errors if the direction is zero
                {
                    Quaternion targetRotation = Quaternion.LookRotation(directionToWaypoint);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, moveSpeed * Time.deltaTime);
                }

                yield return null; // Wait until next frame
            }

            // Wait at the waypoint for the specified time
            yield return new WaitForSeconds(waitTime);

            // Move to the next waypoint (looping back to the first one when we reach the end)
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }
}