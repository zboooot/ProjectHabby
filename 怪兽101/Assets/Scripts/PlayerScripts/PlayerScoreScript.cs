using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreScript : MonoBehaviour
{
    public int entitiesDestroyedCount = 0;

    public int numberOfPlanes;
    public GameObject enemyPlane;

    private Animator anim;
    public Transform[] spawnPoints;
    public Artillery ArtilleryScript;
    bool isActivating;

    private void Start()
    {
        anim = GameObject.Find("MilitaryAbilityWarning").GetComponent<Animator>();
        ArtilleryScript = GameObject.Find("Artyspawner").GetComponent<Artillery>();

        // Check if we have at least one spawn point
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points assigned!");
            return;
        }
    }


    public void EntityDestroyed()
    {
        entitiesDestroyedCount++;

        if (entitiesDestroyedCount >= 2)
        {
            if(isActivating != true)
            {
                BombingRun();
            }
            else
            {
                entitiesDestroyedCount = 0;
            }
        }

        if (entitiesDestroyedCount >= 30)
        {
            if (isActivating != true)
            {
                StartCoroutine(ArtilleryScript.SpawnArtilleryWithDelay());
            }
            else
            {
                entitiesDestroyedCount = 0;
            }
        }

    }

    public void BombingRun()
    {
        if (enemyPlane != null)
        {
            isActivating = true;

            Invoke("DeactiveBanner", 3f);

            anim.SetBool("Close", true);

            Invoke("RandomizeAndSpawn", 6f);

            entitiesDestroyedCount = 0;

            Invoke("ResetActivation", 15f);
        }
        
    }

    public void ResetActivation()
    {
        isActivating = false;
        entitiesDestroyedCount = 0;
    }

    void DeactiveBanner()
    {
        anim.SetBool("Close", false);
    }

    public void RandomizeAndSpawn()
    {
        // Randomly choose a spawn point
        Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // Instantiate the GameObject at the desired position
        SpawnObject(randomSpawnPoint);
    }

    public void SpawnObject(Transform spawnPoint)
    {

        // Define the stagger amount and initial offset
        float staggerAmountX = 2.0f;  // Adjust this based on desired spacing along x-axis
        float staggerAmountY = 2.0f;  // Adjust this based on desired spacing along y-axis
        float initialOffsetX = -staggerAmountX * 1.5f;  // Adjust initial offsets based on the formation
        float initialOffsetY = -staggerAmountY;  // Adjust initial offsets based on the formation

        for (int i = 0; i < numberOfPlanes; i++)
        {
            // Calculate the staggered position for each fighter plane
            float xOffset = initialOffsetX + i * staggerAmountX;
            float yOffset = initialOffsetY + i * staggerAmountY;
            Vector3 staggeredPosition = new Vector3(spawnPoint.position.x + xOffset, spawnPoint.position.y + yOffset, 0f);

            // Instantiate the fighter jet at the staggered position
            GameObject fighterJet = Instantiate(enemyPlane, staggeredPosition, spawnPoint.rotation);
        }
    }

}
