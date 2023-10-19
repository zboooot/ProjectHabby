using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class MenuWeatherManager : MonoBehaviour
{
    public WeatherManagerScriptableObject weatherManagerData;
    private TextMeshProUGUI weatherText;

    // Start is called before the first frame update
    void Start()
    {
        weatherText = GameObject.Find("WeatherText").GetComponent<TextMeshProUGUI>();
        RandomizeWeather();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            RandomizeWeather();
        }
    }

    void RandomizeWeather()
    {
        int firstCheck = Random.Range(0, 2);
        if(firstCheck == 0)
        {
            weatherManagerData.upcomingWeather = weatherManagerData.defaultWeather;
            weatherManagerData.isSpecialWeather = false;
            weatherText.text = weatherManagerData.upcomingWeather.WeatherName;
        }

        else
        {
            int randomNum = Random.Range(0, weatherManagerData.list.Count);
            weatherManagerData.upcomingWeather = weatherManagerData.list[randomNum];
            weatherManagerData.isSpecialWeather = true;
            weatherText.text = weatherManagerData.upcomingWeather.WeatherName;
        }
    }
}
