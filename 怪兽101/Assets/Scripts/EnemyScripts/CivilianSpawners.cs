using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemySpawnInfo
{
    public GameObject enemyPrefab;
    [Range(0, 1)] public float spawnProbability;
}

public class CivilianSpawners : MonoBehaviour
{
    public List<EnemySpawnInfo> enemySpawnInfoList; // List of enemy prefabs with spawn probabilities
    public int numberOfEnemiesToSpawn; // Number of enemies to spawn

    // The minimum and maximum positions where enemies can spawn
    public Vector2 minSpawnPosition;
    public Vector2 maxSpawnPosition;

    void Start()
    {
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        GameObject civilianParent = new GameObject("---Civillian---");
        for (int i = 0; i < numberOfEnemiesToSpawn; i++)
        {
            // Generate a random value between 0 and 1
            float randomValue = Random.Range(0f, 1f);

            // Loop through the enemy spawn info list to find the enemy to spawn based on probabilities
            GameObject enemyPrefabToSpawn = null;
            foreach (var spawnInfo in enemySpawnInfoList)
            {
                if (randomValue <= spawnInfo.spawnProbability)
                {
                    enemyPrefabToSpawn = spawnInfo.enemyPrefab;
                    break; // We found an enemy to spawn, exit the loop
                }
            }

            if (enemyPrefabToSpawn != null)
            {
                // Generate a random spawn position within the specified range
                Vector2 spawnPosition = new Vector2(
                    transform.position.x + Random.Range(minSpawnPosition.x, maxSpawnPosition.x),
                    transform.position.y + Random.Range(minSpawnPosition.y, maxSpawnPosition.y)
                );

                // Spawn the selected enemy prefab at the random position
               GameObject preFabtocreate = Instantiate(enemyPrefabToSpawn, spawnPosition, Quaternion.identity);
                preFabtocreate.transform.parent = civilianParent.transform;
     
            }
        }
    }
}

