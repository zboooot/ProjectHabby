using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public GameObject player; // Prefab of the player
    public GameObject deployScreen;
    public Animator barAnim;
    private bool gameStarted = false;
    public GameObject enemySpawner;

    private void Start()
    {
        Time.timeScale = 0;
        deployScreen.SetActive(true);
        player = GameObject.Find("Player");
      
        player.GetComponent<SpriteRenderer>().enabled = false;
        barAnim.SetBool("RevealGame", false);

        enemySpawner.SetActive(false);
    }


    void Update()
    {
        if (!gameStarted && Input.anyKeyDown)
        {
            StartGame();
        }
    }

    public void CloseBar()
    {
        barAnim.SetBool("GameRevealed", true);
    }

    public void OpenBar()
    {
        barAnim.SetBool("RevealGame", true);
    }
    void StartGame()
    {
        Time.timeScale = 1;
    
        // Call any other functions or actions to start your game
        deployScreen.SetActive(false);

        // Set the game as started
        gameStarted = true;
        OpenBar();


    }
}
