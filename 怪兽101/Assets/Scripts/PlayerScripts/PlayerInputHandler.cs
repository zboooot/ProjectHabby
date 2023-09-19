using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 MovementInput { get; private set; }
    public PlayerStatScriptableObject playerSO;
    private Rigidbody2D rb;
    public bool attackNow;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        ProcessInput();
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

    void OnAnimationMove()
    {
        rb.velocity = new Vector2(MovementInput.x * playerSO.speed, MovementInput.y * playerSO.speed);
    }
}
