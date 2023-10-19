using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayWeatherManager : MonoBehaviour
{
    public WeatherManagerScriptableObject weatherManagerData;
    public List<Weather> list = new List<Weather>();

    // Start is called before the first frame update
    void Start()
    {
        CheckIndex();
        ActivateWeather();
    }

    void CheckIndex()
    {
        foreach(Weather weather in list)
        {
            if (weather.weatherIndex != weatherManagerData.upcomingWeather.WeatherIndex)
            {
                weather.isActivated = false;
            }

            else
            {
                weather.isActivated = true;
            }
        }
    }

    void ActivateWeather()
    {

        foreach(Weather weather in list)
        {
            if (weather.isActivated)
            {
                weather.gameObject.SetActive(true);
            }
            else
            {
                weather.gameObject.SetActive(false);
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
