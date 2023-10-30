using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour, ISoundable
{
    public Vector2 MovementInput { get; private set; }
    public Joystick joystick;

    public PlayerStatScriptableObject playerSO;
    private Rigidbody2D rb;
    public bool attackNow;
    public bool ultimating;
    public float ultimateRadius = 20f;
    public float currentUltimateCharge;

    public Vector2 boxSize; // Adjust the size as needed.
    private float rangeX;
    private float rangeY;
    public LayerMask enemyLayer;
    public Collider2D selectedEnemy;
    bool facingLeft;
    public bool isAttacking;
    public bool isCollision;

    //public bool startScene = true;
    public Transform detectionOrigin;
    public PlayerHealthScript healthScript;

    public GameObject player;
    public Transform hitPos;
    public bool isDead;

    public bool canMove;
    public float move;

    public List<UltimateBase> utlimates = new List<UltimateBase>();
    public List<AudioClip> listofSFX = new List<AudioClip>();
    private AudioSource source;

    //New hit detection
    public float raycastDistance = 5f; // Maximum distance for the raycast
    public Vector3 lastKnownVector;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rangeX = playerSO.attackRange;
        rangeY = playerSO.attackRange;
        boxSize = new Vector2(rangeX, rangeY);
        healthScript = GetComponent<PlayerHealthScript>();
        move = playerSO.speed;
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        Cheats();
        HightlightRange();
        TriggerAttack();

        if (canMove)
        {
            CheckFlip();
            ProcessInput();
        }
        else { return; }
        //AttackNearestEnemy();
    }

    //void AttackNearestEnemy()
    //{
    //    Collider2D[] colliders = Physics2D.OverlapBoxAll(detectionOrigin.position, boxSize, 0f, enemyLayer);

    //    if (colliders.Length > 0)
    //    {
    //        foreach (Collider2D collider in colliders)
    //        {
    //            Vector2 direction = collider.transform.position - detectionOrigin.position;
    //            float angle = Vector2.Angle(direction, detectionOrigin.right);

    //            if (angle < 270f / 2f)
    //            {
    //                Collider2D nearestEnemy = FindNearestEnemy(colliders);
    //                selectedEnemy = nearestEnemy;
    //                if (isAttacking == false)
    //                {
    //                    CheckAttack(true);
    //                    isAttacking = true;
    //                    if(healthScript.healthState == PlayerHealthScript.HealthState.normal)
    //                    {
    //                        Invoke("ResetAttack", playerSO.attackSpeed);
    //                    }
    //                    else { Invoke("ResetAttack", 0.5f); }
    //                }
    //            }
    //        }
    //    }

    //    else { selectedEnemy = null; CheckAttack(false); }
    //}

    public void TriggerAttack(Collider2D enemy)
    {
        selectedEnemy = enemy;
        if (isAttacking == false)
        {
            CheckAttack(true);
            isAttacking = true;
            if (healthScript.healthState == PlayerHealthScript.HealthState.normal)
            {
                Invoke("ResetAttack", playerSO.attackSpeed);
            }
            else { Invoke("ResetAttack", 0.5f); }
        }
    }

    void ResetAttack()
    {
        isAttacking = false;
    }

    private void FixedUpdate()
    {
        if(canMove)
        {
            OnAnimationMove();
        }
        else { return; }
    }

    void ProcessInput()
    {
        float moveX = joystick.Horizontal;
        float moveY = joystick.Vertical;

        MovementInput = new Vector2(moveX, moveY).normalized;
        if(MovementInput.x != 0 && MovementInput.y != 0)
        {
            lastKnownVector = MovementInput;
        }
    }

    public void CheckAttack(bool canAttack)
    {
        attackNow = canAttack;
    }

    void HightlightRange()
    {
        RaycastHit2D hit = Physics2D.Raycast(detectionOrigin.position, lastKnownVector, playerSO.attackRange * 2, enemyLayer);

        // Check if the raycast hits an object
        if (hit.collider != null)
        {
            Debug.Log("Hit object: " + hit.collider.gameObject.name);
        }
    }

    void TriggerAttack()
    {
        RaycastHit2D hit = Physics2D.Raycast(detectionOrigin.position, lastKnownVector, playerSO.attackRange, enemyLayer);

        // Check if the raycast hits an object
        if (hit.collider != null)
        {
            TriggerAttack(hit.collider);
        }
    }

    public void StopAttackAnimation()
    {
        attackNow = false;
    }

    public void DeactivateUltimate()
    {
        ultimating = false;
        currentUltimateCharge = 0;
    }

    public void DeactivateMove()
    {
        canMove = false;
    }

    public void ActivateMove()
    {
        canMove = true;
    }

    //private Collider2D FindNearestEnemy(Collider2D[] enemies)
    //{
    //    selectedEnemy = null;
    //    float nearestDistance = float.MaxValue;
    //    Vector2 playerPosition = detectionOrigin.position;

    //    foreach (Collider2D enemy in enemies)
    //    {
    //        Transform enemyTransform = enemy.transform;
    //        Vector3 directionToEnemy = enemyTransform.position - detectionOrigin.position;
    //        float angle = Vector2.Angle(detectionOrigin.up, directionToEnemy);

    //        if (angle < 180f)
    //        {
    //            float distance = Vector2.Distance(playerPosition, enemy.transform.position);
    //            if (distance < nearestDistance)
    //            {
    //                nearestDistance = distance;
    //                selectedEnemy = enemy;
    //            }
    //        }
    //    }

    //    return selectedEnemy;
    //}
    private void Cheats()
    {
        if (Input.GetKeyUp(KeyCode.V))
        {
            CutSceneManager csManager = GameObject.FindGameObjectWithTag("VictoryScreen").GetComponent<CutSceneManager>();
            GameManagerScript gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>();
            gameManager.isVictory = true;
            csManager.TriggerEnd();

        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            CutSceneManager csManager = GameObject.FindGameObjectWithTag("VictoryScreen").GetComponent<CutSceneManager>();
            GameManagerScript gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>();
            gameManager.isVictory = false;
            csManager.TriggerEnd();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            Artillery artillery = GameObject.FindGameObjectWithTag("Artillery").GetComponent<Artillery>();
            StartCoroutine(artillery.SpawnArtilleryWithDelay());
        }
    }
    public void UseUltimate1()
    {
        utlimates[0].UseDamageUltimate(ultimateRadius, playerSO.ultimateDamage);
    }

    public void UseUltimate2()
    {
        utlimates[1].UseUtilityUltimate();
    }

    public void Attack()
    {
        if (selectedEnemy != null)
        {
            selectedEnemy.GetComponent<Targetable>().TakeDamage();
        }

        else { return; }
    }

    public void ChargeUltimate(int amount)
    {
        if (currentUltimateCharge != playerSO.maxUltimateCharge)
        {
            currentUltimateCharge += amount;
        }

        if (currentUltimateCharge >= playerSO.maxUltimateCharge)
        {
            currentUltimateCharge = playerSO.maxUltimateCharge;
        }
    }
    public void CheckFlip()
    {
        if (!canMove)
        {
            return;
        }
        else
        {
            if (joystick.Horizontal < 0 )
            {
                if (facingLeft != true)
                {
                    transform.Rotate(0.0f, 180.0f, 0.0f);
                    facingLeft = true;
                }

                else { return; }
            }

            else if (joystick.Horizontal > 0)
            {
                if (facingLeft == true)
                {
                    transform.Rotate(0.0f, -180.0f, 0.0f);
                    facingLeft = false;
                }

                else { return; }
            }
        }
    }

    void OnAnimationMove()
    {
        rb.velocity = new Vector2(MovementInput.x * move, MovementInput.y * move);
    }

    public void PlaySFX()
    {
        int index = Random.Range(0,2);
        source.clip = listofSFX[index];
        source.Play();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BigBuilding") || collision.gameObject.CompareTag("Tank"))
        {
            isCollision = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BigBuilding") || collision.gameObject.CompareTag("Tank"))
        {
            isCollision = false;
        }
    }
}