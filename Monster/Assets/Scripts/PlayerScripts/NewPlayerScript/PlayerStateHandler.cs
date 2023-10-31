using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerStateHandler : MonoBehaviour
{
    public enum PlayerCurrentState
    {
        idle,
        move,
        attack,
        death,
        victory,
        rage
    }

    [Header("Current State")]
    public PlayerCurrentState state;
    public bool facingLeft;
    [Range(-1f, 1f)]
    public float currentSpeed;

    public PlayerAnimationHandler animHandler;

    public event System.Action IdleEvent;
    public event System.Action MoveEvent;
    public event System.Action AttackEvent;
    public event System.Action DeathEvent;
    public event System.Action VictoryEvent;
    public event System.Action RageEvent;


    public void ChangeState(int newState)
    {
        switch (newState)
        {
            case 0:
                //idle
                state = PlayerCurrentState.idle;
                break;
            case 1:
                //move
                state = PlayerCurrentState.move;
                break;
            case 2:
                //attack
                break;
            case 3:
                //death
                break;
            case 4:
                //victory
                break;
            case 5:
                //rage
                break;
        }
        Debug.Log(state);
    }
}
