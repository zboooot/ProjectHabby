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
    float tempHealth;
    float attackCDLeft;

    [SerializeField] private Transform pfBullet;
    private Transform bulletSpawn;

    void Start()
    {
        // Initialize to the initial state (e.g., Idle)
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        tempHealth = enemyData.health;
        currentState = EnemyState.move;
        bulletSpawn = GetComponentInChildren<Transform>();
    }

    public void TakeDamage(float damage)
    {
        tempHealth -= damage;
        if (tempHealth <= 0)
        {
            currentState = EnemyState.death;
        }
        else
        {
            Debug.Log(tempHealth);
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
            Debug.Log(enemyData.enemyName + " is attacking");
        }
    }

    void Death()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

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
