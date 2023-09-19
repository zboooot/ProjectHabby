using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    protected Vector2 input;
    public bool isAttacking;
    bool alreadyDead;

    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, PlayerStatScriptableObject playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        alreadyDead = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        //Any future playerstates will have access to the input
        input = player.InputHandler.MovementInput;

        if(player.InputHandler.playerSO.health > 0)
        {
            if (input.magnitude != 0 && isAttacking == false)
            {
                stateMachine.ChangeState(player.MoveState);
            }

            if (player.InputHandler.attackNow == true && isAttacking == false)
            {
                stateMachine.ChangeState(player.AttackState);
            }
        }

        else
        {
            if(alreadyDead == false)
            {
                stateMachine.ChangeState(player.DeathState);
                alreadyDead = true;
            }
            else { return; }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
