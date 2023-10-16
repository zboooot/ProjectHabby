using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCircle : MonoBehaviour
{
    public PlayerStatScriptableObject playerData;
    public float speed = 2.0f;  // The speed of rotation.
    public Transform player;    // Reference to the player's Transform.

    private Vector3 playerLastPosition;
    private float angle;
    private SpriteRenderer spriteRender;

    bool canAttack;

    private void Start()
    {
        playerLastPosition = player.position;
        spriteRender = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        UpdateColor();
        Vector3 playerMoveDirection = player.position - playerLastPosition;

        if (playerMoveDirection != Vector3.zero)
        {
            float playerRotation = Mathf.Atan2(playerMoveDirection.y, playerMoveDirection.x) * Mathf.Rad2Deg;
            angle = playerRotation;
        }

        Vector3 offset = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle) * playerData.attackRange, Mathf.Sin(Mathf.Deg2Rad * angle) * playerData.attackRange, 0);
        transform.position = player.position + offset;
        playerLastPosition = player.position;

        RotateTowardsPlayer();
    }

    private void RotateTowardsPlayer()
    {
        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("BigBuilding"))
        {
            canAttack = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("BigBuilding"))
        {
            canAttack = false;
        }
    }

    private void UpdateColor()
    {
        if (canAttack)
        {
            Debug.Log("Can attack");
            spriteRender.color = Color.red;
        }

        else
        {
            Debug.Log("Can't attack");
            spriteRender.color = Color.white;
        }
    }
}
