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
        if (tempHealth == 0)
        {
            Debug.Log("Building Health:" + tempHealth);
            SpawnCoin();
            Death();
        }
        else return;
    }

    void Death()
    {
        Debug.Log("Boom Doomz Goomz CRASH");
        buildingCollider.enabled = false;
        spriteRenderer.sprite = destroyedSprite;
    }
    private void SpawnCoin()
    {
        //Spawn GNA
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        GameObject coin = Instantiate(pfCoin, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
