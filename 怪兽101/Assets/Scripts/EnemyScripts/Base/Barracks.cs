using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks : MonoBehaviour
{
    public SpawnerScriptableObject spawnerData;
    public Transform spawnLoc;
    private float timeSinceLastSpawn = 0f;

    // Start is called before the first frame update
    void Start()
    {
        

    }
    void SpawnEnemy()
    {
        // Instantiate the enemy at the chosen spawn point.
        Instantiate(spawnerData.spawnedEntity, spawnLoc.position, spawnLoc.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        // Check if it's time to spawn a new enemy.
        if (timeSinceLastSpawn >= spawnerData.spawnRate)
        {
            SpawnEnemy();
            timeSinceLastSpawn = 0f; // Reset the timer.
        }
    }
}
