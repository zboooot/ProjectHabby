using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDPlayerMovementScript : MonoBehaviour
{
    // Start is called before the first frame update

    public Rigidbody2D rb;
    public PlayerStatScriptableObject playerSO;
    private Vector2 moveDir;

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

        moveDir = new Vector2(moveX, moveY).normalized;
    }

    void OnAnimationMove()
    {
        rb.velocity = new Vector2(moveDir.x * playerSO.speed, moveDir.y * playerSO.speed);
    }
}
