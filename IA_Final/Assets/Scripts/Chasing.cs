using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // Required for NavMeshAgent

public class Chasing : MonoBehaviour
{
    public List<GameObject> chickens; // List of chickens in the scene
    public NavMeshAgent agent; // NavMeshAgent for the fox

    private GameObject target; // Current target chicken

    void Update()
    {
        if (chickens.Count > 0)
        {
            target = FindNearestChicken();

            if (target != null)
            {
                Pursue();
            }
        }
    }

    GameObject FindNearestChicken()
    {
        GameObject nearest = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject chicken in chickens)
        {
            if (chicken != null)
            {
                float distance = Vector3.Distance(transform.position, chicken.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearest = chicken;
                }
            }
        }

        return nearest;
    }

    public void Pursue()
    {
        if (target != null)
        {
            // Set the destination of the NavMeshAgent to the chicken's position
            agent.SetDestination(target.transform.position);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Chicken"))
        {
            Debug.Log("Fox caught a chicken: " + collision.gameObject.name);

            // Remove the chicken from the list and destroy it
            chickens.Remove(collision.gameObject);
            Destroy(collision.gameObject);

            target = null; // Reset the target after catching a chicken
        }
    }
}