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
    public List<Image> skullList = new List<Image>();
    public Sprite filledSkull;
    public Sprite ogSkull;

    // Start is called before the first frame update
    void Start()
    {   
        levelCompleteText = GameObject.Find("LevelCompleteText");
        levelCompleteText.SetActive(false);
        Invoke("CalculateTotalDestruction", 1f);
        foreach(Image skull in skullList)
        {
           skull.sprite = ogSkull;
        }
    }

    public void CalculateTotalDestruction()
    {
        GameObject[] buildings = GameObject.FindGameObjectsWithTag("BigBuilding");
        levelData.buildingsInScene = buildings.Length;

        switch (levelData.cityLevel)
        {
            case 1:
                calculateCityDestruction = 23;
                break;

            case 2:
                calculateCityDestruction = 22;
                break;

            case 3:
                calculateCityDestruction = 21;
                break;

            case 4:
                calculateCityDestruction = 20;
                break;

            case 5:
                calculateCityDestruction = 19;
                break;

            case 6:
                calculateCityDestruction = 18;
                break;

            case 7:
                calculateCityDestruction = 17;
                break;

            case 8:
                calculateCityDestruction = 16;
                break;

            case 9:
                calculateCityDestruction = 15;
                break;

            case 10:
                calculateCityDestruction = 14;
                break;
        }

        calculation1 = Mathf.RoundToInt(levelData.buildingsInScene / calculateCityDestruction);
        Debug.Log(calculation1);
        slider.maxValue = calculation1;
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
            levelData.destructionLevel = 0;
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
        if (destructionProgress >= 0 && destructionProgress <= 30)
        {
            levelData.destructionLevel = 0;
          
        }

        else if (destructionProgress >= 31 && destructionProgress <= 70)
        {
            levelData.destructionLevel = 1;
        }

        else { levelData.destructionLevel = 2; }
    }

    private void Update()
    {
        switch (levelData.destructionLevel)
        {
            case 0:
                skullList[0].GetComponent<Image>().sprite = filledSkull;
                break;
            case 1:
                skullList[1].GetComponent<Image>().sprite = filledSkull;
                break;
            case 2:
                skullList[2].GetComponent<Image>().sprite = filledSkull;
                break;
        }
    }
}
