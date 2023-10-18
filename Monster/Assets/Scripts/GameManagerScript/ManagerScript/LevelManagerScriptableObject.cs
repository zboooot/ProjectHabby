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
        
        currentDestruction = 0;
        destructionLevel = 0;
    }
}
