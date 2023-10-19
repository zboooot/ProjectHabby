using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeatherScriptableObject", menuName = "ScriptableObject/Weather")]
public class WeatherScriptableObject : ScriptableObject
{
    public string WeatherName;
    public int WeatherIndex;
    public GameObject target;
    public GameObject[] targets;

    public void GameEndTrigger() { }
}
