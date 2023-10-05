using UnityEngine;

[CreateAssetMenu(fileName = "LevelScriptableObject", menuName = "ScriptableObject/Level")]
public class LevelManagerScriptableObject : ScriptableObject
{
    public int buildingsInScene;
    public float destructionLevel;
    public int cityLevel;
    public float currentDestruction;

    private void OnEnable()
    {
        buildingsInScene = 0;
        currentDestruction = 0;
        destructionLevel = 0;
    }
}
