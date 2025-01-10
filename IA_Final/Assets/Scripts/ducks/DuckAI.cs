using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckAI : MonoBehaviour
{
    public Camera frustum;           // Each duck's field of view camera
    public LayerMask mask;           // Layer to detect objects
    public Transform player;         // Reference to the player
    public float moveSpeed = 2.0f;   // Movement speed of the duck

    // Shared static flag: all ducks check if the player is detected
    public static bool playerDetected = false;

    void Update()
    {
        if (playerDetected)
        {
            // Move away from the player if any duck has detected the player
            MoveAwayFromPlayer();
        }
        else
        {
            // Perform vision check to detect the player within the frustum
            DetectPlayer();
            //MoveAwayFromPlayer();

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
                        playerDetected = true; // Set static flag to signal all ducks
                        break;
                    }
                }
            }
        }
    }

    void MoveAwayFromPlayer()
    {
        Vector3 direction = (transform.position - player.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;

        // Optional: Rotate to face away from the player
        Vector3 lookDirection = transform.position + direction;
        transform.LookAt(new Vector3(lookDirection.x, transform.position.y, lookDirection.z));

        // Check if the player is still detected
        if (!IsPlayerVisible())
        {
            playerDetected = false; // Stop moving away once the player is not detected
        }
    }

    bool IsPlayerVisible()
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
                        return true; // Player is still visible
                    }
                }
            }
        }
        return false; // Player is not visible
    }
}
