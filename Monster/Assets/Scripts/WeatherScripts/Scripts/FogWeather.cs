using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogWeather : Weather
{
    public WeatherScriptableObject weatherData;
    // Start is called before the first frame update
    void Start()
    {
        weatherData.target = GameObject.FindGameObjectWithTag("Player");
        Rigidbody2D rb = weatherData.target.GetComponent<Rigidbody2D>();
    }

    void StaticEffect()
    {
        //Activate Fog of War Vingette here
        weatherData.target.GetComponent<PlayerInputHandler>().ultimateRadius /= 2;
    }

    // Update is called once per frame
    void Update()
    {
        StaticEffect();
    }
}
