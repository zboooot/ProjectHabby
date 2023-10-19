using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCircle : MonoBehaviour
{
    public PlayerStatScriptableObject playerData;
    public PlayerInputHandler inputHandler;
    public float speed = 10.0f;  // The speed of rotation.
    public Transform player;    // Reference to the player's Transform.
    public Joystick joystick;

    private Vector3 playerLastPosition;
    private float currentAngle;
    private SpriteRenderer spriteRender;


    private void Start()
    {
        spriteRender = GetComponent<SpriteRenderer>();
        inputHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInputHandler>();
    }

    private void Update()
    {
        //Check if the player is colliding with entities under the enemy layer
        CheckCollision();

        if (Input.anyKey)
        {
            float horizontalInput = joystick.Horizontal;
            float verticalInput = joystick.Vertical;

            // Calculate the input angle based on player's input.
            float inputAngle = Mathf.Atan2(verticalInput, horizontalInput) * Mathf.Rad2Deg;

            // Calculate the angle between the sprite and the player character.
            float angleToPlayer = Mathf.Atan2(player.position.y - transform.position.y, player.position.x - transform.position.x) * Mathf.Rad2Deg;

            // Calculate the difference between the input angle and angle to the player.
            float angleDifference = inputAngle - angleToPlayer;

            // Update the current angle by adding the difference and the orbit speed.
            currentAngle += (angleDifference + 180f) % 360f + speed * Time.deltaTime;

            if (!inputHandler.isCollision)
            {
                // Calculate the new position of the object in orbit around the player character.
                float x = player.position.x + 3 * Mathf.Cos(currentAngle * Mathf.Deg2Rad);
                float y = player.position.y + 3 * Mathf.Sin(currentAngle * Mathf.Deg2Rad);
                transform.position = new Vector3(x, y, transform.position.z);
            }

            // Rotate the object to face the player character.
            Vector3 direction = player.position - transform.position;
            float lookAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, lookAngle);
        }
    }

    private void CheckCollision()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 1f);
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
                spriteRender.color = Color.white;
            }
        }
    }
}
