
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetEnemy : MonoBehaviour
{
    public float moveSpeed = 10f;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    public Transform cameraTransform;
    private bool isToRightOfCamera = false;
    public float destroyTime = 10f;
    private float currentTime = 0f;

    public GameObject bombPrefab;
    public Transform firingPoint;
    public float fireRate = 1f;
    private float nextFireTime = 0f;

    private bool isPlayerWithinCollider = false; // Flag to check if the player is within the collider

    private void Start()
    {
        cameraTransform = GameObject.Find("Main Camera").GetComponent<Transform>();
        spriteRenderer = GetComponent<SpriteRenderer>(); 
        checkPosition();
    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= destroyTime)
        {
            Destroy(gameObject);
        }

        // Check if the player is within the collider and enough time has passed to drop another bomb
        if (isPlayerWithinCollider && Time.time > nextFireTime)
        {
            BombsAway();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void checkPosition()
    {
        if (transform.position.x > cameraTransform.position.x)
        {
            isToRightOfCamera = true;
            MoveLeft();
            Debug.Log("Object is to the right of the camera.");
        }
        else
        {
            isToRightOfCamera = false;
            MoveRight();
            Debug.Log("Object is not to the right of the camera.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isPlayerWithinCollider = true; // Player is within the collider
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isPlayerWithinCollider = false; // Player has exited the collider
        }
    }

    void BombsAway()
    {
        GameObject newBomb = Instantiate(bombPrefab, firingPoint.position, firingPoint.rotation);
    }

    void MoveLeft()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer.flipX = false;
        rb.velocity = new Vector2(-moveSpeed, 0f);
    }

    void MoveRight()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer.flipX = true;
        rb.velocity = new Vector2(moveSpeed, 0f);
    }
}