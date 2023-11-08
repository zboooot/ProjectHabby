using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCircle : MonoBehaviour
{
    public PlayerStatScriptableObject playerData;
    public float speed = 10.0f;  // The speed of rotation.
    public Transform player;    // Reference to the player's Transform.
    public Joystick joystick;
    [SerializeField] private Rigidbody2D playerRb;

    private Vector3 playerLastPosition;
    [SerializeField] private float prevInput;
    [SerializeField] private float currentAngle;
    [SerializeField] private float angleDifference;


    private void Start()
    {
        playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float horizontalInput = joystick.Horizontal;
        float verticalInput = joystick.Vertical;

        // Calculate the input angle based on player's input.
        float inputAngle = Mathf.Atan2(verticalInput, horizontalInput) * Mathf.Rad2Deg;

        // Calculate the angle between the sprite and the player character.
        float angleToPlayer = Mathf.Atan2(player.position.y - transform.position.y, player.position.x - transform.position.x) * Mathf.Rad2Deg;

        if(playerRb.velocity != Vector2.zero)
        {
            // Calculate the difference between the input angle and angle to the player.
            angleDifference = inputAngle - angleToPlayer;
            prevInput = inputAngle;
        }

        else
        {
            angleDifference = prevInput - angleToPlayer;
        }

        // Update the current angle by adding the difference and the orbit speed.
        currentAngle += (angleDifference + 180f) % 360f + speed * Time.deltaTime;

        // Calculate the new position of the object in orbit around the player character.
        float x = player.position.x + 4.5f * Mathf.Cos(currentAngle * Mathf.Deg2Rad);
        float y = player.position.y + 4.5f * Mathf.Sin(currentAngle * Mathf.Deg2Rad);
        transform.position = new Vector3(x, y, transform.position.z);

        // Rotate the object to face the player character.
        Vector3 direction = player.position - transform.position;
        float lookAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, lookAngle);
    }
}
