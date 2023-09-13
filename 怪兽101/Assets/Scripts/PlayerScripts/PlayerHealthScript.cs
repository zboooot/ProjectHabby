using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthScript : MonoBehaviour
{
    public PlayerStatScriptableObject playerSO; 
    public Slider healthSlider;
    public Image healthFill;
    public float lerpSpeed = 2f;

    private ShakeScript shakeMe;
    private float currentHealth;

    private void Start()
    {
        shakeMe = healthSlider.gameObject.GetComponent<ShakeScript>();
        //playerSO.health = 100;
        currentHealth = playerSO.health; // Set initial health to full
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
            TakeDamage();
        }
    }

    public void TakeDamage()
    {
        shakeMe.StartShake();
        playerSO.health -= 5; // Adjust the damage amount as needed
        Debug.Log("ouch" + playerSO.health);
    }

    private void UpdateHealthBar()
    {
        healthSlider.value = currentHealth; // Update the slider's value
        healthFill.fillAmount = currentHealth; // Update the fill amount of the health bar
    }


}
