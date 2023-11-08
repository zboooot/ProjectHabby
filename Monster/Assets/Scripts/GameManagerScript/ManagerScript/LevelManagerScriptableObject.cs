using UnityEngine;

[CreateAssetMenu(fileName = "LevelScriptableObject", menuName = "ScriptableObject/Level")]
public class LevelManagerScriptableObject : ScriptableObject
{
    public int buildingsInScene;
    public int baseScore;
    public float destructionLevel;
    public int cityLevel;
    public float currentDestruction;
    public bool cutscenePlayed;
    public float baseTime;

    private void OnEnable()
    {
        currentDestruction = 0;
        destructionLevel = 0;
    }
}
