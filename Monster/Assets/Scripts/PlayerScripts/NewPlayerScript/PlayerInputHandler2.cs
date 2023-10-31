using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler2 : MonoBehaviour
{
    public PlayerStatScriptableObject playerData;
    [SerializeField] private PlayerStateHandler stateHandler;
    [SerializeField] private Joystick joystick;
    public Rigidbody2D rb;
    public MovementDir moveDir;
    public int moveDirToPass;


    private Vector2 movementInput;
    private Vector2 lastKnownVector;

    public enum MovementDir
    {
        up,
        down,
        left,
        right,
    }

    // Start is called before the first frame update
    void Start()
    {
        stateHandler = GetComponent<PlayerStateHandler>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        PlayerAttack();
        UpdateState();
    }

    public void PlayerMove()
    {
        float moveX = joystick.Horizontal;
        float moveY = joystick.Vertical;

        movementInput = new Vector2(moveX, moveY).normalized;
        if (movementInput.x != 0 && movementInput.y != 0)
        {
            lastKnownVector = movementInput;
            MovingDirection(lastKnownVector);
        }

        rb.velocity = new Vector2(movementInput.x * playerData.speed, movementInput.y * playerData.speed);
    }

    private void MovingDirection(Vector2 dir)
    {
        if(joystick.Horizontal > 0)
        {
            moveDir = MovementDir.right;
            moveDirToPass = 0;
        }
        else
        {
            moveDir = MovementDir.left;
            moveDirToPass = 1;
        }

        if(joystick.Vertical > 0)
        {
            moveDir = MovementDir.up;
            moveDirToPass = 2;
        }
        else
        {
            moveDir = MovementDir.down;
            moveDirToPass = 3;
        }
    }

    public void PlayerAttack()
    {

    }

    public void PlayerUltimate()
    {

    }

    void UpdateState()
    {
        if(joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            stateHandler.ChangeState(1);
        }

        else
        {
            stateHandler.ChangeState(0);
        }
    }
}
