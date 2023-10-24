using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtilleryBullet : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    public GameObject explosionVFX;

    private CircularIndicator storedData;
    private Collider2D entityCollider;

    private void Start()
    {
        entityCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        CheckTrigger();

    }

    void CheckTrigger()
    {
        if (storedData.isInRange)
        {
            entityCollider.enabled = true;
        }

        else
        {
            entityCollider.enabled = false;
        }
    }

    public void GetData(GameObject data)
    {
        storedData = data.GetComponent<CircularIndicator>();
    }

    public void BlowUp()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 3f);
        foreach (Collider2D collider in hitColliders)
        {
            CollateralScript collateralTrigger = collider.GetComponent<CollateralScript>();
            if (collateralTrigger != null)
            {
                collateralTrigger.CollateralDamage(100f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject bomb = Instantiate(explosionVFX, collision.transform.position, Quaternion.identity);
            collision.gameObject.GetComponent<PlayerHealthScript>().TakeDamage(enemyData.attackDamage);
            Destroy(gameObject);
            Destroy(storedData.gameObject);
        }
    }
}