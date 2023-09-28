using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtilleryBullet : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    public float destroyTime = 10f;
    private float currentTime = 0f;
    private void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= destroyTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerHealthScript>().TakeDamage(enemyData.attackDamage);
            Destroy(gameObject);
        }
    }
}