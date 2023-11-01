using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class PlayerHandler : MonoBehaviour
{
    public enum PlayerStates
    {
        idle,
        attack,
        move,
        victory,
        defeat,
        rage,
        ultimate,
    }

    //Variable for State
    public SkeletonAnimation skeletonAnim;
    public AnimationReferenceAsset idling, moving, moving2, attacking, ultimating, victorying, defeating, raging;
    [SerializeField] private PlayerStates currentState;
    [SerializeField] private PlayerStates prevState;
    public string currentAnimation;

    //Variable for player input
    public PlayerStatScriptableObject playerData;
    public Joystick joystick;
    [SerializeField] private Rigidbody2D rb;
    private Vector2 movementInput;
    private Vector2 lastKnownVector;
    public LayerMask enemyLayer;
    [SerializeField] private Collider2D selectedEnemy;
    [SerializeField] private bool canMove;

    // Start is called before the first frame update
    void Start()
    {
        currentState = PlayerStates.idle;
        SetCharacterState(currentState);

        rb = GetComponent<Rigidbody2D>();
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            PlayerMove();
            PlayerAttack();
        }
    }

    private void PlayerMove()
    {
        float moveX = joystick.Horizontal;
        float moveY = joystick.Vertical;

        movementInput = new Vector2(moveX, moveY).normalized;
        rb.velocity = new Vector2(movementInput.x * playerData.speed, movementInput.y * playerData.speed);
        if (movementInput != Vector2.zero)
        {
            if (!currentState.Equals(PlayerStates.attack))
            {
                SetCharacterState(PlayerStates.move);
            }

            if (movementInput.x != 0 && movementInput.y != 0)
            {
                lastKnownVector = movementInput;
            }
        }

        else
        {
            if (!currentState.Equals(PlayerStates.attack))
            {
                rb.velocity = Vector2.zero;
                SetCharacterState(PlayerStates.idle);
            }
        }
    }

    private void PlayerAttack()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, lastKnownVector, playerData.attackRange, enemyLayer);
        // Check if the raycast hits an object
        if (hit.collider != null)
        {
            //hit.collider.gameObject.GetComponent<Targetable>().TakeDamage();
            Debug.Log(hit);
            selectedEnemy = hit.collider;
            if (!currentState.Equals(PlayerStates.attack))
            {
                prevState = currentState;
            }
            SetCharacterState(PlayerStates.attack);
            //Invoke("TriggerDamage", 2f);
        }
    }

    //In the animation, this will deal damage to the select unit
    public void TriggerDamage()
    {
        if (selectedEnemy != null)
        {
            selectedEnemy.GetComponent<Targetable>().TakeDamage();
        }

        else { return; }
    }

    public void SpeedUp()
    {
        //Increase animation speed for dramatic effect
    }

    //Trigger ultimate, rage, victory and defeat state here
    public void DisableMovement(int state)
    {
        switch (state)
        {
            case 0:
                SetCharacterState(PlayerStates.ultimate);

                if (!currentState.Equals(PlayerStates.ultimate))
                {
                    prevState = currentState;
                }
                Invoke("EnableMovment", 3.2f);
                break;
            case 1:
                SetCharacterState(PlayerStates.rage);
                if (!currentState.Equals(PlayerStates.rage))
                {
                    prevState = currentState;
                }
                Invoke("EnableMovment", 2.4f);
                break;
            case 2:
                SetCharacterState(PlayerStates.victory);
                break;
            case 3:
                SetCharacterState(PlayerStates.defeat);
                break;
        }
        canMove = false;
    }

    public void EnableMovement()
    {
        canMove = true;
    }

    public void SetAnimation(int track, AnimationReferenceAsset animation, bool loop, float timeScale)
    {
        if (animation.name.Equals(currentAnimation))
        {
            return;
        }
        Spine.TrackEntry animationEntry = skeletonAnim.state.SetAnimation(track, animation, loop);
        animationEntry.TimeScale = timeScale;
        animationEntry.Complete += AnimationEntry_Complete;
        currentAnimation = animation.name;
    }

    //Triggers after the animation has played
    private void AnimationEntry_Complete(Spine.TrackEntry trackEntry)
    {
        SetCharacterState(prevState);
    }

    public void SetCharacterState(PlayerStates state)
    {
        if (state.Equals(PlayerStates.idle))
        {
            SetAnimation(0, idling, true, 1f);
        }

        if (state.Equals(PlayerStates.move))
        {
            if(movementInput.y > 0)
            {
                SetAnimation(0, moving, true, 1f);
            }

            else if(movementInput.y < 0)
            {
                SetAnimation(0, moving2, true, 1f);
            }
        }

        if (state.Equals(PlayerStates.attack))
        {
            SetAnimation(0, attacking, true, 1f);
        }

        if (state.Equals(PlayerStates.ultimate))
        {
            SetAnimation(0, ultimating, true, 1f);
        }

        if (state.Equals(PlayerStates.victory))
        {
            SetAnimation(0, victorying, true, 1f);
        }

        if (state.Equals(PlayerStates.defeat))
        {
            SetAnimation(0, defeating, true, 1f);
        }

        if (state.Equals(PlayerStates.rage))
        {
            SetAnimation(0, raging, true, 1f);
        }

        currentState = state;
    }
}
