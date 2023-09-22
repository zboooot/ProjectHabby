using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingEnemy : MonoBehaviour
{
    public EnemyScriptableObject SO_enemy;
    float tempHealth;
    private SpriteRenderer spriteRenderer;
    public Sprite destroyedSprite;
    public Targetable buildingType;

    // Start is called before the first frame update
    void Start()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();
    }   

    public void TakeDamage(float damage)
    {
        tempHealth -= damage;
        if (tempHealth == 0)
        {
            Death();
        }
        else return;
    }


    void Death()
    {
        spriteRenderer.sprite = destroyedSprite;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
