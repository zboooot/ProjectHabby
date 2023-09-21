using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBuildingEnemy : MonoBehaviour
{
    public EnemyScriptableObject SO_enemy;
    float tempHealth;
    private SpriteRenderer spriteRenderer;
    public Sprite destroyedSprite;
    public Targetable buildingType;

    // Start is called before the first frame update
    void Start()
    {
        tempHealth = SO_enemy.health;
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        spriteRenderer.sprite = destroyedSprite;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
