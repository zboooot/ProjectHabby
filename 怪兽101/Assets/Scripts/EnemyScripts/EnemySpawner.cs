using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<Enemy> enemies = new List<Enemy>();
    public int currWave;
    private int waveValue;
    public List<GameObject> enemiesToSpawn = new List<GameObject>();

    public int waveDuration;
    private float waveTimer;
    private float spawnInterval;
    private float spawnTimer;
    public int pointsforSpawning = 0;
    public int numberofEnemies;
    public int NumberofWaves;
    private bool roundComplete;

    public List<GameObject> spawnedEnemies = new List<GameObject>();
    public List<SpawnedEnemy> separateSpawnedEnemies = new List<SpawnedEnemy>();

    // ...


    // The minimum and maximum positions where enemies can spawn
    public Vector2 minSpawnPosition;
    public Vector2 maxSpawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        roundComplete = false;
        GenerateWave();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (currWave < NumberofWaves && spawnTimer <= 0)
        {
            // Spawn an enemy
            if (enemiesToSpawn.Count > 0)
            {
                // Choose a random spawn position within the specified range
                Vector2 randomSpawnPosition = new Vector2(
                    Random.Range(minSpawnPosition.x, maxSpawnPosition.x),
                    Random.Range(minSpawnPosition.y, maxSpawnPosition.y)
                );

                GameObject enemy = Instantiate(enemiesToSpawn[0], randomSpawnPosition, Quaternion.identity);
                enemiesToSpawn.RemoveAt(0);
                spawnedEnemies.Add(enemy);
                spawnTimer = spawnInterval;
            }
            else
            {
                waveTimer = 0;
            }
        }
        else
        {
            spawnTimer -= Time.fixedDeltaTime;
            waveTimer -= Time.fixedDeltaTime;
        }

        // Check for missing enemies and remove them from the spawnedEnemies list
        spawnedEnemies.RemoveAll(enemy => enemy == null);

        // Check if the wave is complete (all enemies of this wave are destroyed)
        if (waveTimer <= 0 && spawnedEnemies.Count == 0)
        {
            currWave++;
            GenerateWave();
        }
    }

    public void GenerateWave()
    {
        switch (currWave)
        {
            case 1:
                waveValue = 10;
                break;
            case 2:
                waveValue = 20;
                break;
            case 3:
                waveValue = 30;
                break;
            case 4:
                waveValue = 40;
                break;
            case 5:
                waveValue = 50;
                break;
            case 6:
                waveValue = 60;
                break;
            case 7:
                waveValue = 70;
                break;
            case 8:
                waveValue = 80;
                break;
            case 9:
                waveValue = 90;
                break;
            case 10:
                waveValue = 100;
                break;
            default:
                // Set a default wave value for other waves
                waveValue = 10; // Change this value as needed
                break;
        }
        pointsforSpawning = waveValue;

        GenerateEnemies();

        // Calculate spawn interval based on your desired rate
        spawnInterval = 1.0f; // Example: Spawn an enemy every 3 seconds
        waveTimer = waveDuration;
    }

    public void GenerateEnemies()
    {
        List<SpawnedEnemy> generatedEnemies = new List<SpawnedEnemy>();

        while (waveValue > 0 || generatedEnemies.Count < 50)
        {
            int randEnemyId = Random.Range(0, enemies.Count);
            int randEnemyCost = enemies[randEnemyId].cost;

            if (waveValue - randEnemyCost >= 0)
            {
                GameObject enemyPrefab = enemies[randEnemyId].enemyPrefab;

                // Check if this enemy type already exists in the list
                SpawnedEnemy existingEnemy = generatedEnemies.Find(e => e.enemyPrefab == enemyPrefab);

                if (existingEnemy != null)
                {
                    // If it exists, increment the count
                    existingEnemy.count++;
                }
                else
                {
                    // If it doesn't exist, add it to the list
                    generatedEnemies.Add(new SpawnedEnemy
                    {
                        enemyPrefab = enemyPrefab,
                        count = 1
                    });

                    generatedEnemies.Remove(new SpawnedEnemy
                    {
                        enemyPrefab = enemyPrefab,
                        count = -1
                    });
                }

                waveValue -= randEnemyCost;
            }
            else if (waveValue <= 0)
            {
                break;
            }
        }

        enemiesToSpawn.Clear();

        // Convert the List of SpawnedEnemy to a List of GameObjects
        foreach (SpawnedEnemy spawnedEnemy in generatedEnemies)
        {
            for (int i = 0; i < spawnedEnemy.count; i++)
            {
                enemiesToSpawn.Add(spawnedEnemy.enemyPrefab);
            }
        }
    }


    [System.Serializable]
    public class Enemy
    {
        public GameObject enemyPrefab;
        public int cost;
    }

    [System.Serializable]
    public class SpawnedEnemy
    {
        public GameObject enemyPrefab;
        public int count;
    }
}