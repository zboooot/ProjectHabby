using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBuildingEnemy : MonoBehaviour
{
    public EnemyScriptableObject SO_enemy;
    float tempHealth;
    private SpriteRenderer spriteRenderer;
    public Sprite damagedSprite;
    public Sprite destroyedSprite;
    public Targetable buildingType;
    private Collider2D buildingCollider;
    public PlayerInputHandler inputHandler;
    public GameObject civilianParent;
    

    [SerializeField] private GameObject pfCoin;

    [SerializeField] private GameObject pfDelvin;
    public int minEntities = 1; // Minimum number of entities to spawn
    public int maxEntities = 4; // Maximum number of entities to spawn
    public float spawnRadius = 3.0f; // Maximum distance from the current position

    private float hitDarkeningAmount = 0.6f; // Amount to darken the sprite on each hit
    private float minDarkness = 0.2f; // Minimum darkness level
    
    private Color originalColor;

    private PlayerScoreScript playerScore;
    private ShakeScript shakeScript;
    private OrbManager orbManager;

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
        shakeScript = GetComponent<ShakeScript>();
        orbManager = GetComponent<OrbManager>();
        originalColor = spriteRenderer.color;
        civilianParent = GameObject.Find("---Civillian---");
       
    }

    public void TakeDamage(float damage)
    {
        tempHealth -= damage;
        spriteRenderer.sprite = damagedSprite;
        shakeScript.StartShake();
        SpawnCivilian();
        DamageEffect();

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
        spriteRenderer.sortingLayerName = "Default";
        spriteRenderer.sortingOrder = 2;
        spriteRenderer.color = originalColor;
    }

    public void IntroDeath()
    {
        buildingCollider.enabled = false;
        spriteRenderer.sprite = destroyedSprite;
    }

    private void SpawnCoin()
    {
        int numberOfEntities = Random.Range(minEntities, maxEntities + 1);
        for (int i = 0; i < numberOfEntities; i++)
        {
            //Spawn GNA
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            GameObject coin = Instantiate(pfCoin, transform.position, Quaternion.identity);
            coin.transform.Rotate(0, 0, 90);
        }

        //Spawn Orbs
        orbManager.DropOrbsOnKill();
    }

    void DamageEffect()
    {
        Color currentColor = spriteRenderer.color;

        // Reduce the brightness of the sprite by the specified amount
        currentColor.r = Mathf.Max(minDarkness, currentColor.r - hitDarkeningAmount);
        currentColor.g = Mathf.Max(minDarkness, currentColor.g - hitDarkeningAmount);
        currentColor.b = Mathf.Max(minDarkness, currentColor.b - hitDarkeningAmount);

        // Apply the new color to the sprite
        spriteRenderer.color = currentColor;
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
            civilian.transform.SetParent(civilianParent.transform);
           
        }
        
    }

}
