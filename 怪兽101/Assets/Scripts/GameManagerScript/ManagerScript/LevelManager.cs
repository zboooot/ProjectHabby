using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public LevelManagerScriptableObject levelData;
    private int calculateCityDestruction;
    private int calculation1;
    public GameObject levelCompleteText;
    public Slider slider;
    private GNAManager gnaManager;

    private void Awake()
    {
        GameObject[] buildings = GameObject.FindGameObjectsWithTag("BigBuilding");
        levelData.buildingsInScene = buildings.Length;
        CalculateTotalDestruction();
        slider.maxValue = calculation1;
        levelCompleteText = GameObject.Find("LevelCompleteText");
        levelCompleteText.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    void CalculateTotalDestruction()
    {
        switch (levelData.cityLevel)
        {
            case 10:
                calculateCityDestruction = 95;
                break;

            case 9:
                calculateCityDestruction = 115;
                break;

            case 8:
                calculateCityDestruction = 120;
                break;

            case 7:
                calculateCityDestruction = 125;
                break;

            case 6:
                calculateCityDestruction = 130;
                break;

            case 5:
                calculateCityDestruction = 135;
                break;

            case 4:
                calculateCityDestruction = 140;
                break;

            case 3:
                calculateCityDestruction = 145;
                break;

            case 2:
                calculateCityDestruction = 150;
                break;

            case 1:
                calculateCityDestruction = 155;
                break;
        }

        calculation1 = Mathf.RoundToInt(levelData.buildingsInScene % calculateCityDestruction);

    }

    public void CalculateScore(int score)
    {
        slider.value += score;
        levelData.currentDestruction = slider.value;
        CalculateProgress();
        if(slider.value == slider.maxValue)
        {
            levelCompleteText.SetActive(true);
            levelData.cityLevel += 1;
            Invoke("ChangeLevel", 3f);
        }
    }

    private void ChangeLevel()
    {
        SceneManager.LoadScene("LevelSelectScene");
    }

    public void CalculateProgress()
    {
        float destructionProgress = Mathf.Round((levelData.currentDestruction / calculation1) * 100f);
        if (destructionProgress >= 0 && destructionProgress <= 44)
        {
            levelData.destructionLevel = 0;
        }

        else if (destructionProgress >= 45 && destructionProgress <= 74)
        {
            levelData.destructionLevel = 1;
        }

        else { levelData.destructionLevel = 2; }
    }
}
