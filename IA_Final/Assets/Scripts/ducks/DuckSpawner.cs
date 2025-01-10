using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckSpawner : MonoBehaviour
{
    public GameObject duckPrefab;     // Reference to the duck prefab
    public Transform player;            // Reference to the player
    public int duckCount = 10;         // Number of ducks to spawn
    public float spawnRadius = 5f;      // Radius around the spawner where ducks will be placed

    void Start()
    {
        for (int i = 0; i < duckCount; i++)
        {
            // Spawn each duck at a random position within the radius
            Vector3 spawnPosition = transform.position + Random.insideUnitSphere * spawnRadius;
            spawnPosition.y = transform.position.y; // Keep ducks on the same height as the spawner

            Instantiate(duckPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
