using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public GameObject player; // Prefab of the player
    public GameObject deployScreen;

    private bool gameStarted = false;


    private void Start()
    {
        Time.timeScale = 0;
        deployScreen.SetActive(true);
        player = GameObject.Find("Player");
        player.GetComponent<SpriteRenderer>().enabled = false;
    }


    void Update()
    {
        if (!gameStarted && Input.anyKeyDown)
        {
            StartGame();
        }
    }


    void StartGame()
    {
        Time.timeScale = 1;
        // Spawn the player
        player.GetComponent<SpriteRenderer>().enabled = true;

        // Optionally, set the player's parent or any other initialization

        // Set the game as started
        gameStarted = true;

        // Call any other functions or actions to start your game
        deployScreen.SetActive(false);

    }
}
