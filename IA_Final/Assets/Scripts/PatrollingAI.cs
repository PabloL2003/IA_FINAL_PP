using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PatrollingAI : MonoBehaviour
{
    public Transform[] waypoints;  // Array de puntos de patrullaje
    public float moveSpeed = 3f;
    public float waitTime = 1f;
    private int currentWaypointIndex = 0;

    public GameObject follower;

    public bool showGhost = true;

    void Start()
    {
        if (!showGhost) this.GetComponent<MeshRenderer>().enabled = false;

        StartCoroutine(Patrol());

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(waypoints[currentWaypointIndex].position), Time.deltaTime * moveSpeed);

        follower = Instantiate(follower, new Vector3(transform.position.x, transform.position.y -6, transform.position.z), transform.rotation);
    }

    IEnumerator Patrol()
    {
        while (true)
        {
            // Visualize the forward direction of the bot
            UnityEngine.Debug.DrawRay(this.transform.position, this.transform.forward * 4, Color.blue);

            // Mover hacia el waypoint actual
            Transform targetWaypoint = waypoints[currentWaypointIndex];
            while (Vector3.Distance(transform.position, targetWaypoint.position) > 0.5f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, moveSpeed * Time.deltaTime);
                follower.transform.position = Vector3.MoveTowards(follower.transform.position, transform.position, moveSpeed * Time.deltaTime);

                // Calculate the rotation
                Quaternion targetRotation = Quaternion.LookRotation(targetWaypoint.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, moveSpeed * Time.deltaTime);
                follower.transform.rotation = transform.rotation;
                
                yield return null; // Espera al siguiente frame
            }

            // Esperar en el waypoint
            yield return new WaitForSeconds(waitTime);

            // Cambiar al siguiente waypoint
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }
}
