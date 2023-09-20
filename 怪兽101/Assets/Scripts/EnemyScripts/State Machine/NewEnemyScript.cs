using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEnemyScript : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    public enum EnemyState { move, attack, death }
    public EnemyState currentState;

    public Transform player;
    private Transform playerLastLoc;
    private SpriteRenderer spriteRenderer;
    public Sprite upSprite;
    public Sprite downSprite;
    public Sprite leftSprite;
    public Sprite destroyedSprite;
    float tempHealth;
    float attackCDLeft;
    private Collider2D entityCollider;

    [SerializeField] private Transform pfBullet;
    private Transform bulletSpawn;

    void Start()
    {
        // Initialize to the initial state (e.g., Idle)
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        tempHealth = enemyData.health;
        currentState = EnemyState.move;
        bulletSpawn = GetComponentInChildren<Transform>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        entityCollider = GetComponent<Collider2D>();
    }

    public void TakeDamage(float damage)
    {
        tempHealth -= damage;
        Debug.Log("Tank health remaining:" + tempHealth);
        if (tempHealth <= 0)
        {
            currentState = EnemyState.death;
        }
    }

    void CheckState(float dis)
    {
        if (currentState != EnemyState.death)
        {
            if (dis <= enemyData.attackRange)
            {
                // Attack the player
                currentState = EnemyState.attack;
            }
            else
            {
                // Move towards the player
                currentState = EnemyState.move;
            }
        }

        else return;
    }

    void MovetoPlayer()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        transform.Translate(direction * enemyData.speed * Time.deltaTime);
    }

    void Attack()
    {
        attackCDLeft -= Time.deltaTime;
        if(attackCDLeft <= 0f)
        {
            playerLastLoc = player.transform;
            attackCDLeft = enemyData.attackSpeed;
            Transform bulletTransform = Instantiate(pfBullet, bulletSpawn.position, Quaternion.identity);
            Vector3 shootDir = (playerLastLoc.position - bulletSpawn.position).normalized;
            bulletTransform.GetComponent<Bullet>().SetUp(shootDir);
        }
    }

    void Death()
    {
        entityCollider.enabled = false;
        spriteRenderer.sprite = destroyedSprite;
        Destroy(gameObject, 5f);
    }

    void CheckFlip()
    {
        Vector3 direction = player.position - transform.position;

        // Check the direction and set the sprite accordingly.
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {

            spriteRenderer.sprite = leftSprite;

            if (direction.x > 0)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
        }
        else
        {
            if (direction.y > 0)
            {
                spriteRenderer.sprite = upSprite;
            }
            else
            {
                spriteRenderer.sprite = downSprite;
            }
        }

    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        CheckFlip();
        CheckState(distanceToPlayer);

        switch (currentState)
        {
            case EnemyState.death:
                Death();
                break;

            case EnemyState.attack:
                Attack();
                break;

            case EnemyState.move:
                MovetoPlayer();
                break;

        }

    }
}
