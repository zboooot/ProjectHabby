using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class PlayerAnimationHandler : MonoBehaviour
{
    #region Inspector
    [Header("Components")]
    public SkeletonAnimation skeletonAnimation;
    public PlayerInputHandler2 inputHandler;

    public AnimationReferenceAsset attack1, attack2, attack3, death, idle1, idle2, rage, smash, victory, walkDown, walkLeft, walkRight, walkUp;
    #endregion

    
    // Start is called before the first frame update
    void Start()
    {
    }

    public void UpdateAnimation(int state)
    {
        switch (state)
        {
            case 0:
                //idle
                PlayIdleAnimation();
                break;
            case 1:
                //move
                PlayMoveAnimation(inputHandler.moveDirToPass);
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
    }

    public void PlayIdleAnimation()
    {
        int random = Random.Range(0, 1);
        if(random == 0)
        {
            skeletonAnimation.AnimationState.SetAnimation(0, idle1, false);
        }
        else
        {
            skeletonAnimation.AnimationState.SetAnimation(0, idle1, false);
        }
    }

    public void PlayMoveAnimation(int dir)
    {
        switch (dir)
        {
            case 2:
                skeletonAnimation.AnimationState.SetAnimation(0, walkUp, false);
                break;

            case 3:
                skeletonAnimation.AnimationState.SetAnimation(0, walkDown, false);
                break;

            case 0:
                skeletonAnimation.AnimationState.SetAnimation(0, walkLeft, false);
                break;

            case 1:
                skeletonAnimation.AnimationState.SetAnimation(0, walkRight, false);
                break;
        }
    }

    public void PlayAttackAnimation()
    {
        int random = Random.Range(0, 2);
        if (random == 0)
        {
            skeletonAnimation.AnimationState.SetAnimation(0, attack1, false);
        }
        else if(random == 1)
        {
            skeletonAnimation.AnimationState.SetAnimation(0, attack2, false);
        }
        else
        {
            skeletonAnimation.AnimationState.SetAnimation(0, attack3, false);
        }
    }

    public void PlayUltimateAnimation()
    {
        skeletonAnimation.AnimationState.SetAnimation(0, smash, false);
    }

    public void PlayRageAnimation()
    {
        skeletonAnimation.AnimationState.SetAnimation(0, rage, false);
    }

    public void PlayDeathAnimation()
    {
        skeletonAnimation.AnimationState.SetAnimation(0, death, false);
    }
}
