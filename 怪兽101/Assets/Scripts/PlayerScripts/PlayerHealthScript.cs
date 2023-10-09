using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealthScript : MonoBehaviour
{
    public PlayerStatScriptableObject playerSO;
    public Slider healthSlider;
    public Image healthFill;
    public float lerpSpeed = 2f;

    private ShakeScript shakeMe;
    private float currentHealth;

    //Flash Effect
    private PlayerFlash flashEffect;
    private int thresholdHealth;
    public int triggerNumber;

    private void Start()
    {
        shakeMe = healthSlider.gameObject.GetComponent<ShakeScript>();
        //playerSO.health = 100;
        currentHealth = playerSO.health; // Set initial health to full
        thresholdHealth = playerSO.health;
        flashEffect = GetComponent<PlayerFlash>();
        UpdateHealthBar();
    }

    private void Update()
    {
        if (currentHealth != playerSO.health)
        {
            // Lerp towards the target health value
            currentHealth = Mathf.Lerp(currentHealth, playerSO.health, Time.deltaTime * lerpSpeed);
            UpdateHealthBar();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(1);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            playerSO.health = 100;
            SceneManager.LoadScene("Gymbox");
        }
    }

    public void TakeDamage(int damage)
    {
        shakeMe.StartShake();
        playerSO.health -= damage; // Adjust the damage amount as needed
        int healthDifference = thresholdHealth - playerSO.health;
        if(healthDifference <= triggerNumber)
        {
            flashEffect.Flash();
            thresholdHealth = playerSO.health;
        }

        else { return; }
    }

    private void UpdateHealthBar()
    {
        healthSlider.value = currentHealth; // Update the slider's value
        healthFill.fillAmount = currentHealth; // Update the fill amount of the health bar
    }


}
