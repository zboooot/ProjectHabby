using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCircle : MonoBehaviour
{
    public PlayerStatScriptableObject playerData;
    public PlayerInputHandler inputHandler;
    public float speed = 10.0f;  // The speed of rotation.
    public Transform player;    // Reference to the player's Transform.

    private Vector3 playerLastPosition;
    private float angle;
    private SpriteRenderer spriteRender;

    private float targetAngle;
    private Vector3 targetPosition;


    private void Start()
    {
        spriteRender = GetComponent<SpriteRenderer>();
        inputHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInputHandler>();
    }

    private void Update()
    {
        CheckCollision();
        Vector3 playerMoveDirection = player.position - playerLastPosition;

        if (playerMoveDirection != Vector3.zero)
        {
            float playerRotation = Mathf.Atan2(playerMoveDirection.y, playerMoveDirection.x) * Mathf.Rad2Deg;
            targetAngle = playerRotation;
            UpdateTargetPosition();
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed);
        playerLastPosition = player.position;
        RotateTowardsPlayer();
    }

    private void UpdateTargetPosition()
    {
        Vector3 offset = new Vector3(Mathf.Cos(Mathf.Deg2Rad * targetAngle) * playerData.attackRange, Mathf.Sin(Mathf.Deg2Rad * targetAngle) * playerData.attackRange, 0);
        targetPosition = player.position + offset;
    }

    private void RotateTowardsPlayer()
    {
        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void CheckCollision()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 2f);
        foreach (Collider2D collider in hitColliders)
        {
            if(collider.gameObject.layer == 7)
            {
                spriteRender.color = Color.red;
                Debug.Log(collider);
                inputHandler.TriggerAttack(collider);
            }

            else
            {
                spriteRender.color = Color.grey;
            }
        }
    }
}
