using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainWeather : Weather
{
    public WeatherScriptableObject weatherData;
    public bool isLeft;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        weatherData.target = GameObject.FindGameObjectWithTag("Player");
        Rigidbody2D rb = weatherData.target.GetComponent<Rigidbody2D>();
        RandomizeRainDirection();
    }

    void RandomizeRainDirection()
    {
        int randomint = Random.Range(0, 1);
        if (randomint == 0)
        {
            //Rain comes from the left
            isLeft = true;
        }
        else
        {
            //Rain comes from the right
            isLeft = false;
        }
    }
    void StaticEffect()
    {
        if (isLeft)
        {
            if (rb.velocity.x < 0)
            {
                weatherData.target.GetComponent<PlayerInputHandler>().movementSpeed /= 2;
            }
        }

        else
        {
            if (rb.velocity.x > 0)
            {
                weatherData.target.GetComponent<PlayerInputHandler>().movementSpeed /= 2;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        StaticEffect();
    }
}
