using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float fallSpeed = 5f;     // Speed at which the bomb falls
    public int damageAmount = 10;    // Amount of damage the bomb does to the player
    public EnemyScriptableObject enemyData;
    public float destroyTime = 2f; // Set the default destroy time in seconds
    private float currentTime = 0f;

    private void Start()
    {
        // Set initial velocity to move the bomb downward
        GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -fallSpeed);
    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        // Check if the current time exceeds the specified destroy time
        if (currentTime >= destroyTime)
        {

            Destroy(gameObject); // Destroy the GameObject
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the bomb collided with the player
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerHealthScript>().TakeDamage(enemyData.attackDamage);
            Destroy(gameObject);
        }
    }

    void SpawnExplosion()
    {

    }
}
