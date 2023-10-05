using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorScript : MonoBehaviour
{
    public Vector2 targetPosition;
    public float speed = 2.0f; // Speed at which the object moves

    public bool isMoving;
    private Animator animator;
    public bool isActive;
    public GameObject player;

    private ShakeScript shakeScript;
    public GameObject crater;

    public PlayerInputHandler playerData;
    public GameObject enemySpawner;
    public void Start()
    {
        StartMoving();
        animator = GetComponent<Animator>();
        shakeScript = GameObject.Find("CM vcam1").GetComponent<ShakeScript>();

        isMoving = true;

        animator.SetBool("isMoving", true);
        player = GameObject.Find("Player");

        Vector2 landingPos = new Vector2(player.transform.position.x, player.transform.position.y + 6f);
       
        targetPosition = landingPos;

        playerData = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInputHandler>();

    }

    public void Shake()
    {
        shakeScript.CineShake();
    }

    public void ReassignFollow()
    {
        shakeScript.ReAssignCam();
    }

    private void Update()
    {
        
        if (isMoving == true)
        {

            // Calculate the direction vector towards the target position
            Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

            // Calculate the new position
            Vector2 newPosition = (Vector2)transform.position + direction * speed * Time.deltaTime;

            // Update the object's position
            transform.position = newPosition;

            // Calculate the angle in radians
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Rotate the object to face the movement direction
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            // Check if the object has reached the target position
            if (Vector2.Distance(transform.position, targetPosition) <= 0.1f)
            {
                // Object has reached the target, you can add further actions here if needed.
                isMoving = false;

                
            }
        }

        if (isMoving == false)
        {
            
            transform.rotation = Quaternion.Euler(Vector3.zero);
            animator.SetBool("isMoving", false);
            Debug.Log("HitFloor");
            // Spawn the player
        }
    }

    public void DestroyMeteor()
    {
        Destroy(gameObject);
    }

    public void ActivatePlayer()
    {
        playerData.startScene = false;
        enemySpawner.SetActive(true);

    }

    public void SpawnPlayer()
    {
        Vector2 spawnPos = new Vector2(player.transform.position.x, player.transform.position.y + 1.6f);
        Instantiate(crater, spawnPos, Quaternion.identity);
        player.GetComponent<SpriteRenderer>().enabled = true;
        
    }

    public void StartMoving()
    {
        isMoving = true;
    }
}
