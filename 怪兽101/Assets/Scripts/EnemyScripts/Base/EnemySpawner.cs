using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    public float CityDestructionLevel;
    public LevelManagerScriptableObject levelData;
    
    public Transform playerTransform;
    public float playerSpawnRadius = 10f;



    public TMP_Text Waveno;

    public List<GameObject> spawnedEnemies = new List<GameObject>();
    public List<SpawnedEnemy> separateSpawnedEnemies = new List<SpawnedEnemy>();

    public LevelManager LevelManagerScript;

    // ...


    // The minimum and maximum positions where enemies can spawn
    public Vector2 minSpawnPosition;
    public Vector2 maxSpawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        CityDestructionLevel = levelData.destructionLevel;
      
        GenerateWave();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
          CityDestructionLevel = levelData.destructionLevel;

        // Check if the CityDestructionLevel and currWave meet the conditions for spawning
        if (!((CityDestructionLevel == 0 && currWave >= 1 && currWave <= 3) ||
              (CityDestructionLevel == 1 && currWave >= 4 && currWave <= 6) ||
              (CityDestructionLevel == 2 && currWave >= 7 && currWave <= 10)))
        {
            // CityDestructionLevel and currWave conditions are not met, so skip to the lowest wave in the next city destruction level.
            if (CityDestructionLevel == 0)
            {
                currWave = 1;
            }
            else if (CityDestructionLevel == 1)
            {
                currWave = 4;
            }
            else if (CityDestructionLevel == 2)
            {
                currWave = 7;
            }

            GenerateWave(); // Generate the next wave
            return;
        }

        if (currWave < NumberofWaves && spawnTimer <= 0 && spawnedEnemies.Count == 0)
        {
            // Check if there are enemies to spawn
            if (enemiesToSpawn.Count > 0)
            {
                // Loop through the enemies to spawn and spawn them all
                foreach (GameObject enemyPrefab in enemiesToSpawn)
                {
                    // Choose a random spawn position within the specified range
                    Vector2 randomSpawnPosition = GetRandomSpawnPosition();
                    GameObject enemy = Instantiate(enemyPrefab, randomSpawnPosition, Quaternion.identity);
                    spawnedEnemies.Add(enemy);
                }

                // Clear the enemiesToSpawn list
                enemiesToSpawn.Clear();

                // Reset the spawn timer
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

        Waveno.text = "Wave " + currWave;
    }

    Vector2 GetRandomSpawnPosition()
    {
        Vector2 randomSpawnPosition;
        do
        {
            // Generate a random position within the specified range
            randomSpawnPosition = new Vector2(
                Random.Range(minSpawnPosition.x, maxSpawnPosition.x),
                Random.Range(minSpawnPosition.y, maxSpawnPosition.y)
            );
        } while (Vector2.Distance(randomSpawnPosition, playerTransform.position) < playerSpawnRadius);

        return randomSpawnPosition;
    }

    public void GenerateWave()
    {
        // Determine the wave range based on the CityDestructionLevel
        int minWave = 1;
        int maxWave = 3; // Default range for CityDestructionLevel 0

        if (levelData.destructionLevel == 1)
        {
            minWave = 4;
            maxWave = 6;
        }

        if (levelData.destructionLevel == 2)
        {
            minWave = 7;
            maxWave = 10;
        }

        // Ensure that currWave stays within the specified range
        if (currWave < minWave || currWave > maxWave)
        {
            currWave = minWave;
        }

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
        spawnInterval = 1.0f; // Example: Spawn an enemy every  seconds
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