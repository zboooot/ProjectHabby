using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBuildingEnemy : MonoBehaviour
{
    public EnemyScriptableObject SO_enemy;
    public float tempHealth;
    public SpriteRenderer spriteRenderer;
    public Sprite damagedSprite;
    public Targetable buildingType;
    private Collider2D buildingCollider;
    public PlayerHandler inputHandler;
    public GameObject civilianParent;

    //VFX
    public GameObject fireVFX;
    public GameObject deathVFX;
    public GameObject damageVFX;
    public GameObject smokeVFX;
    public GameObject hitVFX;
    public GameObject pointIndicatorVFX;
    private GameObject fireHandler;

    public GameObject destroyedBuilding;
    private LevelManager levelManager;
    private Vector3 targetScale = new Vector3(2f, 0, 0);

    [SerializeField] private GameObject pfCoin;

    [SerializeField] private GameObject pfDelvin;
    public int minEntities = 1; // Minimum number of entities to spawn
    public int maxEntities = 4; // Maximum number of entities to spawn
    public float spawnRadius = 3.0f; // Maximum distance from the current position

    private PlayerScoreScript playerScore;
    public ShakeScript shakeScript;

    bool isOnFire;

    public Vector2 groundDispenseVelocity;
    public Vector2 verticalDispenseVelocity;

    // Start is called before the first frame update
    void Start()
    {
        tempHealth = SO_enemy.health;
        buildingCollider = GetComponent<BoxCollider2D>();
        playerScore = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PlayerScoreScript>();
        inputHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHandler>();
        civilianParent = GameObject.Find("---Civillian---");
        levelManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<LevelManager>();
        
    }

    public void TakeDamage(float damage)
    {
        tempHealth -= damage;
        shakeScript.StartShake();
        SpawnCivilian();
        DamageEffect();

        if (isOnFire != true)
        {
            SpawnFire();
        }

        if (tempHealth <= 0)
        {
            inputHandler.ChargeUltimate(10);
            Death();
        }
        else return;
    }

    void SpawnFire()
    {
        GameObject fireAnim = Instantiate(fireVFX, transform.position, Quaternion.identity);
        fireHandler = fireAnim;
        isOnFire = true;
    }

    public void Death()
    {
        Destroy(fireHandler);
        TriggerLoot();
        buildingCollider.enabled = false;
        spriteRenderer.enabled = false;
        Vector2 explosionLoc = new Vector2(transform.position.x, transform.position.y + 1.5f);
        GameObject explosion = Instantiate(deathVFX, explosionLoc, Quaternion.identity);
        GameObject smoke = Instantiate(smokeVFX, transform.position, Quaternion.identity);
        GameObject rubble = Instantiate(destroyedBuilding, transform.position, Quaternion.identity);
        Destroy(smoke, 1.5f);
        Destroy(gameObject, 10f);
    }

    void TriggerLoot()
    {
        if (playerScore != null)
        {
            playerScore.EntityDestroyed();
        }

        int numberOfEntities = Random.Range(minEntities, maxEntities + 1);
        for (int i = 0; i < numberOfEntities; i++)
        {
            //Spawn GNA
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            GameObject coin = Instantiate(pfCoin, transform.position, Quaternion.identity);
            coin.transform.Rotate(0, 0, 90);
        }
        //Add points
        levelManager.CalculateScore(5);
        GameObject pointVFX = Instantiate(pointIndicatorVFX, transform.position, Quaternion.Euler(0f,0f,0f));
    }

    void DamageEffect()
    {
        GameObject hit = Instantiate(damageVFX, transform.position, Quaternion.identity);
        GameObject hitEffect = Instantiate(hitVFX, transform.position, Quaternion.identity);
        Destroy(hit, 1f);
    }

    private void SpawnCivilian()
    {
        int numberOfEntities = Random.Range(minEntities, maxEntities + 1);
        for (int i = 0; i < numberOfEntities; i++)
        {
            Vector3 randomDirection = Random.insideUnitCircle.normalized;
            Vector3 spawnPos = transform.position + randomDirection * Random.Range(0.0f, spawnRadius);
            GameObject civilian = Instantiate(pfDelvin, spawnPos, Quaternion.identity);
            civilian.GetComponent<FakeHeightScript>().Initialize(Random.insideUnitCircle * Random.Range(groundDispenseVelocity.x, groundDispenseVelocity.y), Random.Range(verticalDispenseVelocity.x, verticalDispenseVelocity.y));
  
            //Sets the civilian state upon initialization
            civilian.GetComponentInChildren<Civilian>().enemyState = Civilian.EnemyState.fall;
            civilian.transform.SetParent(civilianParent.transform);

        }

    }

}