using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtilleryBullet : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    public float destroyTime = 10f;
    private float currentTime = 0f;
    public GameObject explosionVFX;
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
        CollateralScript triggerDmg = collision.gameObject.GetComponent<CollateralScript>();

        triggerDmg.CollateralDamage(enemyData.attackDamage);
       
        if (collision.gameObject.tag == "Player")
        {
            GameObject bomb = Instantiate(explosionVFX, collision.transform.position, Quaternion.identity);
            collision.gameObject.GetComponent<PlayerHealthScript>().TakeDamage(enemyData.attackDamage);
            Destroy(gameObject);
        }
      
    }
}