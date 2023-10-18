using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScoreScript : MonoBehaviour
{
    public int entitiesDestroyedCount = 0;

    public int numberOfPlanes;
    public GameObject enemyPlane;

    private Animator anim;
    public Transform player;
    public Transform[] spawnPoints;
    public Artillery ArtilleryScript;
    bool isActivating;
    bool isBombing;

    public float blinkSpeed = 0.1f; // Speed of the blinking effect
    public float minAlpha = 0.0f;   // Minimum alpha value (fully transparent)
    public float maxAlpha = 1.0f;   // Maximum alpha value (fully opaque)
    public float lifeDuration = 1.5f; // Time the zone will exist before being destroyed
    private float currentLife;

    public GameObject warningZone;
    private Color originalColor;
    private float currentAlpha;
    private float blinkDirection = 1.0f; // Used to control the blinking direction

    public TextMeshProUGUI bannerText;

    private void Start()
    {
        anim = GameObject.Find("MilitaryAbilityWarning").GetComponent<Animator>();
        ArtilleryScript = GameObject.Find("ArtySpawner").GetComponent<Artillery>();
        warningZone.SetActive(false);

        // Check if we have at least one spawn point
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points assigned!");
            return;
        }
        
        if (warningZone == null)
        {
            Debug.LogError("SpriteRenderer component not found.");
            return;
        }

        originalColor = warningZone.GetComponent<SpriteRenderer>().color;
        currentAlpha = originalColor.a;

       
    }

    private void Update()
    {
        currentLife += Time.deltaTime;

        if (isBombing == true & currentLife <= lifeDuration)
        {
            BlinkingEffect();
        }
        else // Turn off after the specified duration
        {
            warningZone.SetActive(false);
            currentLife = 0;
            isBombing = false;

        }


    }

    void BlinkingEffect()
    {
        if (isBombing == true)
        {
            warningZone.SetActive(true);
            // Update the alpha value to create a blinking effect
            currentAlpha += blinkDirection * blinkSpeed * Time.deltaTime;
            currentAlpha = Mathf.Clamp(currentAlpha, minAlpha, maxAlpha);

            // Apply the new color with the updated alpha value
            Color newColor = warningZone.GetComponent<SpriteRenderer>().color;
            newColor.a = currentAlpha;
            warningZone.GetComponent<SpriteRenderer>().color = newColor;

            // Change blinking direction at the alpha limits
            if (currentAlpha <= minAlpha || currentAlpha >= maxAlpha)
            {
                blinkDirection *= -1.0f;
            }

        }

    }

    public void EntityDestroyed()
    {
        entitiesDestroyedCount++;

        if (entitiesDestroyedCount >= 10)
        {
            if (isActivating != true)
            {
                // Randomly choose between a bombing run and spawning artillery
                int randomEvent = Random.Range(0, 2); // 0 for bombing run, 1 for artillery
                if (randomEvent == 0)
                {
                    AirStrike();
                    bannerText.text = "Incoming AirStrike!";
                }
                else
                {
                    isActivating = true;
                    bannerText.text = "Incoming Barrage!";

                    StartCoroutine(ArtilleryScript.SpawnArtilleryWithDelay());

                    Invoke("DeactiveBanner", 3f);

                    anim.SetBool("Close", true);

                    entitiesDestroyedCount = 0;

                    Invoke("ResetActivation", 15f);


                }
            }
            else
            {
                entitiesDestroyedCount = 0;
            }
        }
    }

    public void AirStrike()
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
        isBombing = true;
        warningZone.transform.position = player.transform.position;

        // Set spawn points to player y pos
        spawnPoints[0].position = new Vector3(spawnPoints[0].position.x, player.position.y, spawnPoints[0].position.z);
        spawnPoints[1].position = new Vector3(spawnPoints[1].position.x, player.position.y, spawnPoints[1].position.z);
        
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
