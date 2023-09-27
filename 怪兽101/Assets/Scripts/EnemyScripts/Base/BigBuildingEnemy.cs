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
    public PlayerInputHandler inputHandler;

    [SerializeField] private GameObject pfCoin;

    [SerializeField] private GameObject pfDelvin;
    public int minEntities = 1; // Minimum number of entities to spawn
    public int maxEntities = 4; // Maximum number of entities to spawn
    public float spawnRadius = 3.0f; // Maximum distance from the current position

    private PlayerScoreScript playerScore;

    float pushForce = 2f;
    float upForce = 5f;

    // Start is called before the first frame update
    void Start()
    {
        tempHealth = SO_enemy.health;
        spriteRenderer = GetComponent<SpriteRenderer>();
        buildingCollider = GetComponent<BoxCollider2D>();
        playerScore = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScoreScript>();
        inputHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInputHandler>();
    }

    public void TakeDamage(float damage)
    {
        tempHealth -= damage;
        SpawnCivilian();

        if (tempHealth <= 0)
        {
            SpawnCoin();
            Death();
        }
        else return;
    }

    void Death()
    {
        if (playerScore != null)
        {
            playerScore.EntityDestroyed();
        }

        inputHandler.ChargeUltimate(10);
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
            civilian.GetComponent<Rigidbody2D>().AddForce(Vector2.up * upForce, ForceMode2D.Impulse);
            civilian.GetComponent<Rigidbody2D>().AddForce(randomDirection * pushForce, ForceMode2D.Impulse);
            //Sets the civilian state upon initialization
            civilian.GetComponent<Civilian>().enemyState = Civilian.EnemyState.fall;

        }
        
    }

}
