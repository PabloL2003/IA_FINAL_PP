using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAI : MonoBehaviour
{
    public Camera frustum;           // Each zombie's field of view camera
    public LayerMask mask;           // Layer to detect objects
    public Transform player;         // Reference to the player
    public float moveSpeed = 2.0f;   // Movement speed of the zombie

    // Shared static flag: all zombies check if the player is detected
    public static bool playerDetected = false;

    void Update()
    {
        if (playerDetected)
        {
            // Move towards the player if any zombie has detected the player
            MoveTowardsPlayer();
        }
        else
        {
            // Perform vision check to detect the player within the frustum
            DetectPlayer();
        }
    }

    void DetectPlayer()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, frustum.farClipPlane, mask);
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(frustum);

        foreach (Collider col in colliders)
        {
            if (col.gameObject == player.gameObject && GeometryUtility.TestPlanesAABB(planes, col.bounds))
            {
                RaycastHit hit;
                Ray ray = new Ray(transform.position, (player.position - transform.position).normalized);
                ray.origin = ray.GetPoint(frustum.nearClipPlane);

                if (Physics.Raycast(ray, out hit, frustum.farClipPlane, mask))
                {
                    if (hit.collider.gameObject == player.gameObject)
                    {
                        playerDetected = true; // Set static flag to signal all zombies
                        MoveTowardsPlayer();
                        break;
                    }
                }
            }
        }
    }

    void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;


        // Optional: Rotate to face the player
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
    }
}
