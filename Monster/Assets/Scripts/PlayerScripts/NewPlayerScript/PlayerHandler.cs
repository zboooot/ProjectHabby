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
    public AnimationReferenceAsset idling, moving, moving2, attacking, attacking2, attacking3, ultimating, victorying, defeating, raging;
    [SerializeField] private PlayerStates currentState;
    [SerializeField] private PlayerStates prevState;
    public string currentAnimation;

    //Variable for player input
    public PlayerStatScriptableObject playerData;
    public Joystick joystick;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private PlayerVFXManager vfxManager;
    private Vector2 movementInput;
    private Vector2 lastKnownVector;
    public LayerMask enemyLayer;
    [SerializeField] private Collider2D selectedEnemy;
    public bool canMove;
    [SerializeField] private bool isAttacking;

    public GameObject[] legLocations;
    public List<UltimateBase> utlimates = new List<UltimateBase>();
    public float currentUltimateCharge;
    public float ultimateRadius = 20f;
    [SerializeField] private bool isUltimate;
    [SerializeField] private bool isRaging;
    public bool isEnd;

    public float impactTimer;
    public float currentImpactTimer;

    // Start is called before the first frame update
    void Start()
    {
        currentState = PlayerStates.idle;
        SetCharacterState(currentState);

        vfxManager = GetComponent<PlayerVFXManager>();
        rb = GetComponent<Rigidbody2D>();
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            if (!isEnd)
            {
                PlayerMove();
                PlayerAttack();

            }
            else
            {
                rb.velocity = Vector2.zero;
                return;
            }
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
            SpawnFootprint();
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


    public void SpawnFootprint()
    {
        if(currentImpactTimer < impactTimer)
        {
            currentImpactTimer += Time.deltaTime;
        }


        else if (currentImpactTimer >= impactTimer)
        {
            currentImpactTimer = 0f;
            vfxManager.SpawnImpactAtFoot(0);
            vfxManager.SpawnImpactAtFoot(1);

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
            if (!isAttacking)
            {
                Invoke("TriggerDamage", 1f);
                isAttacking = true;
            }
        }
        else
        {
            selectedEnemy = null;
            if (isAttacking)
            {
                SetCharacterState(prevState);
                isAttacking = false;
            }
        }
    }

    void TriggerAttackDirAnimation()
    {
        float playerX = this.transform.position.x;
        float objectX = selectedEnemy.gameObject.transform.position.x;

        if (objectX > playerX)
        {
            SetAnimation(0, attacking, true, 1f);
            // You can perform actions specific to the object being on the left
        }
        else if (objectX < playerX)
        {
            SetAnimation(0, attacking2, true, 1f);
            // You can perform actions specific to the object being on the right
        }
        else
        {
            SetAnimation(0, attacking3, true, 1f);
            // You can handle the scenario when the object and player are at the same position
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
    public void ChargeUltimate(int amount)
    {
        if (currentUltimateCharge != playerData.maxUltimateCharge)
        {
            currentUltimateCharge += amount;
        }

        if (currentUltimateCharge >= playerData.maxUltimateCharge)
        {
            currentUltimateCharge = playerData.maxUltimateCharge;
        }
    }

    public void UseUltimate1()
    {
        utlimates[0].UseDamageUltimate(ultimateRadius, playerData.ultimateDamage);
    }

    //Trigger ultimate, rage, victory and defeat state here
    public void DisableMovement(int state)
    {
        canMove = false;
        switch (state)
        {
            case 0:
                SetCharacterState(PlayerStates.ultimate);

                if (!currentState.Equals(PlayerStates.ultimate))
                {
                    prevState = currentState;
                }
                SetCharacterState(PlayerStates.ultimate);
                if (!isUltimate)
                {
                    Invoke("UseUltimate1", 1f);
                    isUltimate = true;
                }
                break;
            case 1:
                SetCharacterState(PlayerStates.rage);
                if (!currentState.Equals(PlayerStates.rage))
                {
                    prevState = currentState;
                }
                SetCharacterState(PlayerStates.rage);
                if (!isRaging)
                {
                    isRaging = true;
                }
                break;
            case 2:
                SetCharacterState(PlayerStates.victory);
                break;
            case 3:
                SetCharacterState(PlayerStates.defeat);
                break;
        }
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
        if (isAttacking || isUltimate || isRaging)
        {
            isAttacking = false;
            isUltimate = false;
            isRaging = false;

            if (!canMove)
            {
                canMove = true;
            }

            else { return;  }
        }

        else
        {
            return;
        }

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
            TriggerAttackDirAnimation();
        }

        if (state.Equals(PlayerStates.ultimate))
        {
            SetAnimation(0, ultimating, false, 1f);
        }

        if (state.Equals(PlayerStates.victory))
        {
            SetAnimation(0, victorying, false, 1f);
        }

        if (state.Equals(PlayerStates.defeat))
        {
            SetAnimation(0, defeating, false, 1f);
        }

        if (state.Equals(PlayerStates.rage))
        {
            SetAnimation(0, raging, false, 1f);
        }

        currentState = state;
    }
}
