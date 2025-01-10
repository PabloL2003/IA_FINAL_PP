using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckSpawner : MonoBehaviour
{
    public GameObject duckPrefab;     // Reference to the zombie prefab
    public Transform player;            // Reference to the player
    public int DuckCount = 10;        // Number of zombies to spawn
    public float spawnRadius = 10f;     // Radius around the spawner where zombies will be placed

    void Start()
    {
        for (int i = 0; i < DuckCount; i++)
        {
            // Spawn each zombie at a random position around the spawner
            Vector3 spawnPosition = transform.position + Random.insideUnitSphere * spawnRadius;
            spawnPosition.y = 2; // Ensure zombies are on the ground

            GameObject zombie = Instantiate(duckPrefab, spawnPosition, Quaternion.identity);
            ZombieAI zombieAI = zombie.GetComponent<ZombieAI>();
            zombieAI.player = player; // Assign the player to each zombie
        }
    }
}
