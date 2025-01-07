using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIVision : MonoBehaviour
{
    public Camera frustum;
    public LayerMask mask;
    public GameObject prefab;

    private NavMeshAgent agent;
    private static List<AIVision> allPolice = new List<AIVision>(); // Static list of all zombies

    void Awake()
    {
        // Add this instance to the static list
        allPolice.Add(this);
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        Vector3 randomPosition = new Vector3(Random.Range(-30.0f, 30.0f), 0, Random.Range(-30.0f, 30.0f));
        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomPosition, out hit, 1.0f, NavMesh.AllAreas))
        {
            transform.position = hit.position; // Set the position to the closest valid NavMesh point
        }
    }

    void Update()
    {
        if (agent.isActiveAndEnabled && agent.isOnNavMesh)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, frustum.farClipPlane, mask);
            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(frustum);

            foreach (Collider col in colliders)
            {
                if (col.gameObject != gameObject && GeometryUtility.TestPlanesAABB(planes, col.bounds))
                {
                    RaycastHit hit;
                    Ray ray = new Ray();
                    ray.origin = transform.position;
                    ray.direction = (col.transform.position - transform.position).normalized;
                    ray.origin = ray.GetPoint(frustum.nearClipPlane);

                    if (Physics.Raycast(ray, out hit, frustum.farClipPlane, mask))
                    {
                        Debug.Log(hit.collider.gameObject.tag);
                        if (hit.collider.gameObject.CompareTag("Player"))
                        {
                            // Set the destination for this zombie
                            agent.SetDestination(hit.collider.gameObject.transform.position);

                            // Notify other zombies
                            BroadcastMessageToAllZombies(hit.collider.gameObject.transform.position);
                        }
                    }
                }
            }
        }
        else
        {
            Debug.LogWarning("Agent is not active or not on NavMesh");
        }
    }

    void BroadcastMessageToAllZombies(Vector3 playerPosition)
    {
        foreach (var cop in allPolice)
        {
            if (cop != this) // Don't notify itself
            {
                cop.OnPlayerDetected(playerPosition);
            }
        }
    }

    void OnPlayerDetected(Vector3 playerPosition)
    {
        // Set the destination or perform other actions
        agent.SetDestination(playerPosition);
    }
}
