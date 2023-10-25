using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerVictory : MonoBehaviour
{
    public GameManagerScript gameManager;
    public Animator anim;
    private PlayerInputHandler inputHandler;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>();
        inputHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInputHandler>();
        anim = GetComponent<Animator>();
    }

    public void TriggerVictoryScreen()
    {
        inputHandler.canMove = false;
        gameManager.TriggerEndScreen(false);
    }

    public void ResetTrigger()
    {
        anim.SetBool("isTriggered", false);
    }

}
