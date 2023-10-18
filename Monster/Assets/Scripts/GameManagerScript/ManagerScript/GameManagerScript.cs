using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public GameObject player; // Prefab of the player
    public GameObject deployScreen;
    public Animator barAnim;
    public bool gameStarted = false;
    public GameObject enemySpawner;
    public List<GameObject> obstacleList = new List<GameObject>();
    public MeteorScript meteor;
    public GameObject hitcircle;
    private void Start()
    {
        deployScreen.SetActive(true);
        player = GameObject.Find("Player");

        player.GetComponent<SpriteRenderer>().enabled = false;
        barAnim.SetBool("RevealGame", false);
        AstarPath.active.Scan(); //scan the grid
        ScanAndInsert();
        DisableObstacles();
        meteor = GameObject.Find("Meteor").GetComponent<MeteorScript>();
        enemySpawner.SetActive(false);
        hitcircle = GameObject.Find("HitCircle");
        hitcircle.SetActive(false);
    }


    void Update()
    {
        if (!gameStarted && Input.anyKeyDown)
        {
            StartGame();
            meteor.isMoving = true;

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

        // Call any other functions or actions to start your game
        deployScreen.SetActive(false);

        // Set the game as started
        gameStarted = true;
        OpenBar();


    }

    void ScanAndInsert()
    {
        GameObject[] obstacleCollider = GameObject.FindGameObjectsWithTag("Obstacle");

        foreach (var obs in obstacleCollider)
        {
            obstacleList.Add(obs);
        }
    }

    void DisableObstacles()
    {
        foreach (var obs in obstacleList)
        {
            Destroy(obs);
        }

    }
}