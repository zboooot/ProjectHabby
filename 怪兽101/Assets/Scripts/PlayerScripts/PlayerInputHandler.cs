using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 MovementInput { get; private set; }
    public PlayerStatScriptableObject playerSO;
    private Rigidbody2D rb;
    public bool attackNow;

    public Vector2 boxSize = new Vector2(4f, 4f); // Adjust the size as needed.
    public LayerMask enemyLayer;
    public Collider2D selectedEnemy;

    public float attackCD;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        ProcessInput();
        Debug.Log("Is player attacking" + attackNow);
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, boxSize, 0f, enemyLayer);

        if (colliders.Length > 0)
        {
            Collider2D nearestEnemy = FindNearestEnemy(colliders);
            selectedEnemy = nearestEnemy;
            CheckAttack(true);
        }

        else { selectedEnemy = null; }
    }

    private void FixedUpdate()
    {
        OnAnimationMove();
    }

    void ProcessInput()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

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

    private Collider2D FindNearestEnemy(Collider2D[] enemies)
    {
        selectedEnemy = null;
        float nearestDistance = float.MaxValue;
        Vector2 playerPosition = transform.position;

        foreach (Collider2D enemy in enemies)
        {
            float distance = Vector2.Distance(playerPosition, enemy.transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                selectedEnemy = enemy;
            }
        }

        return selectedEnemy;
    }

    public void Attack()
    {
        if(selectedEnemy != null)
        {
            selectedEnemy.GetComponent<NewEnemyScript>().TakeDamage(playerSO.attackDamage);
        }

        else { return; }
    }

    void OnAnimationMove()
    {
        rb.velocity = new Vector2(MovementInput.x * playerSO.speed, MovementInput.y * playerSO.speed);
    }
}
