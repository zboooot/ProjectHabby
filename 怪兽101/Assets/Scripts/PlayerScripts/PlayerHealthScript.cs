using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealthScript : MonoBehaviour
{
    public enum HealthState
    {
        normal,
        berserk,
    }

    public PlayerStatScriptableObject playerSO;
    public Slider healthSlider;
    public Image healthFill;
    public float lerpSpeed = 2f;

    private ShakeScript shakeMe;
    private float currentHealth;
    public HealthState healthState;

    //Flash Effect
    private PlayerFlash flashEffect;
    private int thresholdHealth;
    public int triggerNumber;

    //Berserk mode Feedback
    private CanvasGroup berserkVignette;
    private bool fadeIn;
    private bool fadeOut;

    private void Start()
    {
        shakeMe = healthSlider.gameObject.GetComponent<ShakeScript>();
        //playerSO.health = 100;
        currentHealth = playerSO.health; // Set initial health to full
        thresholdHealth = playerSO.health;
        flashEffect = GetComponent<PlayerFlash>();
        UpdateHealthBar();

        healthState = HealthState.normal;

        berserkVignette = GameObject.Find("Vignette").GetComponent<CanvasGroup>();
    }

    void TriggerVignette()
    {
        if(healthState != HealthState.berserk)
        {
            if (berserkVignette.alpha >= 0)
            {
                berserkVignette.alpha -= Time.deltaTime;
            }
        }

        else
        {
            if (berserkVignette.alpha < 1)
            {
                berserkVignette.alpha += Time.deltaTime;
            }
        }
    }

    private void Update()
    {
        if (currentHealth != playerSO.health)
        {
            // Lerp towards the target health value
            currentHealth = Mathf.Lerp(currentHealth, playerSO.health, Time.deltaTime * lerpSpeed);
            UpdateHealthBar();
        }

        TriggerVignette();
    }

    void CheckHealthStatus(float playerhealth)
    {
        int healthPercentage = (int)(100 - ((100f / playerSO.maxhealth) * currentHealth));

        if (healthPercentage <= 45)
        {
            healthState = HealthState.normal;
        }

        else
        {
            healthState = HealthState.berserk;
        }
    }

    public void TakeDamage(int damage)
    {
        shakeMe.StartShake();
        CheckHealthStatus(currentHealth);
        if(healthState == HealthState.normal)
        {
            playerSO.health -= damage; // Adjust the damage amount as needed
        }

        else
        {
            playerSO.health -= damage * 2; // Player takes double damage when they are in berserk mode
        }

        int healthDifference = thresholdHealth - playerSO.health;
        if(healthDifference >= triggerNumber)
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
