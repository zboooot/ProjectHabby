using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEnemyScript : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    public enum EnemyState { Move, Attack, Death }
    public EnemyState currentState;


    public Transform player;
    float tempHealth;
    void Start()
    {
        // Initialize to the initial state (e.g., Idle)
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        tempHealth = enemyData.health;
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

    private void Update()
    {
        Debug.Log(currentState);
    }
}
