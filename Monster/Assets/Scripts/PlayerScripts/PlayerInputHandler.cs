using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
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
    public GameObject hitVFX;
    public Transform hitPos;
    public GameObject ultimateVFX;
    public bool isDead;

    public bool canMove;
    public float move;


    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rangeX = playerSO.attackRange;
        rangeY = playerSO.attackRange;
        boxSize = new Vector2(rangeX, rangeY);
        healthScript = GetComponent<PlayerHealthScript>();
        move = playerSO.speed;
    }

    private void Update()
    {
        if(canMove)
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
    }

    public void CheckAttack(bool canAttack)
    {
        attackNow = canAttack;
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

    public void UseUltimate()
    {
        GameObject ultiVFX = Instantiate(ultimateVFX, player.transform.position, Quaternion.identity);
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(detectionOrigin.position, ultimateRadius);
        foreach (Collider2D collider in hitColliders)
        {
            if (collider.CompareTag("Tank"))
            {
                NewEnemyScript tank = collider.GetComponent<NewEnemyScript>();
                if (tank != null)
                {
                    tank.TakeDamage(playerSO.ultimateDamage);
                }
                else { return; }
            }

            else if (collider.CompareTag("BigBuilding"))
            {
                BigBuildingEnemy bigBuilding = collider.GetComponent<BigBuildingEnemy>();
                if (bigBuilding != null)
                {
                    bigBuilding.TakeDamage(playerSO.ultimateDamage);
                }
                else { return; }
            }

            else if (collider.CompareTag("Civilian"))
            {
                Civilian civilian = collider.GetComponent<Civilian>();
                if (civilian != null)
                {
                    civilian.enemyState = Civilian.EnemyState.death;
                }
                else { return; }
            }


            else if (collider.CompareTag("Tree"))
            {
                Trees tree = collider.GetComponent<Trees>();
                if (tree != null)
                {
                    tree.Death();
                }
                else { return; }
            }

            else if (collider.CompareTag("Car"))
            {
                CarAI car = collider.GetComponent<CarAI>();
                if (car != null)
                {
                    car.Death();
                }
                else { return; }
            }

            else if (collider.CompareTag("Solider"))
            {
                HumanSoldier soldier = collider.GetComponent<HumanSoldier>();
                if (soldier != null)
                {
                    soldier.isBurnt = true;
                    soldier.Death();
                }
                else { return; }
            }
        }
    }

    public void Attack()
    {
        if (selectedEnemy != null)
        {
            GameObject hitEffect = Instantiate(hitVFX, hitPos.transform.position, Quaternion.identity);
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