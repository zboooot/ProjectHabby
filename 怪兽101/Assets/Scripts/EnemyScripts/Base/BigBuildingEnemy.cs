using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBuildingEnemy : MonoBehaviour
{
    public EnemyScriptableObject SO_enemy;
    float tempHealth;
    private SpriteRenderer spriteRenderer;
    public Sprite destroyedSprite;
    public Targetable buildingType;
    private Collider2D buildingCollider;

    [SerializeField] private GameObject pfCoin;

    [SerializeField] private GameObject pfDelvin;
    public int minEntities = 1; // Minimum number of entities to spawn
    public int maxEntities = 4; // Maximum number of entities to spawn
    public float spawnRadius = 3.0f; // Maximum distance from the current position

    // Start is called before the first frame update
    void Start()
    {
        tempHealth = SO_enemy.health;
        spriteRenderer = GetComponent<SpriteRenderer>();
        buildingCollider = GetComponent<BoxCollider2D>();
    }

    public void TakeDamage(float damage)
    {
        tempHealth -= damage;
        SpawnCivilian();

        if (tempHealth == 0)
        {
            SpawnCoin();
            Death();
        }
        else return;
    }

    void Death()
    {
        buildingCollider.enabled = false;
        spriteRenderer.sprite = destroyedSprite;
    }
    private void SpawnCoin()
    {
        //Spawn GNA
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        GameObject coin = Instantiate(pfCoin, transform.position, Quaternion.identity);
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

    // Update is called once per frame
    void Update()
    {
    }
}
