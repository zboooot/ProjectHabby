using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorScript : MonoBehaviour
{
    public Vector2 targetPosition;
    public Vector2 direction;
    public float speed = 2.0f; // Speed at which the object moves

    public bool isMoving;
    private Animator animator;
    public bool isActive;
    public GameObject player;
    public GameObject secondaryExplosion;

    private ShakeScript shakeScript;
    public GameObject crater;

    public PlayerInputHandler playerData;
    public GameObject enemySpawner;

    float ultimateRadius = 10f;
    public PlayerStatScriptableObject playerSO;
    public GameObject hitcircle;

    public void Start()
    {
        animator = GetComponent<Animator>();
        shakeScript = GameObject.Find("CM vcam1").GetComponent<ShakeScript>();
        playerData = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInputHandler>();
        hitcircle = GameObject.Find("HitCircle");
        player = GameObject.Find("Player");
        animator.SetBool("isMoving", true);

        Vector2 landingPos = new Vector2(player.transform.position.x, player.transform.position.y + 6f);
        targetPosition = landingPos;

        // Calculate the direction vector towards the target position
        direction = (targetPosition - (Vector2)transform.position).normalized;
        //hitcircle.SetActive(false);
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
            // Update the object's position
            //transform.position = (Vector2)transform.position + direction * speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            // Calculate the angle in radians
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Rotate the object to face the movement direction
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            // Check if the object has reached the target position
            if (Vector2.Distance(transform.position, targetPosition) <= 0f)
            {
                // Object has reached the target, you can add further actions here if needed.
                TransformMeteor();
                UseUltimate();
            }
        }

    }

    public void SecondaryExplosion()
    {
        Vector2 spawnPos = new Vector2(player.transform.position.x, player.transform.position.y -0.5f);
        Instantiate(secondaryExplosion, spawnPos, Quaternion.identity);
    }

    public void UseUltimate()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, ultimateRadius);
        foreach (Collider2D collider in hitColliders)
        {
            CollateralScript collateralTrigger = collider.GetComponent<CollateralScript>();
            if (collateralTrigger != null)
            {
                collateralTrigger.CollateralDamage(100f);
            }
        }
    }
    public void TransformMeteor()
    {
        transform.rotation = Quaternion.Euler(Vector3.zero);
        animator.SetBool("isMoving", false);
        // Spawn the player
    }

    public void DestroyMeteor()
    {
        Destroy(gameObject);
    }

    public void ActivatePlayer()
    {
        playerData.canMove = true;
        enemySpawner.SetActive(true);
    }

    public void SpawnPlayer()
    {
        Vector2 spawnPos = new Vector2(player.transform.position.x, player.transform.position.y + 1.6f);
        Instantiate(crater, spawnPos, Quaternion.identity);
       
        player.GetComponent<SpriteRenderer>().enabled = true;
        //hitcircle.SetActive(true);
    }



    public void StartMoving()
    {
        isMoving = true;
    }
}