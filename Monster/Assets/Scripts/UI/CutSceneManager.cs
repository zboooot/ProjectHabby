using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutSceneManager : MonoBehaviour
{
    public GameManagerScript gameManager;
    public Animator anim;
    private PlayerHandler inputHandler;
    public Image commanderImage;
    public List<Sprite> commanderDialogues = new List<Sprite>();

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>();
        inputHandler = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHandler>();
        anim = GetComponent<Animator>();
    }

    public void TriggerDialogue()
    {
        if (gameManager.isVictory)
        {
            commanderImage.sprite = commanderDialogues[0]; //Victory dialogue goes here
        }

        else
        {
            commanderImage.sprite = commanderDialogues[1]; //Defeat dialogue goes here
        }
    }

    public void CallEndScreen()
    {
        gameManager.TriggerEndScreen();
    }

    public void TriggerEnd()
    {
        inputHandler.canMove = false;
        TriggerDialogue();
        anim.SetBool("isTriggered", true);
    }

    public void ResetTrigger()
    {
        anim.SetBool("isTriggered", false);
    }

}
