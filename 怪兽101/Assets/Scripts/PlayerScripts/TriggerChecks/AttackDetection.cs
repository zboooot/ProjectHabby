using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDetection : MonoBehaviour
{
    public Player Player { get; private set; }

    public PlayerStatScriptableObject SO_player;

    public LayerMask enemyLayer;

    // Start is called before the first frame update
    void Start()
    {
        Player = GetComponentInParent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Collider2D FindNearestEnemy(Collider2D[] enemies)
    {
        Collider2D nearestEnemy = null;
        float nearestDistance = float.MinValue;
        Vector2 playerPos = Player.transform.position;

        foreach (Collider2D enemy in enemies)
        {
            float distance = Vector2.Distance(Player.transform.position, enemy.transform.position);
            if(distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestEnemy = null;
            }
        }

        return nearestEnemy;
    }  

    public void Attack(GameObject enemy)
    {
        //enemy.GetComponent<Enemy>().TakeDamage(SO_player.attackDamage);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if((enemyLayer & ( 1 << other.gameObject.layer)) != 0)
        {
            //Player.InputHandler.CheckAttack(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //Player.InputHandler.CheckAttack(false);
        }
    }

}
