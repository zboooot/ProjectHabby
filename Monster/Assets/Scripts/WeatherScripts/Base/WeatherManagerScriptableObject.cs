using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeatherManagerScriptableObject", menuName = "ScriptableObject/WeatherManager")]
public class WeatherManagerScriptableObject : ScriptableObject
{
    public WeatherScriptableObject defaultWeather;
    public List<WeatherScriptableObject> list = new List<WeatherScriptableObject>();
    public WeatherScriptableObject upcomingWeather;
    public bool isSpecialWeather;
}
