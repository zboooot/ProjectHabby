using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "RampageGameManagerScriptableObject", menuName = "ScriptableObject/RampageManager")]
public class RampageGameManagerScriptableObject : ScriptableObject
{
    public float time;
    public int waves;
    public float timeToNextWave;
    public float pointsPerWave;
    public int killCount;
    public int highscore;
}
