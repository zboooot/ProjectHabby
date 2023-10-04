using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }

    public PlayerMoveState MoveState { get; private set; }

    public PlayerAttackState AttackState { get; private set; }

    public PlayerDeathState DeathState { get; private set; }

    public PlayerUltimateState UltimateState { get; private set; }

    public Animator Anim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }

    public Rigidbody2D rb { get; private set; }

    private PlayerStatScriptableObject playerData;

    public Vector2 CurrentVelocity;

    public int FacingDirection { get; private set; }

    private Vector2 workspace;

    public void Awake()
    {
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        AttackState = new PlayerAttackState(this, StateMachine, playerData, "attack");
        DeathState = new PlayerDeathState(this, StateMachine, playerData, "death");
        UltimateState = new PlayerUltimateState(this, StateMachine, playerData, "ultimate");
    }

    private void Start()
    {
        //Initialize StateMacahine
        FacingDirection = -1;
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        rb = GetComponent <Rigidbody2D> ();
        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
        //CheckFlip();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
        
    }

    //Check & Change velocity
    public void SetVelocity(float velocity)
    {
        workspace.Set(velocity, CurrentVelocity.y);
        rb.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void CheckFlip()
    {
            if(rb.velocity.x >= 0)
            {
                transform.Rotate(0.0f, 0.0f, 0.0f);
            }
            else if (rb.velocity.x <= 0)
            {
                transform.Rotate(0.0f, 180.0f, 0.0f);
            }
            //Flip();
    }

    private void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }
}
