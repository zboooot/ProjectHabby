using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameLandmark : MonoBehaviour
{

    public EnemyScriptableObject enemyData;
    private float health;


    [SerializeField] private GameObject pfDelvin;
    public int minEntities = 1; // Minimum number of entities to spawn
    public int maxEntities = 4; // Maximum number of entities to spawn
    public float spawnRadius = 3.0f; // Maximum distance from the current position

    // Start is called before the first frame update
    void Start()
    {
        health = enemyData.health;
    }

    private void SpawnCivilian()
    {
        int numberOfEntities = Random.Range(minEntities, maxEntities + 1);
        for (int i = 0; i < numberOfEntities; i++)
        {
            Vector3 randomDirection = Random.insideUnitCircle.normalized;
            Vector3 spawnPos = transform.position + randomDirection * Random.Range(0.0f, spawnRadius);
            GameObject civilian = Instantiate(pfDelvin, spawnPos, Quaternion.identity);
            //Sets the civilian state upon initialization
            civilian.GetComponent<Civilian>().enemyState = Civilian.EnemyState.fall;

        }

    }

    public void TakeDamage(float damage)
    {
        if(health >= 0)
        {
            health -= damage;
            SpawnCivilian();
        }

        else
        {
            Death();
        }
    }

    private void Death()
    {
        if (gameObject != null)
        {
            Destroy(gameObject, 1f);

        }
        else return;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
