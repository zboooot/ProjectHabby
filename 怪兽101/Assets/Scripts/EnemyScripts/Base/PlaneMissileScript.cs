using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMissileScript : MonoBehaviour
{
    public float missilespeed = 20f;     // Speed at which the missile moves
    public int damageAmount = 10;    // Amount of damage the bomb does to the player
    public EnemyScriptableObject enemyData;
    public float destroyTime; // Set the default destroy time in seconds
    private float currentTime = 0f;
    public GameObject explosionVFX;
    public Transform jetPos;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Set initial velocity to move the missile forward
        CheckAndFire();

    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        // Check if the current time exceeds the specified destroy time
        if (currentTime >= destroyTime)
        {
            SpawnExplosion();
            Destroy(gameObject); // Destroy the GameObject
        }
    }

    void CheckAndFire()
    {
        if (transform.position.x > jetPos.position.x)
        {
            spriteRenderer.flipX = true;
            GetComponent<Rigidbody2D>().velocity = new Vector2(-missilespeed, 0f);
        }
        else if(transform.position.x < jetPos.position.x)
        {
            spriteRenderer.flipX = false;
            GetComponent<Rigidbody2D>().velocity = new Vector2(missilespeed, 0f);
        }
        else { return; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the bomb collided with the player
        if (collision.CompareTag("Player"))
        {
            SpawnExplosion();
            collision.GetComponent<PlayerHealthScript>().TakeDamage(enemyData.attackDamage);

            Destroy(gameObject);
        }
    }

    void SpawnExplosion()
    {
        GameObject bomb = Instantiate(explosionVFX, transform.position, Quaternion.identity);
    }
}
