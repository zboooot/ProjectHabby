using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public EnemyScriptableObject SO_enemy;
    float tempHealth;

    // Start is called before the first frame update
    void Start()
    {
        tempHealth = SO_enemy.health;
    }   

    public void TakeDamage(float damage)
    {
        tempHealth -= damage;
        if (tempHealth == 0)
        {
            Debug.Log("Building Health:" + tempHealth);
            Death();
        }
        else return;
    }


    void Death()
    {
        Debug.Log("Boom Doomz Goomz CRASH");
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
