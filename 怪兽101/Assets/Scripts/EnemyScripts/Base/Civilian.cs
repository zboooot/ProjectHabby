using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Civilian : MonoBehaviour
{
    public enum EnemyState { fall, run, death, walk,}

    public EnemyState enemyState;
    SpriteRenderer spriteRenderer;
    public EnemyScriptableObject enemyData;
    Rigidbody2D rb;
    public float detectionDistance = 4f;
    private Collider2D entityCollider;
    public Transform player;
    public Animator anim;
    private float lastPosX;

    //Wandering Variables
    public float changeDirectionInterval = 2.0f; // Time interval to change direction
    public float maxWanderDistance = 5.0f; // Maximum distance to wander from the starting point

    private Vector2 targetPosition;
    private float timeSinceLastDirectionChange = 0.0f;

    private PlayerScoreScript playerScore;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        entityCollider = GetComponent<Collider2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerScore = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScoreScript>();
    }

    // Start is called before the first frame update
    void Start()
    {
        lastPosX = transform.position.x;

        targetPosition = transform.position;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerLeg" || collision.gameObject.tag == "End")
        {
            entityCollider.enabled = false;
            if (playerScore != null)
            {
                playerScore.EntityDestroyed();
            }
            enemyState = EnemyState.death;
        }

        //Put else if to make humans die upon contact with the other gameobjects
    }

    void MimicFall()
    {
        if(enemyState == EnemyState.fall)
        {
            rb.AddForce(Vector3.down * Time.deltaTime * 120);
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    void Run(Vector2 dir)
    {
            // Calculate the direction away from the player
            Vector3 runDirection = transform.position - player.position;

            // Normalize the direction to get a unit vector
            runDirection.Normalize();

            // Move the enemy in the runDirection
            transform.position += runDirection * enemyData.speed * Time.deltaTime;
  
    }

    void Walk()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Check if the player is within the detection distance
        if (distanceToPlayer < detectionDistance)
        {
            anim.SetBool("run", true);
            enemyState = EnemyState.run;
        }

        else
        {
            anim.SetBool("walk", true);
            timeSinceLastDirectionChange += Time.deltaTime;

            // Check if it's time to change direction
            if (timeSinceLastDirectionChange >= changeDirectionInterval)
            {
                // Generate a random direction vector
                Vector2 randomDirection = Random.insideUnitCircle.normalized;

                // Calculate a new target position within the wander range
                targetPosition = (Vector2)transform.position + randomDirection * Random.Range(0.0f, maxWanderDistance);

                // Reset the timer
                timeSinceLastDirectionChange = 0.0f;
            }

            // Move towards the target position
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, enemyData.speed * Time.deltaTime);
        }
    }

    void ChangeState()
    {
        anim.SetBool("run", true);
        enemyState = EnemyState.run;
    }

    void FallToRun()
    {
        Invoke("ChangeState", 1.5f);
    }

    void Death()
    {
        entityCollider.enabled = false;
        Destroy(gameObject, 2f);
    }

    void FlipSprite()
    {
        // Check if the sprite's horizontal position has changed since the last frame
        float currentPositionX = transform.position.x;
        if (currentPositionX > lastPosX)
        {
            // Moving right, so flip the sprite to face right
            spriteRenderer.flipX = true;
        }
        else if (currentPositionX < lastPosX)
        {
            // Moving left, so flip the sprite to face left
            spriteRenderer.flipX = false;
        }

        // Update the last position for the next frame
        lastPosX = currentPositionX;
    }

    // Update is called once per frame
    void Update()
    {
        FlipSprite();
        MimicFall();

        switch (enemyState)
        {
            case EnemyState.death:
                anim.SetBool("death", true);
                Death();
                break;

            case EnemyState.fall:
                anim.SetBool("fall", true);
                FallToRun();
                break;

            case EnemyState.run:
                Run(player.position);
                break;

            case EnemyState.walk:
                Walk();
                break;

        }
    }
}
