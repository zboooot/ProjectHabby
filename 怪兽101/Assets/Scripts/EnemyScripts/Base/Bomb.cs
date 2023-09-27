using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Bomb : MonoBehaviour
{
    public float fallSpeed = 5f;     // Speed at which the bomb falls
    public int damageAmount = 10;    // Amount of damage the bomb does to the player
    public EnemyScriptableObject enemyData;
    public float destroyTime; // Set the default destroy time in seconds
    private float currentTime = 0f;
    public GameObject explosionVFX;


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
            SpawnExplosion();
            Destroy(gameObject); // Destroy the GameObject
        }
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
