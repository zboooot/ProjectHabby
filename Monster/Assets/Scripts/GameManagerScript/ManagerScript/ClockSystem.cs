using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClockSystem : MonoBehaviour
{
    public LevelManagerScriptableObject levelData;
    public TextMeshProUGUI timerText;
    private GameManagerScript gameManager;

    private float timerValue;
    private float addOnTime;

    public bool startTime;
    // Start is called before the first frame update
    void Start()
    {
        CalculateLevelTime();
        timerText = GetComponent<TextMeshProUGUI>();
        DisplayTime(timerValue);
        startTime = false;

        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (startTime)
        {
            if (timerValue > 0)
            {
                timerValue -= Time.deltaTime;
            }
            else
            {
                timerValue = 0;
                gameManager.isVictory = false;
                gameManager.TriggerEndScreen();
            }

            DisplayTime(timerValue);
        }

        else { return; }
    }

    public void CalculateLevelTime()
    {
        switch (levelData.cityLevel)
        {
            case 1:
                addOnTime = 0;
                break;

            case 2:
                addOnTime = 30;
                break;

            case 3:
                addOnTime = 45;
                break;

            case 4:
                addOnTime = 60;
                break;

            case 5:
                addOnTime = 90;
                break;

            case 6:
                addOnTime = 105;
                break;

            case 7:
                addOnTime = 120;
                break;

            case 8:
                addOnTime = 135;
                break;

            case 9:
                addOnTime = 150;
                break;

            case 10:
                addOnTime = 160;
                break;
        }

        timerValue = levelData.baseTime + addOnTime;
    }

    void DisplayTime(float timeToDisplay)
    {
        if(timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
